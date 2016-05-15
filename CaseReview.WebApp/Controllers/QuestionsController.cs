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

namespace CaseReview.WebApp.Controllers
{
    public class QuestionsController : Controller
    {

        // GET: Questions
        public ActionResult Index()
        {
            var data = new GeneralLogic().GetAllQuestion();
            return View(data);
        }


        // GET: Questions/Details/5
        public ActionResult Details(Guid id)
        {

            var question = new GeneralLogic().GetQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }



        // GET: Questions/Create
        public ActionResult Create()
        {
            Question model = new Question();
            model.ID = Guid.NewGuid();
            model.IsActive = true;
            foreach (var option in new GeneralLogic().GetAllSection())
            {
                model.Sections.Add(new SelectListItem()
                {
                    Text = option.SectionName,
                    Value = option.ID.ToString()
                });
            }
            model.Sections.First().Selected = true;
            return View(model);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.Question Question = new DataLayer.Models.Question()
                {
                    ID = Guid.NewGuid(),
                    DisplayOrder = model.DisplayOrder,
                    IsActive = model.IsActive,
                    QuestionName = model.QuestionName,
                    SectionID = model.SectionID
                };
                Question = new GeneralLogic().AddQuestion(Question);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        // GET: Questions/Edit/5
        public ActionResult Edit(Guid id)
        {
            var dbobject = new GeneralLogic().GetQuestion(id);

            Models.Question model = new Question()
            {
                DisplayOrder = dbobject.DisplayOrder,
                ID = dbobject.ID,
                IsActive = dbobject.IsActive,
                QuestionName = dbobject.QuestionName,
                SectionID = dbobject.SectionID
            };

            foreach (var option in new GeneralLogic().GetAllSection())
            {
                model.Sections.Add(new SelectListItem()
                {
                    Text = option.SectionName,
                    Value = option.ID.ToString()
                });
            }

            return View(model);
        }


        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.Question Question = new DataLayer.Models.Question()
                {
                    ID = model.ID,
                    DisplayOrder = model.DisplayOrder,
                    IsActive = model.IsActive,
                    QuestionName = model.QuestionName,
                    SectionID = model.SectionID
                };
                Question = new GeneralLogic().UpdateQuestion(Question);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /*
         * 
        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question Question = db.Questions.Find(id);
            if (Question == null)
            {
                return HttpNotFound();
            }
            return View(Question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question Question = db.Questions.Find(id);
            db.Questions.Remove(Question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

         */

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
