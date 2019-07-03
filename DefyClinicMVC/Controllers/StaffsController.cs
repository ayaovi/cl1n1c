using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DefyClinicInfastructure;
using DefyClinicModels;
using DefyClinicMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DefyClinicMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffsController : Controller
    {
        private DefyClinicContxet db = new DefyClinicContxet();

        // GET: Staffs
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Branch).Include(s => s.Department);
            return View(staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(string id)
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

        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.BranchCode = new SelectList(db.Branchs, "BranchCode", "BranchName");
            ViewBag.D_ID = new SelectList(db.Departments, "D_ID", "D_Name");
           ViewBag.Roles = db.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpNo,Title,EmpName,EmpSurname,ID_Pass,Gender,EmpTel,EmpEmail,EmpAddress,D_ID,HireDate,P_ID,Status,Role,Picture,BranchCode")] Staff staff, HttpPostedFileBase img_upload)
        {
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            staff.Picture = data;
           
            if (ModelState.IsValid)
            {
                //ApplicationUser user = db.Users.Where(u => u.UserName.Equals(staff.EmpEmail, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                //userManager.AddToRole(user.Id, staff.EmpEmail);
                //ApplicationUser user = db.Users.Where(u => u.UserName.Equals(staff.EmpEmail, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                //userManager.AddToRole(user.Id, rolname);
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchCode = new SelectList(db.Branchs, "BranchCode", "BranchName", staff.BranchCode);
            ViewBag.D_ID = new SelectList(db.Departments, "D_ID", "D_Name", staff.D_ID);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.BranchCode = new SelectList(db.Branchs, "BranchCode", "BranchName", staff.BranchCode);
            ViewBag.D_ID = new SelectList(db.Departments, "D_ID", "D_Name", staff.D_ID);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpNo,Title,EmpName,EmpSurname,ID_Pass,Gender,EmpTel,EmpEmail,EmpAddress,D_ID,HireDate,P_ID,Status,Role,Picture,BranchCode")] Staff staff, HttpPostedFileBase img_upload)
        {

            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            staff.Picture = data;
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchCode = new SelectList(db.Branchs, "BranchCode", "BranchName", staff.BranchCode);
            ViewBag.Roles = db.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.D_ID = new SelectList(db.Departments, "D_ID", "D_Name", staff.D_ID);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
         
        public ActionResult Doctors()
        {
            List<Staff> a = db.Staffs.ToList().FindAll(p => p.Role == "Doctor");
            return View(a);
        }
        public ActionResult Nurses()
        {
            List<Staff> a = db.Staffs.ToList().FindAll(p => p.Role == "Nurse");
            return View(a);
        }
        public ActionResult Recetionist()
        {
            List<Staff> a = db.Staffs.ToList().FindAll(p => p.Role == "Recetionist");
            return View(a);
        }
    }
}
