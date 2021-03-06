﻿using CaseReview.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseReview.WebApp.Controllers
{
    [Authorize(Roles = "Admin, CaseReviewer, Reporter")]
    public class PrototypesController : Controller
    {
        // GET: Prototypes
        public ActionResult Index()
        {
            var model = new List<PieChartModel>();
            model.Add(new PieChartModel()
            {
                Title = "Statements and Debt Repayment Offers",
                CanvasId = "canvas1",
                Data = new List<int>() { 572, 6 },
                Labels = new List<string>() { "Yes (99% 572)", "No (1% 6)" }
            });

            model.Add(new PieChartModel()
            {
                Title = "Debt Management Plans",
                CanvasId = "canvas2",
                Data = new List<int>() { 1023, 8 },
                Labels = new List<string>() { "Yes (99% 1023)", "No (1% 8)" }
            });

            model.Add(new PieChartModel()
            {
                Title = "Credit Control",
                CanvasId = "canvas3",
                Data = new List<int>() { 343, 12 },
                Labels = new List<string>() { "Yes (97% 343)", "No (3% 12)" }
            });

            model.Add(new PieChartModel()
            {
                Title = "Vulnerability and other advice triggers",
                CanvasId = "canvas4",
                Data = new List<int>() { 48, 0 },
                Labels = new List<string>() { "Yes (100% 48)", "No (0% 0)" }
            });

            model.Add(new PieChartModel()
            {
                Title = "Creditor Contact",
                CanvasId = "canvas5",
                Data = new List<int>() { 828, 4 },
                Labels = new List<string>() { "Yes (99% 828)", "No (1% 4)" }
            });


            model.Add(new PieChartModel()
            {
                Title = "Review or advised by customer",
                CanvasId = "canvas6",
                Data = new List<int>() { 436, 36 },
                Labels = new List<string>() { "Yes (92% 436)", "No (8% 36)" }
            });


            model.Add(new PieChartModel()
            {
                Title = "Customer Contact",
                CanvasId = "canvas7",
                Data = new List<int>() { 653, 36 },
                Labels = new List<string>() { "Yes (95% 653)", "No (5% 36)" }
            });


            model.Add(new PieChartModel()
            {
                Title = "Steve Bean",
                CanvasId = "canvas8",
                Data = new List<int>() { 691, 16 },
                Labels = new List<string>() { "Yes (98% 691)", "No (2% 16)" }
            });


            model.Add(new PieChartModel()
            {
                Title = "Amy Chesterfield",
                CanvasId = "canvas9",
                Data = new List<int>() { 790, 22 },
                Labels = new List<string>() { "Yes (97% 790)", "No (3% 22)" }
            });


            model.Add(new PieChartModel()
            {
                Title = "Sally Foss",
                CanvasId = "canvas10",
                Data = new List<int>() { 465, 10 },
                Labels = new List<string>() { "Yes (98% 465)", "No (2% 10)" }
            });


            /*
		

*/

            model.Add(new PieChartModel()
            {
                Title = "Steve Parry",
                CanvasId = "canvas11",
                Data = new List<int>() { 575, 42 },
                Labels = new List<string>() { "Yes (93% 575)", "No (7% 42)" }
            });


            model.Add(new PieChartModel()
            {
                Title = "Natalie Savage",
                CanvasId = "canvas12",
                Data = new List<int>() { 786, 5 },
                Labels = new List<string>() { "Yes (99% 786)", "No (1% 5)" }
            });


            model.Add(new PieChartModel()
            {
                Title = "Jenna Williams",
                CanvasId = "canvas13",
                Data = new List<int>() { 596, 7 },
                Labels = new List<string>() { "Yes (99% 596)", "No (1% 7)" }
            });


            return View(model);
        }


        public ActionResult One()
        {
            var model = new ReportStaffSections();
            var reportItem = new ReportItem();
            reportItem.ReportItemTitle = "All Staff, all sections";

            reportItem.PieChart = new PieChartModel()
            {
                CanvasId = "canvasP1",
                Data = new List<int>() { 572, 6 },
                Labels = new List<string>() { "Yes (99% 572)", "No (1% 6)" }
            };

            reportItem.LineChartPercNo = new LineChartModel()
            {
                CanvasId = "canvasLpn1",
                Data = new List<int>() { 1, 1, 0, 0, 1, 2, 1, 0, 0, 1 },
                Labels = new List<string>() { "9/15", "10/15", "11/15", "12/15", "1/16", "2/16", "3/16", "4/16", "5/16", "6/16" }
            };


            reportItem.LineChartNumberAnswered = new LineChartModel()
            {
                CanvasId = "canvasLna1",
                Data = new List<int>() { 45, 37, 23, 10, 70, 82, 52, 49, 61, 50 },
                Labels = new List<string>() { "9/15", "10/15", "11/15", "12/15", "1/16", "2/16", "3/16", "4/16", "5/16", "6/16" }
            };
            
            model.ReportItems.Add(reportItem);
            return View("StaffSectionsReport", model);
        }

        public ActionResult two()
        {
            var model = new ReportStaffSections();

            for (int i = 3; i <= 5; i++)
            {
                var reportItem1 = new ReportItem();
                reportItem1.ReportItemTitle = "Some StaffMember" + i.ToString() + ", all sections";

                reportItem1.PieChart = new PieChartModel()
                {
                    CanvasId = "canvasP" + i.ToString(),
                    Data = new List<int>() { 572, 6 },
                    Labels = new List<string>() { "Yes (99% 572)", "No (1% 6)" }
                };

                reportItem1.LineChartPercNo = new LineChartModel()
                {
                    CanvasId = "canvasLpn" + i.ToString(),
                    Data = new List<int>() { 1, 1, 0, 0, 1, 2, 1, 0, 0, 1 },
                    Labels = new List<string>() { "9/15", "10/15", "11/15", "12/15", "1/16", "2/16", "3/16", "4/16", "5/16", "6/16" }
                };


                reportItem1.LineChartNumberAnswered = new LineChartModel()
                {
                    CanvasId = "canvasLna" + i.ToString(),
                    Data = new List<int>() { 45, 37, 23, 10, 70, 82, 52, 49, 61, 50 },
                    Labels = new List<string>() { "9/15", "10/15", "11/15", "12/15", "1/16", "2/16", "3/16", "4/16", "5/16", "6/16" }
                };

                model.ReportItems.Add(reportItem1);

            }
            return View("StaffSectionsReport", model);
        }

        public ActionResult three()
        {
            var model = new ReportStaffSections();

            for (int i = 4; i <= 5; i++)
            {
                for (int y = 2; y <= 3; y++)
                {
                    var reportItem1 = new ReportItem();
                    reportItem1.ReportItemTitle = "Some StaffMember" + i.ToString() + ", Section" + y.ToString();

                    reportItem1.PieChart = new PieChartModel()
                    {
                        CanvasId = "canvasP" + i.ToString() + "_" + y.ToString(),
                        Data = new List<int>() { 572, 6 },
                        Labels = new List<string>() { "Yes (99% 572)", "No (1% 6)" }
                    };

                    reportItem1.LineChartPercNo = new LineChartModel()
                    {
                        CanvasId = "canvasLpn" + i.ToString() + "_" + y.ToString(),
                        Data = new List<int>() { 1, 1, 0, 0, 1, 2, 1, 0, 0, 1 },
                        Labels = new List<string>() { "9/15", "10/15", "11/15", "12/15", "1/16", "2/16", "3/16", "4/16", "5/16", "6/16" }
                    };


                    reportItem1.LineChartNumberAnswered = new LineChartModel()
                    {
                        CanvasId = "canvasLna" + i.ToString() + "_" + y.ToString(),
                        Data = new List<int>() { 45, 37, 23, 10, 70, 82, 52, 49, 61, 50 },
                        Labels = new List<string>() { "9/15", "10/15", "11/15", "12/15", "1/16", "2/16", "3/16", "4/16", "5/16", "6/16" }
                    };

                    model.ReportItems.Add(reportItem1);
                }
            }
            return View("StaffSectionsReport", model);
        }
    }
}