using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dashboard_HospitalGuide_.Models;

namespace dashboard_HospitalGuide_.Controllers
{
    public class Care_UnitsController : Controller
    {
        private Hospital_Services_GiudDBEntities db = new Hospital_Services_GiudDBEntities();

        // GET: Care_Units
        public ActionResult Index()
        {
            var care_Units = db.Care_Units.Include(c => c.Admin);
            return View(care_Units.ToList());
        }

        // GET: Care_Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Care_Units care_Units = db.Care_Units.Find(id);
            if (care_Units == null)
            {
                return HttpNotFound();
            }
            return View(care_Units);
        }

        // GET: Care_Units/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name");
            return View();
        }

        // POST: Care_Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Care_ID,Care_Name,Care_NumberOfemptybed,Care_NumberOfbed,Care_Info,Care_Description,Care_Numberofemployees,Admin_ID")] Care_Units care_Units)
        {
            if (ModelState.IsValid)
            {
                care_Units.Admin_ID = int.Parse(Session["ID"].ToString());

                db.Care_Units.Add(care_Units);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", care_Units.Admin_ID);
            return View(care_Units);
        }

        // GET: Care_Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Care_Units care_Units = db.Care_Units.Find(id);
            if (care_Units == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", care_Units.Admin_ID);
            return View(care_Units);
        }

        // POST: Care_Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Care_ID,Care_Name,Care_NumberOfemptybed,Care_NumberOfbed,Care_Info,Care_Description,Care_Numberofemployees,Admin_ID")] Care_Units care_Units)
        {
            if (ModelState.IsValid)
            {
                care_Units.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Entry(care_Units).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", care_Units.Admin_ID);
            return View(care_Units);
        }

        // GET: Care_Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Care_Units care_Units = db.Care_Units.Find(id);
            if (care_Units == null)
            {
                return HttpNotFound();
            }
            return View(care_Units);
        }

        // POST: Care_Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Care_Units care_Units = db.Care_Units.Find(id);
            db.Care_Units.Remove(care_Units);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
