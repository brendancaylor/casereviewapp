﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaseReview.BusinessLogic;
using CaseReview.WebApp.Models;
using PagedList;

namespace CaseReview.WebApp.Controllers
{
    [Authorize]
    public class NonCompliantReportController : Controller
    {
        // GET: NonCompliantReport
        public ActionResult Index(int? month, int? year, string surname, int? page)
        {
            GeneralSearch s = new GeneralSearch();
            s.Month = month;
            s.Year = year;
            s.Surname= surname;
            s.Page = page ?? 1;
            s.PageSize = 10;
            ViewBag.Search = s;

            DateTime? date = null;
            if (month.HasValue && year.HasValue)
            {
                date = new DateTime(year.Value, month.Value, 1);
            }

            var data = new GeneralLogic().SearchvwNonCompliant(date, surname);
            var model = new List<NonCompliant>();

            s.ChartLabel = "";
            s.ChartData = "";

            int LabelIndex = -1;
            List<int> Counts = new List<int>();

            foreach (var o in data.OrderBy(o => o.Year).ThenBy(o => o.Month))
            {
                var sDate = string.Format(", \"{0}/{1}\"", o.Month, o.Year.ToString().Substring(2,2));
                if (!s.ChartLabel.Contains(sDate))
                {
                    s.ChartLabel += sDate;
                    LabelIndex ++;
                    Counts.Add(1);
                }
                else
                {
                    Counts[LabelIndex] += 1;
                }
            }

            foreach (var count in Counts)
            {
                var sDate = string.Format(", {0}", count); 
                s.ChartData += sDate;
            }

            if (s.ChartLabel.Length > 0)
            {
                s.ChartLabel = s.ChartLabel.Substring(1, s.ChartLabel.Length-1);
            }

            if (s.ChartData.Length > 0)
            {
                s.ChartData = s.ChartData.Substring(1, s.ChartData.Length - 1);
            }

            foreach (var o in data.OrderBy(o => o.Year).ThenBy(o => o.Month))
            {
                model.Add(new NonCompliant()
                {
                    StaffSurname = o.StaffSurname,
                    ClientRef = o.ClientRef,
                    Count = 1,
                    Month = o.Month,
                    Year = o.Year,
                    QuestionName = o.QuestionName,
                    QuestionOrder = o.QuestionOrder,
                    SectionName = o.SectionName,
                    SectionOrder = o.SectionOrder                    
                });
            }

            return View(model.ToPagedList(s.Page, s.PageSize));
        }

    }
}