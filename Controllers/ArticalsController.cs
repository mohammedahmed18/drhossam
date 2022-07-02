using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dashboard_HospitalGuide_.Models;

namespace dashboard_HospitalGuide_.Controllers
{
    public class ArticalsController : Controller
    {
        private Hospital_Services_GiudDBEntities db = new Hospital_Services_GiudDBEntities();

        // GET: Articals
        public ActionResult Index()
        {
            var articals = db.Articals.Include(a => a.Admin);
            return View(articals.ToList());
        }

        // GET: Articals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artical artical = db.Articals.Find(id);
            if (artical == null)
            {
                return HttpNotFound();
            }
            return View(artical);
        }

        // GET: Articals/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name");
            return View();
        }

        // POST: Articals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Artical artical , HttpPostedFileBase ImgUpload)
        {

            if (ModelState.IsValid)
            {
                string fileName = ImgUpload.FileName;
                //string fileName =  ""+DateTime.Now.Year+DateTime.Now.Day+DateTime.Now.Month + ".jpg";
                ImgUpload.SaveAs(Server.MapPath("~/ImgUpload/") + fileName);
                string path = Path.Combine(Server.MapPath("~/ImgUpload/"), fileName);
                ImgUpload.SaveAs(path);
                //var d=Directory.CreateDirectory("C://Users//HP//Desktop//Practical_Training_View//Practical_Training_View//ImgUpload//" + fileName);
                artical.Artical_Image = fileName;
                artical.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Articals.Add(artical);
                db.SaveChanges();
                return RedirectToAction("Index");
               
            }
           
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", artical.Admin_ID);
            return View(artical);
        }

        // GET: Articals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artical artical = db.Articals.Find(id);
            if (artical == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", artical.Admin_ID);
            return View(artical);
        }

        // POST: Articals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Artical artical)
        {
           
            if (ModelState.IsValid)
            {
                artical.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Entry(artical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", artical.Admin_ID);
            return View(artical);
        }
     
        // GET: Articals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artical artical = db.Articals.Find(id);
            if (artical == null)
            {
                return HttpNotFound();
            }
            return View(artical);
        }

        // POST: Articals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artical artical = db.Articals.Find(id);
            db.Articals.Remove(artical);
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
