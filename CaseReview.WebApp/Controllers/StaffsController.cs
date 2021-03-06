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
    public class StaffsController : Controller
    {

        // GET: Staffs
        public ActionResult Index()
        {
            var data = new StaffLogic().GetAllStaff();
            return View(data);
        }

        
        // GET: Staffs/Details/5
        public ActionResult Details(Guid id)
        {

            var staff = new StaffLogic().GetStaff(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        
        
        // GET: Staffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StaffFirstname,StaffSurname,Email")] Staff model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.Staff staff = new DataLayer.Models.Staff()
                {
                    ID = Guid.NewGuid(),
                    StaffFirstname = model.StaffFirstname,
                    StaffSurname = model.StaffSurname,
                    Email = model.Email
                };
                staff = new StaffLogic().AddStaff(staff);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        
        
        // GET: Staffs/Edit/5
        public ActionResult Edit(Guid id)
        {
            var staff = new StaffLogic().GetStaff(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }


        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StaffFirstname,StaffSurname,Email")] Staff model)
        {
            if (ModelState.IsValid)
            {
                CaseReview.DataLayer.Models.Staff staff = new DataLayer.Models.Staff()
                {
                    ID = model.ID,
                    StaffFirstname = model.StaffFirstname,
                    StaffSurname = model.StaffSurname,
                    Email = model.Email
                };
                staff = new StaffLogic().UpdateStaff(staff);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /*
         * 
        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
