using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CaseReview.DataLayer;
using CaseReview.DataLayer.Models;
using CaseReview.BusinessObjects;
using System.Reflection;
using System.Web;

namespace CaseReview.BusinessLogic
{
    public class ReportLogic
    {
        //SearchVwReportStaffSections
        const string constSelectedStaffValue = "Selected Staff";
        const string constSelectedSectionsValue = "Selected Sections";

        public string GenerateStaffReportDataAsCsv(
            DateTime from
            , DateTime to
            , List<Guid> staffIds
            , List<Guid> sectionsIds
            , bool isDisplayByStaffFirst
            , bool isStaffCombined
            , bool isSectionsCombined)
        {
            // get the data
            var reportDa = new ReportDa();
            int intFromDate = (from.Year * 100) + from.Month;
            int intToDate = (to.Year * 100) + to.Month;
            var data = reportDa.SearchVwReportStaffSections(
            intFromDate
            , intToDate
            , staffIds
            , sectionsIds).ToList();

            //var csvUtils = new CsvUtils();

            PropertyInfo[] properties = typeof(vwReportStaffSection).GetProperties();
            var separator = ",";
            StringBuilder csvBuild = new StringBuilder(String.Join(separator, properties.Select(f => "\"" + f.Name + "\"").ToArray()));
            foreach (var o in data)
            {
                //  + "\""
                var newLine = System.Environment.NewLine  + HttpUtility.HtmlDecode(string.Join(separator, properties.Select(f => ("\"" + f.GetValue(o).ToString() + "\"" )).ToArray()));
                csvBuild.Append(newLine);
            }
            return csvBuild.ToString();
        }

        

        public ReportStaffSections GenerateStaffReportData(
            DateTime from
            , DateTime to
            , List<Guid> staffIds
            , List<Guid> sectionsIds
            , bool isDisplayByStaffFirst
            , bool isStaffCombined
            , bool isSectionsCombined)
        {
            // this is what we're returning
            var reportStaffSections = new ReportStaffSections();

            // get the data
            var reportDa = new ReportDa();
            int intFromDate = (from.Year * 100) + from.Month;
            int intToDate = (to.Year * 100) + to.Month;
            var data = reportDa.SearchVwReportStaffSections(
            intFromDate
            , intToDate
            , staffIds
            , sectionsIds);

            if (!data.Any())
            {
                return reportStaffSections;
            }

            // need to order the data accordingly
            var orderedData = data.OrderBy(o => o.SectionID).ThenBy(o => o.StaffSurname).ThenBy(o => o.ReviewedDateYear).ThenBy(o => o.ReviewedDateMonth);
            if (isDisplayByStaffFirst)
            {
                orderedData = data.OrderBy(o => o.StaffSurname).ThenBy(o => o.SectionID).ThenBy(o => o.ReviewedDateYear).ThenBy(o => o.ReviewedDateMonth);
            }

            // set up the a reportItem to combine data in the loop
            var firstDataItem = orderedData.First();

            AddReportItem(reportStaffSections, firstDataItem, isDisplayByStaffFirst, isStaffCombined, isSectionsCombined);
            // loop through data and build report items

            // var lastReportItem = reportStaffSections.ReportItems.First();

            foreach (var dataItem in orderedData)
            {
                // START get the reportItem we need from the list of reportStaffSections.ReportItems

                var reportItem = reportStaffSections.ReportItems.FirstOrDefault();

                if (isDisplayByStaffFirst)
                {
                    if (isStaffCombined)
                    {
                        if (isSectionsCombined)
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} - {1}", constSelectedStaffValue, constSelectedSectionsValue)).FirstOrDefault();
                        }
                        else
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} - {1}", constSelectedStaffValue, dataItem.SectionName)).FirstOrDefault();
                        }
                    }
                    else
                    {
                        // staff not comb

                        if (isSectionsCombined)
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} {1} - {2}", dataItem.StaffFirstname, dataItem.StaffSurname, constSelectedSectionsValue)).FirstOrDefault();
                        }
                        else
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} {1} - {2}", dataItem.StaffFirstname, dataItem.StaffSurname, dataItem.SectionName)).FirstOrDefault();
                        } 
                    }
                }
                else
                {
                    // not isDisplayByStaffFirst ie. section first

                    if (isStaffCombined)
                    {
                        if (isSectionsCombined)
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} - {1}", constSelectedSectionsValue, constSelectedStaffValue)
                                ).FirstOrDefault();
                        }
                        else
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} - {1}", dataItem.SectionName, constSelectedStaffValue)).FirstOrDefault();
                        }
                    }
                    else
                    {
                        // staff not comb

                        if (isSectionsCombined)
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} - {1} {2}", constSelectedSectionsValue, dataItem.StaffFirstname, dataItem.StaffSurname)).FirstOrDefault();
                        }
                        else
                        {
                            reportItem = reportStaffSections.ReportItems.Where(
                                o => o.Title == string.Format("{0} - {1} {2}", dataItem.SectionName, dataItem.StaffFirstname, dataItem.StaffSurname)).FirstOrDefault();
                        }
                    }
                }
                
                if (reportItem == null)
                {
                    reportItem = AddReportItem(reportStaffSections, dataItem, isDisplayByStaffFirst, isStaffCombined, isSectionsCombined);
                }
                // END get the reportItem we need from the list of reportStaffSections.ReportItems

                // we now have the reportItem we need

                AddDataToReportItem(reportItem, dataItem);

                //lastReportItem = reportItem;
            }

            foreach (var item in reportStaffSections.ReportItems)
            {
                item.LineChartNumberNo.DataItems.Sort(delegate (DataItem x, DataItem y)
                {
                    return x.Order.CompareTo(y.Order);
                });

                item.LineChartNumberAnswered.DataItems.Sort(delegate (DataItem x, DataItem y)
                {
                    return x.Order.CompareTo(y.Order);
                });

            }
            
            return reportStaffSections;
        }

        

        private void AddDataToReportItem(ReportItem reportItem, vwReportStaffSection vwReport)
        {
            var sDate = string.Format("{0}/{1}", vwReport.ReviewedDateMonth, vwReport.ReviewedDateYear.ToString().Substring(2, 2));
            var dataItem1 = reportItem.LineChartNumberAnswered.DataItems.FirstOrDefault(o => o.Label == sDate);

            if (dataItem1 == null)
            {
                dataItem1 = new DataItem();
                var dataItem2 = new DataItem();
                dataItem1.Label = sDate;
                dataItem1.Data = 0;
                dataItem1.Order = vwReport.ReviewedDateInt;

                dataItem2.Label = sDate;
                dataItem2.Data = 0;
                dataItem2.Order = vwReport.ReviewedDateInt;


                reportItem.LineChartNumberAnswered.DataItems.Add(dataItem1);
                reportItem.LineChartNumberNo.DataItems.Add(dataItem2);
            }

            var index = reportItem.LineChartNumberAnswered.DataItems.IndexOf(dataItem1);

            var dataPieYes = reportItem.PieChart.DataItems[0].Data;
            var dataPieNo = reportItem.PieChart.DataItems[1].Data;

            var dataNumberAnswered = reportItem.LineChartNumberAnswered.DataItems[index].Data;
            var dataNumberNo = reportItem.LineChartNumberNo.DataItems[index].Data;

            dataPieYes += vwReport.SumCompliant;
            reportItem.PieChart.DataItems[0].Data = dataPieYes;

            dataPieNo += vwReport.SumNonCompliant;
            reportItem.PieChart.DataItems[1].Data = dataPieNo;
            
            dataNumberAnswered += vwReport.SumCompliant + vwReport.SumNonCompliant;
            reportItem.LineChartNumberAnswered.DataItems[index].Data = dataNumberAnswered;
            
            dataNumberNo += vwReport.SumNonCompliant;
            reportItem.LineChartNumberNo.DataItems[index].Data = dataNumberNo;
            
        }

        private ReportItem AddReportItem(
            ReportStaffSections reportStaffSections
            , vwReportStaffSection dataItem
            , bool isDisplayByStaffFirst
            , bool isStaffCombined
            , bool isSectionsCombined)
        {

            var reportItem = new ReportItem();
            reportItem.PieChart.DataItems.Add(new DataItem() { Data = 0, Label = "Yes", Order = dataItem.ReviewedDateInt });
            reportItem.PieChart.DataItems.Add(new DataItem() { Data = 0, Label = "No", Order = dataItem.ReviewedDateInt });

            var sDate = string.Format("{0}/{1}", dataItem.ReviewedDateMonth, dataItem.ReviewedDateYear.ToString().Substring(2, 2));
            reportItem.LineChartNumberNo.DataItems.Add(new DataItem() { Data = 0, Label = sDate, Order = dataItem.ReviewedDateInt });
            reportItem.LineChartNumberAnswered.DataItems.Add(new DataItem() { Data = 0, Label = sDate, Order = dataItem.ReviewedDateInt });

            if (isDisplayByStaffFirst)
            {
                if (isStaffCombined)
                {
                    if (isSectionsCombined)
                    {

                        reportItem.Title = string.Format("{0} - {1}", constSelectedStaffValue, constSelectedSectionsValue);
                    }
                    else
                    {

                        reportItem.Title = string.Format("{0} - {1}", constSelectedStaffValue, dataItem.SectionName);
                    }
                }
                else
                {
                    // staff not comb

                    if (isSectionsCombined)
                    {

                        reportItem.Title = string.Format("{0} {1} - {2}", dataItem.StaffFirstname, dataItem.StaffSurname, constSelectedSectionsValue);
                    }
                    else
                    {

                        reportItem.Title = string.Format("{0} {1} - {2}", dataItem.StaffFirstname, dataItem.StaffSurname, dataItem.SectionName);
                    }
                }
            }
            else
            {
                // not isDisplayByStaffFirst ie. section first

                if (isStaffCombined)
                {
                    if (isSectionsCombined)
                    {

                        reportItem.Title = string.Format("{0} - {1}", constSelectedSectionsValue, constSelectedStaffValue);
                    }
                    else
                    {

                        reportItem.Title = string.Format("{0} - {1}", dataItem.SectionName, constSelectedStaffValue);
                    }
                }
                else
                {
                    // staff not comb

                    if (isSectionsCombined)
                    {

                        reportItem.Title = string.Format("{0} - {1} {2}", constSelectedSectionsValue, dataItem.StaffFirstname, dataItem.StaffSurname);
                    }
                    else
                    {

                        reportItem.Title = string.Format("{0} - {1} {2}", dataItem.SectionName, dataItem.StaffFirstname, dataItem.StaffSurname);
                    }
                }
            }

            reportStaffSections.ReportItems.Add(reportItem);
            return reportItem;

        }
    }
}
