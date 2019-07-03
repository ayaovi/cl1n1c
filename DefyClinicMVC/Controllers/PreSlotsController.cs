using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DefyClinicCore.App;
using DefyClinicInfastructure;
using DefyClinicModels;

namespace DefyClinicMVC.Controllers
{
    public class PreSlotsController : Controller
    {
        private DefyClinicRepository db = new DefyClinicRepository();

        // GET: PreSlots
        public ActionResult Index()
        {
            return View(db.GetSlots());
        }
        [HttpPost]
        public JsonResult Index(int id, bool isPublicPost)
        {


            var result = db.FindById(Convert.ToInt32(id));

            if (result != null)
            {
                result.Status = isPublicPost;

                db.Edit(result);
            }

            return Json(true);
        }
        // GET: PreSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreSlot preSlot = db.FindById(Convert.ToInt32(id));
            if (preSlot == null)
            {
                return HttpNotFound();
            }
            return View(preSlot);
        }

        // GET: PreSlots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartTime,EndTime,Minutes,A_Slot,Date,Status")] PreSlot preSlot)
        {
            if (ModelState.IsValid)
            {
                int num = 0;
                while (preSlot.A_Slot < preSlot.EndTime)
                {
                   
                    preSlot.A_Slot = preSlot.StartTime.AddMinutes(preSlot.Minutes * num++).ToLocalTime();
                   
                    db.Add(preSlot);
                }
                return RedirectToAction("Index");
            }

            return View(preSlot);
        }

        // GET: PreSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreSlot preSlot =  db.FindById(Convert.ToInt32(id));
            if (preSlot == null)
            {
                return HttpNotFound();
            }
            return View(preSlot);
        }

        // POST: PreSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartTime,EndTime,Minutes,A_Slot,Date,Status")] PreSlot preSlot)
        {
            if (ModelState.IsValid)
            {
                db.Edit(preSlot);
                return RedirectToAction("Index");
            }
            return View(preSlot);
        }

        // GET: PreSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreSlot preSlot = db.FindById(Convert.ToInt32(id)); 
            if (preSlot == null)
            {
                return HttpNotFound();
            }
            return View(preSlot);
        }

        // POST: PreSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PreSlot preSlot = db.FindById(Convert.ToInt32(id));
            db.Remove(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            //    db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
