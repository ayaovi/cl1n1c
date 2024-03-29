﻿using System.Net;
using System.Web.Mvc;
using DefyClinicInfastructure;
using DefyClinicModels;

namespace DefyClinicMVC.Controllers
{
  public class DepartmentsController : Controller
  {
    private readonly DepartmentRepository _db = new DepartmentRepository();

    // GET: Departments
    public ActionResult Index()
    {
      return View(_db.GetDepartments());
    }

    // GET: Departments/Details/5
    public ActionResult Details(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var department = _db.FindById(id);
      if (department == null)
      {
        return HttpNotFound();
      }
      return View(department);
    }

    // GET: Departments/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Departments/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "D_ID,D_Name,Manager")] Department department)
    {
      if (ModelState.IsValid)
      {
        _db.Add(department);
        return RedirectToAction("Index");
      }

      return View(department);
    }

    // GET: Departments/Edit/5
    public ActionResult Edit(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Department department = _db.FindById(id);
      if (department == null)
      {
        return HttpNotFound();
      }
      return View(department);
    }

    // POST: Departments/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "D_ID,D_Name,Manager")] Department department)
    {
      if (ModelState.IsValid)
      {
        _db.Edit(department);
        return RedirectToAction("Index");
      }
      return View(department);
    }

    // GET: Departments/Delete/5
    public ActionResult Delete(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var department = _db.FindById(id);
      if (department == null)
      {
        return HttpNotFound();
      }
      return View(department);
    }

    // POST: Departments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id)
    {
      Department department = _db.FindById(id);
      _db.Remove(id);
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        // db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}