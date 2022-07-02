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
    public class BloodNeedsController : Controller
    {
        private Hospital_Services_GiudDBEntities db = new Hospital_Services_GiudDBEntities();

        // GET: BloodNeeds
        public ActionResult Index()
        {
            var bloodNeeds = db.BloodNeeds.Include(b => b.Blood);
            return View(bloodNeeds.ToList());
        }

        // GET: BloodNeeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodNeed bloodNeed = db.BloodNeeds.Find(id);
            if (bloodNeed == null)
            {
                return HttpNotFound();
            }
            return View(bloodNeed);
        }

        // GET: BloodNeeds/Create
        public ActionResult Create()
        {
            ViewBag.Blood_ID = new SelectList(db.Bloods, "Blood_ID", "Blood1");
            return View();
        }

        // POST: BloodNeeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IsNeeded,Blood_ID")] BloodNeed bloodNeed)
        {
            if (ModelState.IsValid)
            {
                db.BloodNeeds.Add(bloodNeed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Blood_ID = new SelectList(db.Bloods, "Blood_ID", "Blood1", bloodNeed.Blood_ID);
            return View(bloodNeed);
        }

        // GET: BloodNeeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodNeed bloodNeed = db.BloodNeeds.Find(id);
            if (bloodNeed == null)
            {
                return HttpNotFound();
            }
            ViewBag.Blood_ID = new SelectList(db.Bloods, "Blood_ID", "Blood1", bloodNeed.Blood_ID);
            return View(bloodNeed);
        }

        // POST: BloodNeeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IsNeeded,Blood_ID")] BloodNeed bloodNeed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodNeed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Blood_ID = new SelectList(db.Bloods, "Blood_ID", "Blood1", bloodNeed.Blood_ID);
            return View(bloodNeed);
        }

        // GET: BloodNeeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodNeed bloodNeed = db.BloodNeeds.Find(id);
            if (bloodNeed == null)
            {
                return HttpNotFound();
            }
            return View(bloodNeed);
        }

        // POST: BloodNeeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodNeed bloodNeed = db.BloodNeeds.Find(id);
            db.BloodNeeds.Remove(bloodNeed);
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
