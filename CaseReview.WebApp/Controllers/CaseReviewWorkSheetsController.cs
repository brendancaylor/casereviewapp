using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaseReview.BusinessLogic;
using CaseReview.WebApp.Models;
using PagedList;
using System.Web.Script.Serialization;
using Answer = CaseReview.DataLayer.Models.Answer;
using CaseReviewWorkSheet = CaseReview.WebApp.Models.CaseReviewWorkSheet;

namespace CaseReview.WebApp.Controllers
{
    [Authorize(Roles = "Admin, CaseReviewer")]
    public class CaseReviewWorkSheetsController : Controller
    {

        // GET: CaseReviewWorkSheets
        public ActionResult Index(int? month, int? year, string surname, int? page)
        {
            GeneralSearch s = new GeneralSearch();
            s.Month = month;
            s.Year = year;
            s.Surname = surname;
            s.Page = page ?? 1;
            s.PageSize = 10;
            ViewBag.Search = s;

            DateTime? date = null;
            if (month.HasValue && year.HasValue)
            {
                date = new DateTime(year.Value, month.Value, 1);
            }


            //var data = new CaseReviewWorkSheetLogic().GetAll();
            var data = new CaseReviewWorkSheetLogic().SearchCaseReviewWorkSheets(date, surname);
            return View(data.ToPagedList(s.Page, s.PageSize));
        }


        // GET: CaseReviewWorkSheets/Details/5
        public ActionResult Details(Guid id)
        {

            var CaseReviewWorkSheet = new CaseReviewWorkSheetLogic().Get(id);
            if (CaseReviewWorkSheet == null)
            {
                return HttpNotFound();
            }
            return View(CaseReviewWorkSheet);
        }

        private void LoadDds(CaseReviewWorkSheet model)
        {
            var caseReviewTypeLogic = new GenericLogic<DataLayer.Models.CaseReviewType>();
            var staffs = new StaffLogic().GetAllStaff().Where(o => o.IsActive).ToList();
            var caseReviewTypes = caseReviewTypeLogic.GetAll().Where(o => o.IsActive).ToList();
            if(model.StaffID == null)
            {
                model.StaffID = staffs.First().ID;
            }
            foreach (var staff in staffs)
            {
                model.StaffMembers.Add(new SelectListItem()
                {
                    Text = staff.StaffFirstname + " " + staff.StaffSurname,
                    Value = staff.ID.ToString(),
                    Selected = model.StaffID == staff.ID
                });
            }
            foreach (var caseReviewType in caseReviewTypes)
            {
                model.Types.Add(new SelectListItem() {
                    Text = caseReviewType.TypeName,
                    Value = caseReviewType.ID.ToString(),
                    Selected = model.CaseReviewTypeID == caseReviewType.ID
                    });
            }
            
            //model.Types.Add(new SelectListItem() { Text = "Client call", Value = "2" });
            //model.Types.Add(new SelectListItem() { Text = "Credit control", Value = "3" });


        }

        // GET: CaseReviewWorkSheets/Create
        public ActionResult Create()
        {
            Models.CaseReviewWorkSheet model = new CaseReviewWorkSheet()
            {
                ID = Guid.NewGuid()
            };
            LoadDds(model);
            return View(model);
        }

        // POST: CaseReviewWorkSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CaseReviewWorkSheet model)
        {
            // [Bind(Include = "ID,CaseReviewWorkSheetFirstname,CaseReviewWorkSheetSurname")] 
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.CaseReviewWorkSheet caseReviewWorkSheet = new DataLayer.Models.CaseReviewWorkSheet()
                {
                    ID = Guid.NewGuid(),
                    ClientRef = model.ClientRef,
                    ReviewedDate = model.ReviewedDate,
                    Reviewer = model.Reviewer,
                    StaffID = model.StaffID,
                    CaseReviewTypeID = model.CaseReviewTypeID,
                    Type = model.Type,
                    IsCompleted = model.IsCompleted
                };

                foreach (var question in new GeneralLogic().GetAllQuestionByType(model.CaseReviewTypeID).Where(o => o.IsActive))
                {
                    caseReviewWorkSheet.Answers.Add(new Answer()
                    {
                        CaseReviewWorkSheet = caseReviewWorkSheet,
                        ID = Guid.NewGuid(),
                        QuestionID = question.ID,
                        Comments = string.Empty
                    }
                        );
                }

                caseReviewWorkSheet = new CaseReviewWorkSheetLogic().Add(caseReviewWorkSheet);
                return RedirectToAction("Index");
            }

            LoadDds(model);
            return View(model);
        }



        // GET: CaseReviewWorkSheets/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.CaseReviewWorkSheet model = new CaseReviewWorkSheet();
            var dbObject = new CaseReviewWorkSheetLogic().Get(id);
            model.ID = dbObject.ID;
            model.ClientRef = dbObject.ClientRef;
            model.ReviewedDate = dbObject.ReviewedDate;
            model.Reviewer = dbObject.Reviewer;
            model.StaffID = dbObject.StaffID;
            model.Type = dbObject.Type;
            model.CaseReviewTypeID = dbObject.CaseReviewTypeID;
            model.CaseReviewTypeName = dbObject.CaseReviewType.TypeName;
            model.IsCompleted = dbObject.IsCompleted;

            LoadDds(model);
            LoadAnswers(model, dbObject);
            return View(model);
        }

        private void LoadAnswers(CaseReviewWorkSheet model, CaseReview.DataLayer.Models.CaseReviewWorkSheet dbObject)
        {
            model.StandardLines.Add(new SelectListItem() { Text = " - Select to add - " });
            foreach (var standardLine in new GeneralLogic().GetAllStandardLine("Comment").OrderBy(o => o.Line))
            {
                model.StandardLines.Add(new SelectListItem()
                {
                    Text = standardLine.Line,
                    Value = standardLine.Line,
                });
            }


            dbObject.Answers.OrderBy(o => o.Question.Section.DisplayOrder).ThenBy(o => o.Question.DisplayOrder);
            var lastSectionId = Guid.Empty;

            foreach (var answer in dbObject.Answers.OrderBy(o => o.Question.Section.DisplayOrder).ThenBy(o => o.Question.DisplayOrder))
            {
                var sectionAnswer = new SectionAnswer();

                if (answer.Question.Section.ID != lastSectionId)
                {
                    sectionAnswer = new SectionAnswer
                    {
                        DisplayOrder = answer.Question.Section.DisplayOrder,
                        ID = answer.Question.Section.ID,
                        SectionName = answer.Question.Section.SectionName
                    };

                    model.SectionAnswers.Add(sectionAnswer);
                    lastSectionId = answer.Question.Section.ID;
                }

                model.SectionAnswers.FirstOrDefault(o => o.ID == lastSectionId).Answers.Add(
                        new Models.Answer
                        {
                            ID = answer.ID,
                            CaseReviewWorkSheetID = model.ID,
                            Comments = answer.Comments,
                            Compliant = answer.Compliant,

                            Question = new Question()
                            {
                                ID = answer.QuestionID,
                                QuestionName = answer.Question.QuestionName,
                                DisplayOrder = answer.Question.DisplayOrder,
                                IsMandatory = answer.Question.IsMandatory,
                                Risk = answer.Question.Risk,
                                /*Section = new Section()
                                {
                                    SectionName = answer.Question.Section.SectionName,
                                    DisplayOrder = answer.Question.Section.DisplayOrder

                                }
                                */
                            }

                        }
                );
            }
        }

        // POST: CaseReviewWorkSheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CaseReviewWorkSheet model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.CaseReviewWorkSheet CaseReviewWorkSheet = new DataLayer.Models.CaseReviewWorkSheet()
                {
                    ID = model.ID,
                    ClientRef = model.ClientRef,
                    ReviewedDate = model.ReviewedDate,
                    Reviewer = model.Reviewer,
                    Type = model.Type,
                    IsCompleted = model.IsCompleted,
                    StaffID = model.StaffID,
                    CaseReviewTypeID = model.CaseReviewTypeID
                };

                CaseReviewWorkSheet = new CaseReviewWorkSheetLogic().Update(CaseReviewWorkSheet);

                return RedirectToAction("Index");
            }
            var dbObject = new CaseReviewWorkSheetLogic().Get(model.ID);
            LoadAnswers(model, dbObject);
            LoadDds(model);
            return View(model);
        }

 
        // GET: CaseReviewWorkSheets/Delete/5
        public ActionResult Delete(Guid id)
        {
            var dbObject = new CaseReviewWorkSheetLogic().Get(id);
            return View(dbObject);
        }

        // POST: CaseReviewWorkSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            new CaseReviewWorkSheetLogic().DeleteCaseReviewWorkSheet(id);
            return RedirectToAction("Index");
        }

        // GET: CaseReviewWorkSheets/Edit/5
        public ActionResult Update(Guid id)
        {
            Models.CaseReviewWorkSheet model = new CaseReviewWorkSheet();
            var dbObject = new CaseReviewWorkSheetLogic().Get(id);
            model.ID = dbObject.ID;
            model.ClientRef = dbObject.ClientRef;
            model.ReviewedDate = dbObject.ReviewedDate;
            model.Reviewer = dbObject.Reviewer;
            model.StaffID = dbObject.StaffID;
            model.Type = dbObject.Type;
            model.CaseReviewTypeID = dbObject.CaseReviewTypeID;
            model.CaseReviewTypeName = dbObject.CaseReviewType.TypeName;
            model.IsCompleted = dbObject.IsCompleted;

            LoadDds(model);


            model.StandardLines.Add(new SelectListItem() { Text = " - Select to add - " });
            foreach (var standardLine in new GeneralLogic().GetAllStandardLine("Comment").OrderBy(o => o.Line))
            {
                model.StandardLines.Add(new SelectListItem()
                {
                    Text = standardLine.Line,
                    Value = standardLine.Line,
                });
            }


            //dbObject.Answers.OrderBy(o => o.Question.Section.DisplayOrder).ThenBy(o => o.Question.DisplayOrder);
            var lastSectionId = Guid.Empty;

            foreach (var answer in dbObject.Answers.OrderBy(o => o.Question.Section.DisplayOrder).ThenBy(o => o.Question.DisplayOrder))
            {
                var sectionAnswer = new SectionAnswer();

                if (answer.Question.Section.ID != lastSectionId)
                {
                    sectionAnswer = new SectionAnswer
                    {
                        DisplayOrder = answer.Question.Section.DisplayOrder,
                        ID = answer.Question.Section.ID,
                        SectionName = answer.Question.Section.SectionName
                    };

                    model.SectionAnswers.Add(sectionAnswer);
                    lastSectionId = answer.Question.Section.ID;
                }

                model.SectionAnswers.FirstOrDefault(o => o.ID == lastSectionId).Answers.Add(
                    new Models.Answer
                    {
                        ID = answer.ID,
                        CaseReviewWorkSheetID = model.ID,
                        Comments = answer.Comments,
                        Compliant = answer.Compliant,

                        Question = new Question()
                        {
                            ID = answer.QuestionID,
                            QuestionName = answer.Question.QuestionName,
                            DisplayOrder = answer.Question.DisplayOrder,
                            IsMandatory = answer.Question.IsMandatory,
                            Risk = answer.Question.Risk,
                        }
                    }
                );
            }

            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(model.SectionAnswers);
            ViewBag.Json = json;

            ViewBag.urlApiUpdate = "api/casereview/update";
            ViewBag.urlApiUpdateAnswer = "api/casereview/saveanswer/savemany";
            return View("Update", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CaseReviewWorkSheet model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.CaseReviewWorkSheet CaseReviewWorkSheet = new DataLayer.Models.CaseReviewWorkSheet()
                {
                    ID = model.ID,
                    ClientRef = model.ClientRef,
                    ReviewedDate = model.ReviewedDate,
                    Reviewer = model.Reviewer,
                    Type = model.Type,
                    IsCompleted = model.IsCompleted,
                    StaffID = model.StaffID,
                    CaseReviewTypeID = model.CaseReviewTypeID
                };

                CaseReviewWorkSheet = new CaseReviewWorkSheetLogic().Update(CaseReviewWorkSheet);

                return RedirectToAction("Index");
            }
            LoadDds(model);
            return View(model);
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
