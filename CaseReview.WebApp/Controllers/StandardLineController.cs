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
    [Authorize(Roles = "Admin, CaseReviewer")]
    public class StandardLinesController : Controller
    {

        // GET: StandardLines
        public ActionResult Index()
        {
            var data = new GeneralLogic().GetAllStandardLine();
            data = data.OrderBy(o => o.Line).ToList();
            return View(data);
        }


        // GET: StandardLines/Details/5
        public ActionResult Details(Guid id)
        {

            var StandardLine = new GeneralLogic().GetStandardLine(id);
            if (StandardLine == null)
            {
                return HttpNotFound();
            }
            return View(StandardLine);
        }



        // GET: StandardLines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StandardLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StandardLine model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.StandardLine StandardLine = new DataLayer.Models.StandardLine()
                {
                    Id = Guid.NewGuid(),
                    Line = model.Line,
                    LineType = model.LineType
                };
                StandardLine = new GeneralLogic().AddStandardLine(StandardLine);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        // GET: StandardLines/Edit/5
        public ActionResult Edit(Guid id)
        {
            var dbobject = new GeneralLogic().GetStandardLine(id);

            Models.StandardLine model = new StandardLine()
            {
                ID = dbobject.Id,
                Line= dbobject.Line,
                LineType = dbobject.LineType
            };
            return View(model);
        }


        // POST: StandardLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StandardLine model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.StandardLine StandardLine = new DataLayer.Models.StandardLine()
                {
                    Id = model.ID,
                    Line = model.Line,
                    LineType = model.LineType
                };
                StandardLine = new GeneralLogic().UpdateStandardLine(StandardLine);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /*
         * 
        // GET: StandardLines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StandardLine StandardLine = db.StandardLines.Find(id);
            if (StandardLine == null)
            {
                return HttpNotFound();
            }
            return View(StandardLine);
        }

        // POST: StandardLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StandardLine StandardLine = db.StandardLines.Find(id);
            db.StandardLines.Remove(StandardLine);
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
