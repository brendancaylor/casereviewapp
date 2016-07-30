using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CaseReview.BusinessLogic;
using CaseReview.DataLayer.Models;
using System.Web.Script.Serialization;
using CaseReview.WebApp.Models;
using CaseReview.WebApp.Models.Api;

namespace CaseReview.WebApp.Controllers.api
{
    public class NonCompliantTotalsController : ApiController
    {
        private JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        private List<string> feedbackTypes = new List<string>() {
        "Initial Development Notes", "Development Plan", "Re-training", "Disciplinary"};

        // api/NonCompliantTotals
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage getTotalsByStaffId(GetNcTotalsReq getNcTotalsReq)
        {
            var data = new GenericLogic<vwStaffNonCompliantTotal>().FindAll(o => o.StaffID == getNcTotalsReq.StaffID).Select(x => new vwStaffNonCompliantTotal
            {
                ID = x.ID,
                FeedbackType = x.FeedbackType,
                SectionID = x.SectionID,
                StaffID = x.StaffID,
                SumNonCompliant = x.SumNonCompliant,
                QuestionID = x.QuestionID,
                IsMandatory = x.IsMandatory
            }).ToList();

            var totals = new List<NonCompStaffTotal>();
            var totalThisQuestion = new NonCompStaffTotal()
            {
                FeedbackType = "Total",
                IsMandatory = false,
                IsThisQuestion = true,
                Total = 0
            };
            var totalAllManQuestions = new NonCompStaffTotal()
            {
                FeedbackType = "Total",
                IsMandatory = true,
                IsThisQuestion = false,
                Total = 0
            };
            var totalAllNonManQuestions = new NonCompStaffTotal()
            {
                FeedbackType = "Total",
                IsMandatory = false,
                IsThisQuestion = false,
                Total = 0
            };
            totals.Add(totalThisQuestion);
            totals.Add(totalAllManQuestions);
            totals.Add(totalAllNonManQuestions);

            foreach (var item in data)
            {
                // set the total for the question
                if (item.QuestionID == getNcTotalsReq.QuestionID)
                {
                    if(item.FeedbackType == "Disciplinary")
                    {
                        var debugTest = true;
                    }
                    totals.Add(new NonCompStaffTotal() {
                        FeedbackType = item.FeedbackType,
                        IsMandatory = (item.IsMandatory.HasValue && item.IsMandatory.Value) ? true : false,
                        IsThisQuestion = true,
                        Total = item.SumNonCompliant.Value
                    });
                    totalThisQuestion.IsMandatory = (item.IsMandatory.HasValue && item.IsMandatory.Value) ? true : false;
                    totalThisQuestion.Total += item.SumNonCompliant.Value;
                }

                // set the total for the fbt
                // see if one exists
                var existingTotal = totals.FirstOrDefault(o => o.IsThisQuestion == false && o.FeedbackType == item.FeedbackType && o.IsMandatory == (item.IsMandatory.HasValue && item.IsMandatory.Value));

                if(existingTotal == null)
                {
                    existingTotal = new NonCompStaffTotal()
                    {
                        FeedbackType = item.FeedbackType,
                        IsMandatory = (item.IsMandatory.HasValue && item.IsMandatory.Value) ? true : false,
                        IsThisQuestion = false
                    };
                    totals.Add(existingTotal);
                }
                existingTotal.Total += item.SumNonCompliant.Value;

                if (item.IsMandatory.HasValue && item.IsMandatory.Value)
                {
                    totalAllManQuestions.Total += item.SumNonCompliant.Value;
                }
                else
                {
                    totalAllNonManQuestions.Total += item.SumNonCompliant.Value;
                }
            }

            totals.Sort(delegate (NonCompStaffTotal x, NonCompStaffTotal y)
            {
                // its on a bool so reverse the order (y then x)
                var result = y.IsThisQuestion.CompareTo(x.IsThisQuestion);
                if(result == 0)
                {
                    result = x.FeedbackType.CompareTo(y.FeedbackType);
                    if (result == 0)
                    {
                        result = y.IsMandatory.CompareTo(x.IsMandatory);
                    }
                }
                return result;
            });

            var json = jsonSerializer.Serialize(totals);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
    }
}
