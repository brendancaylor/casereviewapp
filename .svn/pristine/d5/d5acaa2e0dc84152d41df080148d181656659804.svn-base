using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaseReview.BusinessLogic;
using CaseReview.WebApp.Models;
using System.Text;

namespace CaseReview.WebApp.Controllers
{
    public class ReportStaffSectionsController : Controller
    {
        // GET: ReportStaffSections

        private void LoadDds(ReportStaffSections model, bool isFirst)
        {
            var staffLogic = new GenericLogic<DataLayer.Models.Staff>();
            var sectionLogic = new GenericLogic<DataLayer.Models.Section>();
            var staffs = staffLogic.GetAll().ToList();
            var sections = sectionLogic.GetAll().ToList();

            foreach (var item in staffs)
            {
                model.staff.Add(new ListItem() { text = string.Format("{0} {1}", item.StaffFirstname, item.StaffSurname), value = item.ID});
            }
            foreach (var item in sections)
            {
                model.sections.Add(new ListItem() { text = string.Format("{0}", item.SectionName), value = item.ID });
            }
            if (isFirst)
            {
                //model.staffIds = model.staff.Select(x => x.value).ToList();
                //model.sectionIds = model.sections.Select(x => x.value).ToList();
            }
        }


        public ActionResult Index()
        {
            var model = new ReportStaffSections();
            LoadDds(model, true);
            PrepModel(model);
            return View(model);
        }
        

        public ActionResult GetCsv(ReportStaffSections model)
        {
            var logic = new ReportLogic();
            var csvData = logic.GenerateStaffReportDataAsCsv(
                            model.from
                            , model.to
                            , model.staffIds
                            , model.sectionIds
                            , model.isDisplayByStaffFirst
                            , model.isStaffCombined
                            , model.isSectionsCombined);

            byte[] byteData = Encoding.ASCII.GetBytes(csvData);
            return File(byteData, "text/csv", "ReportExport.csv");
        }

        [HttpPost]
        public ActionResult Index(ReportStaffSections model)
        {
            LoadDds(model, false);
            
            if (model.staffIds.Count() == 1)
            {
                model.isStaffCombined = false;
            }
            if (model.sectionIds.Count() == 1)
            {
                model.isSectionsCombined = false;
            }
            PrepModel(model);
            return View(model);
        }

        private void PrepModel(ReportStaffSections model)
        {

            var reportData = new ReportLogic().GenerateStaffReportData(
                            model.from
                            , model.to
                            , model.staffIds
                            , model.sectionIds
                            , model.isDisplayByStaffFirst
                            , model.isStaffCombined
                            , model.isSectionsCombined);
            int i = 0;
            foreach (var item in reportData.ReportItems)
            {
                i++;
                var reportItem = new ReportItem();

                reportItem.ReportItemTitle = item.Title;

                reportItem.PieChart = new PieChartModel()
                {
                    CanvasId = "canvasP" + i.ToString()
                };

                foreach (var di in item.PieChart.DataItems)
                {
                    reportItem.PieChart.Data.Add(di.Data);
                    reportItem.PieChart.Labels.Add(di.Label);
                }

                reportItem.LineChartPercNo = new LineChartModel()
                {
                    CanvasId = "canvasLpn" + i.ToString()
                };

                foreach (var di in item.LineChartNumberNo.DataItems)
                {
                    reportItem.LineChartPercNo.Data.Add(di.Data);
                    reportItem.LineChartPercNo.Labels.Add(di.Label);
                }

                reportItem.LineChartNumberAnswered = new LineChartModel()
                {
                    CanvasId = "canvasLna" + i.ToString()
                };

                foreach (var di in item.LineChartNumberAnswered.DataItems)
                {
                    reportItem.LineChartNumberAnswered.Data.Add(di.Data);
                    reportItem.LineChartNumberAnswered.Labels.Add(di.Label);
                }

                model.ReportItems.Add(reportItem);

            }
        }
    }
}