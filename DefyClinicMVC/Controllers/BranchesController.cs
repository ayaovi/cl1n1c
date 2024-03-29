﻿using System.Net;
using System.Web.Mvc;
using DefyClinicInfastructure;
using DefyClinicModels;

namespace DefyClinicMVC.Controllers
{
  public class BranchesController : Controller
  {
    private readonly BranchRepository _db = new BranchRepository();
    
    // GET: Branches
    public ActionResult Index()
    {
      return View(_db.GetBranches());
    }

    // GET: Branches/Details/5
    public ActionResult Details(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var branch = _db.FindById(id);
      if (branch == null)
      {
        return HttpNotFound();
      }
      return View(branch);
    }

    // GET: Branches/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Branches/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "BranchCode,BranchName,BranchLocation")] Branch branch)
    {
      if (ModelState.IsValid)
      {
        _db.Add(branch);

        return RedirectToAction("Index");
      }

      return View(branch);
    }

    // GET: Branches/Edit/5
    public ActionResult Edit(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var branch = _db.FindById(id);
      if (branch == null)
      {
        return HttpNotFound();
      }
      return View(branch);
    }

    // POST: Branches/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "BranchCode,BranchName,BranchLocation")] Branch branch)
    {
      if (ModelState.IsValid)
      {
        _db.Edit(branch);
        return RedirectToAction("Index");
      }
      return View(branch);
    }

    // GET: Branches/Delete/5
    public ActionResult Delete(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var branch = _db.FindById(id);
      if (branch == null)
      {
        return HttpNotFound();
      }
      return View(branch);
    }

    // POST: Branches/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id)
    {
      var branch = _db.FindById(id);
      _db.Remove(id);
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        //  db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}