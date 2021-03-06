﻿using System;
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
    [Authorize(Roles = "Admin")]
    public class SectionsController : Controller
    {
        /*
        // GET: Sections
        public ActionResult Index()
        {
            var data = new GeneralLogic().GetAllSection();
            data = data.OrderBy(o => o.DisplayOrder).ToList();
            return View(data);
        }


        // GET: Sections/Details/5
        public ActionResult Details(Guid id)
        {

            var Section = new GeneralLogic().GetSection(id);
            if (Section == null)
            {
                return HttpNotFound();
            }
            return View(Section);
        }



        // GET: Sections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Section model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.Section Section = new DataLayer.Models.Section()
                {
                    ID = Guid.NewGuid(),
                    DisplayOrder = model.DisplayOrder,
                    IsActive = model.IsActive,
                    SectionName = model.SectionName
                };
                Section = new GeneralLogic().AddSection(Section);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        // GET: Sections/Edit/5
        public ActionResult Edit(Guid id)
        {
            var dbobject = new GeneralLogic().GetSection(id);
            
            Models.Section model = new Section()
            {
                DisplayOrder = dbobject.DisplayOrder,
                ID = dbobject.ID,
                IsActive = dbobject.IsActive,
                SectionName = dbobject.SectionName
            };
            return View(model);
        }


        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Section model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.Section Section = new DataLayer.Models.Section()
                {
                    ID = model.ID,
                    DisplayOrder = model.DisplayOrder,
                    IsActive = model.IsActive,
                    SectionName = model.SectionName
                };
                Section = new GeneralLogic().UpdateSection(Section);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /*
         * 
        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section Section = db.Sections.Find(id);
            if (Section == null)
            {
                return HttpNotFound();
            }
            return View(Section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section Section = db.Sections.Find(id);
            db.Sections.Remove(Section);
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
