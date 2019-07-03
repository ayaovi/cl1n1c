using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DefyClinicInfastructure;
using DefyClinicModels;

namespace DefyClinicMVC.Controllers
{
  [Authorize]
  public class BookingsController : Controller
  {
    private BookingRepository db = new BookingRepository();
    public ActionResult Confirmed(int? id)
    {
      var appointment = db.FindById(Convert.ToInt32(id));
      var search = Confirm(id);
      ViewBag.Search = search;
      return View(appointment);
    }
    public ActionResult ConfirmForUser(int? id)
    {
      var appointment = db.FindById(Convert.ToInt32(id));
      var search = Confirm(id);
      ViewBag.Search = search;
      var booking = db.GetBookings().Last();
      return View(appointment);
    }

    public ActionResult Declined(int? id)
    {
      var appointment = db.FindById(Convert.ToInt32(id));
      var search = Decline(id);
      ViewBag.Search = search;
      return View(appointment);
    }
    public ActionResult DeclineForUser(int? id)
    {
      var appointment = db.FindById(Convert.ToInt32(id));
      var search = Decline(id);
      ViewBag.Search = search;
      return View(appointment);
    }
    public string Confirm(int? BookId)
    {
      var a = db.FindById(Convert.ToInt32(BookId));
      a.AppStatus = "Confirmed";
      db.Edit(a);
      return "Appointment has been Confirmed";
    }

    public string Decline(int? BookId)
    {
      var a = db.FindById(Convert.ToInt32(BookId));
      a.AppStatus = "Declined";
      db.Edit(a);
      return "Appointment has been Declined. Please book another appointment";
    }

    public ActionResult ViewStatus()
    {
      var m = HttpContext.User.Identity.Name;
      var a = db.GetBookings().ToList().FindAll(p => p.PatientDetails == m);
      foreach (var item in a)
      {
        if (item.AppStatus == "Confirmed")
        {
          ViewBag.c = "Confirmed";
        }
        if (item.AppStatus == "Declined")
        {
          ViewBag.d = "Declined";
        }
        if (item.AppStatus == "Not Yet Confirmed")
        {
          ViewBag.n = "Not Yet Confirmed";
        }
      }
      return View(a);

    }
    // GET: Bookings
    public ActionResult Index()
    {

      return View(db.GetBookings());
    }

    // GET: Bookings/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var booking = db.FindById(Convert.ToInt32(id));
      if (booking == null)
      {
        return HttpNotFound();
      }
      return View(booking);
    }

    // GET: Bookings/Create
    public ActionResult Create(string Emp)
    {
      var d = new DefyClinicRepository();
      var contxet = new DefyClinicContxet();
      ViewData["App_Date"] = d.GetSlots().Where(w => w.Status)
                      .AsEnumerable()
                      .Select(i => new SelectListItem
                      {
                        Value = i.Date.ToShortDateString(),
                        Text = i.Date.ToShortDateString()
                      });
      // ViewBag.App_Time = new SelectList(d.GetSlots().Where(X => X.Status == true), "Id", "A_Slot");
      ViewBag.App_Time = d.GetSlots().Where(w => w.Status)
                     .AsEnumerable()
                     .Select(i => new SelectListItem
                     {
                       Value = i.A_Slot.ToShortTimeString(),
                       Text = i.A_Slot.ToShortTimeString()
                     });
      // ViewBag.App_Time = d.GetSlots().Where(X => X.Status == true).SingleOrDefault().A_Slot.ToShortTimeString();
      // ViewData["App_Date"] = new SelectList(d.GetSlots().Where(X => X.Status == true), "Id", "Date");
      // ViewData["App_Date"] = d.GetSlots().Where(X => X.Status == true).SingleOrDefault().Date.ToShortDateString();
      //  ViewBag.App_Time = new SelectList(db.BookingSlots.Where(X => X.Date.ToString() == id), "BS_Id", "StartTime");
      var userId = Emp ?? HttpContext.User.Identity.Name;


      var pNo = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpNo;
      var pTitle = contxet.Staffs.Single(x => x.EmpEmail == userId).Title;
      var pName = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpName;
      var pSurname = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpSurname;
      var pGender = contxet.Staffs.Single(x => x.EmpEmail == userId).Gender;
      var pMobile = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpTel;
      var pId = contxet.Staffs.Single(x => x.EmpEmail == userId).ID_Pass;
      var dep = contxet.Staffs.Single(x => x.EmpEmail == userId).Department.D_Name;
      ViewBag.P_Name = $"{pTitle} {pName} {pSurname}";
      ViewBag.P_Gender = pGender;
      ViewBag.P_No = pNo;
      ViewBag.P_Mobiel = pMobile;
      ViewBag.P_ID = pId;
      ViewBag.Dep = dep;
      return View();
    }

    // POST: Bookings/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "App_Id,P_Details,App_Status,App_Date,Dep,App_Time,Reason,isFree,TimeId")] Booking booking, string id, string Emp)
    {
      var result = DateTime.Compare(DateTime.Now, booking.AppDate);
      var errorMessage = "";

      var repository = new DefyClinicRepository();
      var contxet = new DefyClinicContxet();

      //Appointment newTime = db.Appointments.Find(appointment.TimeId);
      //Appointment newDate = db.Appointments.Find(appointment.BookDate);
      var sTime = db.GetBookings().FirstOrDefault(p => p.TimeId == booking.TimeId)?.TimeId;
      var sDate = db.GetBookings().FirstOrDefault(p => p.AppDate == booking.AppDate)?.AppDate;
      var userId = "";
      ViewBag.App_Time = repository.GetSlots()
                                   .Where(w => w.Status && w.Date.ToString() == id)
                                   .AsEnumerable()
                                   .Select(i => new SelectListItem
                                   {
                                     Value = i.A_Slot.ToShortTimeString(),
                                     Text = i.A_Slot.ToShortTimeString()
                                   });
      //   ViewBag.App_Time = new SelectList(d.GetSlots().Where(X => X.Status == true && X.Date.ToString() == id), "Id", "A_Slot");
      ViewData["App_Date"] = repository.GetSlots()
                                       .Where(w => w.Status)
                                       .AsEnumerable()
                                       .Select(i => new SelectListItem
                                       {
                                         Value = i.Date.ToShortDateString(),
                                         Text = i.Date.ToShortDateString()
                                       });
      // ViewBag.App_Time = d.GetSlots().Where(X => X.Status == true).SingleOrDefault().A_Slot.ToShortTimeString();
      //   ViewData["App_Date"] = new SelectList(d.GetSlots().Where(X => X.Status == true), "Id", "Date");
      // ViewData["App_Date"] = d.GetSlots().Where(X => X.Status == true).SingleOrDefault().Date.ToShortDateString();
      //  ViewBag.App_Time = new SelectList(db.BookingSlots.Where(X => X.Date.ToString() == id), "BS_Id", "StartTime");
      userId = Emp ?? HttpContext.User.Identity.Name;


      var pNo = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpNo;
      var pTitle = contxet.Staffs.Single(x => x.EmpEmail == userId).Title;
      var pName = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpName;
      var pSurname = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpSurname;
      var pGender = contxet.Staffs.Single(x => x.EmpEmail == userId).Gender;
      var pMobile = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpTel;
      var pId = contxet.Staffs.Single(x => x.EmpEmail == userId).ID_Pass;
      var dep = contxet.Staffs.Single(x => x.EmpEmail == userId).Department.D_Name;
      ViewBag.P_Name = $"{pTitle} {pName} {pSurname}";
      ViewBag.P_Gender = pGender;
      ViewBag.P_No = pNo;
      ViewBag.P_Mobiel = pMobile;
      ViewBag.P_ID = pId;
      ViewBag.Dep = dep;
      if (ModelState.IsValid)
      {
        var b = new Booking
        {
          PatientDetails = HttpContext.User.Identity.Name,
          AppDate = booking.AppDate,
          TimeId = booking.TimeId,

          AppStatus = "Not Yet Confirmed",

          AppId = booking.AppId,
          AppTime = booking.AppTime,
          Reason = booking.Reason,

          IsFree = true

        };

        db.Add(booking);
        return RedirectToAction("Index");
      }

      return View(booking);
    }

    // GET: Bookings/Edit/5
    public ActionResult Edit(int? id)
    {

      var book = new Booking();
      var userId = book.PatientDetails;
      var repository = new DefyClinicRepository();
      var contxet = new DefyClinicContxet();

      var pNo = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpNo;
      var pTitle = contxet.Staffs.Single(x => x.EmpEmail == userId).Title;
      var pName = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpName;
      var pSurname = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpSurname;
      var pGender = contxet.Staffs.Single(x => x.EmpEmail == userId).Gender;
      var pMobile = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpTel;
      var pId = contxet.Staffs.Single(x => x.EmpEmail == userId).ID_Pass;
      var dep = contxet.Staffs.Single(x => x.EmpEmail == userId).Department.D_Name;
      ViewBag.P_Name = $"{pTitle} {pName} {pSurname}";
      ViewBag.P_Gender = pGender;
      ViewBag.P_No = pNo;
      ViewBag.P_Mobiel = pMobile;
      ViewBag.P_ID = pId;
      ViewBag.Dep = dep;
      ViewBag.App_Time = new SelectList(repository.GetSlots(), "BS_Id", "StartTime");
      ViewData["App_Date"] = new SelectList(repository.GetSlots(), "Id", "Date");
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var booking = db.FindById(Convert.ToInt32(id));
      if (booking == null)
      {
        return HttpNotFound();
      }
      return View(booking);
    }

    // POST: Bookings/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "App_Id,P_Details,App_Status,App_Date,Dep,App_Time,Reason,isFree,TimeId")] Booking booking)
    {
      var result = DateTime.Compare(DateTime.Now, booking.AppDate);
      var errorMessage = "";

      var repository = new DefyClinicRepository();
      var contxet = new DefyClinicContxet();

      //Appointment newTime = db.Appointments.Find(appointment.TimeId);
      //Appointment newDate = db.Appointments.Find(appointment.BookDate);
      var sTime = db.GetBookings().Where(p => p.TimeId == booking.TimeId).Select(p => p.TimeId).FirstOrDefault();
      var sDate = db.GetBookings().Where(p => p.AppDate == booking.AppDate).Select(p => p.AppDate).FirstOrDefault();
      var userId = "";
      //  ViewBag.App_Time = new SelectList(d.GetSlots().Where(X => X.Date.ToString() == id), "Id", "A_Slot");
      ViewBag.App_Time = new SelectList(repository.GetSlots(), "BS_Id", "StartTime");
      ViewData["App_Date"] = new SelectList(repository.GetSlots(), "Id", "Date");
      var pNo = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpNo;
      var pTitle = contxet.Staffs.Single(x => x.EmpEmail == userId).Title;
      var pName = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpName;
      var pSurname = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpSurname;
      var pGender = contxet.Staffs.Single(x => x.EmpEmail == userId).Gender;
      var pMobile = contxet.Staffs.Single(x => x.EmpEmail == userId).EmpTel;
      var pId = contxet.Staffs.Single(x => x.EmpEmail == userId).ID_Pass;
      var dep = contxet.Staffs.Single(x => x.EmpEmail == userId).Department.D_Name;
      ViewBag.P_Name = $"{pTitle} {pName} {pSurname}";
      ViewBag.P_Gender = pGender;
      ViewBag.P_No = pNo;
      ViewBag.P_Mobiel = pMobile;
      ViewBag.P_ID = pId;
      ViewBag.Dep = dep;
      if (ModelState.IsValid)
      {
        var b = new Booking
        {
          PatientDetails = HttpContext.User.Identity.Name,
          AppDate = booking.AppDate,
          TimeId = booking.TimeId,

          AppStatus = "Not Yet Confirmed",

          AppId = booking.AppId,
          AppTime = booking.AppTime,
          Reason = booking.Reason,

          IsFree = true

        };
        db.Edit(booking);
        return RedirectToAction("Index");
      }
      return View(booking);
    }

    // GET: Bookings/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var booking = db.FindById(Convert.ToInt32(id));
      if (booking == null)
      {
        return HttpNotFound();
      }
      return View(booking);
    }

    // POST: Bookings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      var booking = db.FindById(Convert.ToInt32(id));
      db.Remove(id);
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