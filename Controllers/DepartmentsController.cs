using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using dashboard_HospitalGuide_.Models;

namespace dashboard_HospitalGuide_.Controllers
{
    public class DepartmentsController : Controller
    {
        private Hospital_Services_GiudDBEntities db = new Hospital_Services_GiudDBEntities();

        // GET: Departments
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.Admin);
            return View(departments.ToList());
        }
      
        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dept_ID,dept_Name,dept_NumberOfemptybed,dept_NumberOfBed,dept_Info,dept_Descripation,dept_NumberOfEmp,Admin_ID")] Department department)
        {
            if (ModelState.IsValid)
            {
                //Admin admin = db.Admins.FirstOrDefault(u => u.Admin_ID == int.Parse(Session["ID"].ToString()));
                department.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", department.Admin_ID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", department.Admin_ID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dept_ID,dept_Name,dept_NumberOfemptybed,dept_NumberOfBed,dept_Info,dept_Descripation,dept_NumberOfEmp,Admin_ID")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.Admin_ID = int.Parse(Session["ID"].ToString());
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", department.Admin_ID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        public string OpenModelPopup()
        {
            //can send some data also.  
            return "<h1>This is Modal Popup Window</h1>";
        }
        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
