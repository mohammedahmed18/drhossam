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
    public class NewssesController : Controller
    {
        private Hospital_Services_GiudDBEntities db = new Hospital_Services_GiudDBEntities();

        // GET: Newsses
        public ActionResult Index()
        {
            var newsses = db.Newsses.Include(n => n.Admin);
            return View(newsses.ToList());
        }

        // GET: Newsses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newss newss = db.Newsses.Find(id);
            if (newss == null)
            {
                return HttpNotFound();
            }
            return View(newss);
        }

        // GET: Newsses/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name");
            return View();
        }

        // POST: Newsses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Newss newss, HttpPostedFileBase ImgUpload)
        {
            if (ModelState.IsValid)
            {
                string fileName = ImgUpload.FileName;
                //string fileName =  ""+DateTime.Now.Year+DateTime.Now.Day+DateTime.Now.Month + ".jpg";
                ImgUpload.SaveAs(Server.MapPath("~/ImgUpload/") + fileName);
                newss.News_Image = fileName;
                newss.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Newsses.Add(newss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", newss.Admin_ID);
            return View(newss);
        }

        // GET: Newsses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newss newss = db.Newsses.Find(id);
            if (newss == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", newss.Admin_ID);
            return View(newss);
        }

        // POST: Newsses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "News_ID,News_Content,News_Image,News_Date,Admin_ID")] Newss newss)
        {
            if (ModelState.IsValid)
            {
                newss.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Entry(newss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", newss.Admin_ID);
            return View(newss);
        }

        // GET: Newsses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newss newss = db.Newsses.Find(id);
            if (newss == null)
            {
                return HttpNotFound();
            }
            return View(newss);
        }

        // POST: Newsses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newss newss = db.Newsses.Find(id);
            db.Newsses.Remove(newss);
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
