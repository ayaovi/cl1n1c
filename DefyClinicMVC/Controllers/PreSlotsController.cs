using System;
using System.Net;
using System.Web.Mvc;
using DefyClinicInfastructure;
using DefyClinicModels;

namespace DefyClinicMVC.Controllers
{
  public class PreSlotsController : Controller
  {
    private readonly DefyClinicRepository _db = new DefyClinicRepository();

    // GET: PreSlots
    public ActionResult Index()
    {
      return View(_db.GetSlots());
    }

    [HttpPost]
    public JsonResult Index(int id, bool isPublicPost)
    {
      var result = _db.FindById(Convert.ToInt32(id));

      if (result != null)
      {
        result.Status = isPublicPost;
        _db.Edit(result);
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
      var preSlot = _db.FindById(Convert.ToInt32(id));
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
        var num = 0;
        while (preSlot.A_Slot < preSlot.EndTime)
        {
          preSlot.A_Slot = preSlot.StartTime.AddMinutes(preSlot.Minutes * num++).ToLocalTime();

          _db.Add(preSlot);
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
      var preSlot = _db.FindById(Convert.ToInt32(id));
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
        _db.Edit(preSlot);
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
      var preSlot = _db.FindById(Convert.ToInt32(id));
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
      var preSlot = _db.FindById(Convert.ToInt32(id));
      _db.Remove(id);
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