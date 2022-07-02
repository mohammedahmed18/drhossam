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
    public class patients1Controller : Controller
    {
        private Hospital_Services_GiudDBEntities db = new Hospital_Services_GiudDBEntities();

        // GET: patients1
        public ActionResult Index()
        {
            var patients = db.patients.Include(p => p.Admin).Include(p => p.Care_Units).Include(p => p.Department);
            return View(patients.ToList());
        }

        // GET: patients1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: patients1/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name");
            ViewBag.Care_ID = new SelectList(db.Care_Units, "Care_ID", "Care_Name");
            ViewBag.dept_ID = new SelectList(db.Departments, "dept_ID", "dept_Name");
            return View();
        }

        // POST: patients1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Patient_ID,Patient_Name,Patient_DateOfBirth,Patient_PhoneNumber,Patient_Gender,Patient_NationalID,Patient_Address,Admin_ID,dept_ID,Care_ID")] patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Admin_ID = int.Parse(Session["ID"].ToString());
                db.patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", patient.Admin_ID);
            ViewBag.Care_ID = new SelectList(db.Care_Units, "Care_ID", "Care_Name", patient.Care_ID);
            ViewBag.dept_ID = new SelectList(db.Departments, "dept_ID", "dept_Name", patient.dept_ID);            
            return View(patient);
        }

        // GET: patients1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", patient.Admin_ID);
            ViewBag.Care_ID = new SelectList(db.Care_Units, "Care_ID", "Care_Name", patient.Care_ID);
            ViewBag.dept_ID = new SelectList(db.Departments, "dept_ID", "dept_Name", patient.dept_ID);
            return View(patient);
        }

        // POST: patients1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Patient_ID,Patient_Name,Patient_DateOfBirth,Patient_PhoneNumber,Patient_Gender,Patient_NationalID,Patient_Address,Admin_ID,dept_ID,Care_ID")] patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", patient.Admin_ID);
            ViewBag.Care_ID = new SelectList(db.Care_Units, "Care_ID", "Care_Name", patient.Care_ID);
            ViewBag.dept_ID = new SelectList(db.Departments, "dept_ID", "dept_Name", patient.dept_ID);
            return View(patient);
        }

        // GET: patients1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: patients1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            patient patient = db.patients.Find(id);
            db.patients.Remove(patient);
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
