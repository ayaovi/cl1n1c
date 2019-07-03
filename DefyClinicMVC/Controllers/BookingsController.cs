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

namespace DefyClinicMVC.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private BookingRepository db = new BookingRepository();
        public ActionResult Confirmed(int? id)
        {
            Booking appointment = db.FindById(Convert.ToInt32(id));
            string search = "";
            search =
            Confirm(id);
            ViewBag.Search = search;
            return View(appointment);
        }
        public ActionResult ConfirmForUser(int? id)
        {
            Booking appointment = db.FindById(Convert.ToInt32(id));
            string search = "";
            search =
            Confirm(id);
            ViewBag.Search = search;
            Booking p = db.GetBookings().Last();
            return View(appointment);
        }

        public ActionResult Declined(int? id)
        {
            Booking appointment = db.FindById(Convert.ToInt32(id));
            string search = "";
            search =
            Decline(id);
            ViewBag.Search = search;
            return View(appointment);
        }
        public ActionResult DeclineForUser(int? id)
        {
            Booking appointment = db.FindById(Convert.ToInt32(id));
            string search = "";
            search =
            Decline(id);
            ViewBag.Search = search;
            return View(appointment);
        }
        public string Confirm(int? BookId)
        {
            Booking a = db.FindById(Convert.ToInt32(BookId));
            a.App_Status = "Confirmed";
            db.Edit(a);
            return "Appointment has been Confirmed";
        }

        public string Decline(int? BookId)
        {
            Booking a = db.FindById(Convert.ToInt32(BookId));
            a.App_Status = "Declined";
            db.Edit(a);
            return "Appointment has been Declined. Please book another appointment";
        }

        public ActionResult ViewStatus()
        {
            string m = HttpContext.User.Identity.Name;
            List<Booking> a = db.GetBookings().ToList().FindAll(p => p.P_Details == m);
            foreach (var item in a)
            {
                if (item.App_Status == "Confirmed")
                {
                    ViewBag.c = "Confirmed";
                }
                if (item.App_Status == "Declined")
                {
                    ViewBag.d = "Declined";
                }
                if (item.App_Status == "Not Yet Confirmed")
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
            Booking booking = db.FindById(Convert.ToInt32(id));
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(string Emp)
        {
            DefyClinicRepository d = new DefyClinicRepository();
            DefyClinicContxet S = new DefyClinicContxet();
            string userId = "";
            ViewData["App_Date"] = d.GetSlots().Where(w => w.Status == true)
                            .AsEnumerable()
                            .Select(i => new SelectListItem
                            {
                                Value = i.Date.ToShortDateString(),
                                Text = i.Date.ToShortDateString()
                            });
           // ViewBag.App_Time = new SelectList(d.GetSlots().Where(X => X.Status == true), "Id", "A_Slot");
            ViewBag.App_Time = d.GetSlots().Where(w => w.Status == true)
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
            if (Emp != null)
            {
                userId = Emp;
            }
            else
            {
                userId = HttpContext.User.Identity.Name;
            }
           

            string P_No = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpNo;
            string P_Title = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Title;
            string P_Name = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpName;
            string P_Surname = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpSurname;
            string P_Gender = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Gender;
            string P_Mobile = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpTel;
            string P_ID = S.Staffs.ToList().Find(X => X.EmpEmail == userId).ID_Pass;
            string Dep = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Department.D_Name;
            ViewBag.P_Name = P_Title + " " + P_Name + " " + P_Surname;
            ViewBag.P_Gender = P_Gender;
            ViewBag.P_No = P_No;
            ViewBag.P_Mobiel = P_Mobile;
            ViewBag.P_ID = P_ID;
            ViewBag.Dep = Dep;
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "App_Id,P_Details,App_Status,App_Date,Dep,App_Time,Reason,isFree,TimeId")] Booking booking, string id, string Emp)
        {

            int result = DateTime.Compare(DateTime.Now, booking.App_Date);
            string errorMessage = "";

            DefyClinicRepository d = new DefyClinicRepository();
            DefyClinicContxet S = new DefyClinicContxet();

            //Appointment newTime = db.Appointments.Find(appointment.TimeId);
            //Appointment newDate = db.Appointments.Find(appointment.BookDate);
            int sTime = db.GetBookings().Where(p => p.TimeId == booking.TimeId).Select(p => p.TimeId).FirstOrDefault();
            DateTime sDate = db.GetBookings().Where(p => p.App_Date == booking.App_Date).Select(p => p.App_Date).FirstOrDefault();
            string userId = "";
            ViewBag.App_Time = d.GetSlots().Where(w => w.Status == true && w.Date.ToString() == id)
                         .AsEnumerable()
                         .Select(i => new SelectListItem
                         {
                             Value = i.A_Slot.ToShortTimeString(),
                             Text = i.A_Slot.ToShortTimeString()
                         });
         //   ViewBag.App_Time = new SelectList(d.GetSlots().Where(X => X.Status == true && X.Date.ToString() == id), "Id", "A_Slot");
            ViewData["App_Date"] = d.GetSlots().Where(w => w.Status == true)
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
            if (Emp != null)
            {
                userId = Emp;
            }
            else
            {
                userId = HttpContext.User.Identity.Name;
            }


            string P_No = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpNo;
            string P_Title = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Title;
            string P_Name = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpName;
            string P_Surname = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpSurname;
            string P_Gender = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Gender;
            string P_Mobile = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpTel;
            string P_ID = S.Staffs.ToList().Find(X => X.EmpEmail == userId).ID_Pass;
            string Dep = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Department.D_Name;
            ViewBag.P_Name = P_Title + " " + P_Name + " " + P_Surname;
            ViewBag.P_Gender = P_Gender;
            ViewBag.P_No = P_No;
            ViewBag.P_Mobiel = P_Mobile;
            ViewBag.P_ID = P_ID;
            ViewBag.Dep = Dep;
            if (ModelState.IsValid)
            {
                Booking b = new Booking
                {
                    P_Details = HttpContext.User.Identity.Name,
                    App_Date = booking.App_Date,
                    TimeId = booking.TimeId,
                   
                    App_Status = "Not Yet Confirmed",

                    App_Id = booking.App_Id,
                    App_Time = booking.App_Time,
                    Reason = booking.Reason,

                    isFree = true

                };

                db.Add(booking);
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {

            Booking book = new Booking();
            string userId = book.P_Details;
            DefyClinicRepository d = new DefyClinicRepository();
            DefyClinicContxet S = new DefyClinicContxet();

            string P_No = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpNo;
            string P_Title = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Title;
            string P_Name = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpName;
            string P_Surname = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpSurname;
            string P_Gender = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Gender;
            string P_Mobile = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpTel;
            string P_ID = S.Staffs.ToList().Find(X => X.EmpEmail == userId).ID_Pass;
            string Dep = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Department.D_Name;
            ViewBag.P_Name = P_Title + " " + P_Name + " " + P_Surname;
            ViewBag.P_Gender = P_Gender;
            ViewBag.P_No = P_No;
            ViewBag.P_Mobiel = P_Mobile;
            ViewBag.P_ID = P_ID;
            ViewBag.Dep = Dep;
            ViewBag.App_Time = new SelectList(d.GetSlots(), "BS_Id", "StartTime");
            ViewData["App_Date"] = new SelectList(d.GetSlots(), "Id", "Date");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.FindById(Convert.ToInt32(id));
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
            int result = DateTime.Compare(DateTime.Now, booking.App_Date);
            string errorMessage = "";

            DefyClinicRepository d = new DefyClinicRepository();
            DefyClinicContxet S = new DefyClinicContxet();

            //Appointment newTime = db.Appointments.Find(appointment.TimeId);
            //Appointment newDate = db.Appointments.Find(appointment.BookDate);
            int sTime = db.GetBookings().Where(p => p.TimeId == booking.TimeId).Select(p => p.TimeId).FirstOrDefault();
            DateTime sDate = db.GetBookings().Where(p => p.App_Date == booking.App_Date).Select(p => p.App_Date).FirstOrDefault();
            string userId = "";
            //  ViewBag.App_Time = new SelectList(d.GetSlots().Where(X => X.Date.ToString() == id), "Id", "A_Slot");
            ViewBag.App_Time = new SelectList(d.GetSlots(), "BS_Id", "StartTime");
            ViewData["App_Date"] = new SelectList(d.GetSlots(), "Id", "Date");
            string P_No = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpNo;
            string P_Title = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Title;
            string P_Name = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpName;
            string P_Surname = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpSurname;
            string P_Gender = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Gender;
            string P_Mobile = S.Staffs.ToList().Find(X => X.EmpEmail == userId).EmpTel;
            string P_ID = S.Staffs.ToList().Find(X => X.EmpEmail == userId).ID_Pass;
            string Dep = S.Staffs.ToList().Find(X => X.EmpEmail == userId).Department.D_Name;
            ViewBag.P_Name = P_Title + " " + P_Name + " " + P_Surname;
            ViewBag.P_Gender = P_Gender;
            ViewBag.P_No = P_No;
            ViewBag.P_Mobiel = P_Mobile;
            ViewBag.P_ID = P_ID;
            ViewBag.Dep = Dep;
            if (ModelState.IsValid)
            {

                Booking b = new Booking
                {
                    P_Details = HttpContext.User.Identity.Name,
                    App_Date = booking.App_Date,
                    TimeId = booking.TimeId,

                    App_Status = "Not Yet Confirmed",

                    App_Id = booking.App_Id,
                    App_Time = booking.App_Time,
                    Reason = booking.Reason,

                    isFree = true

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
            Booking booking = db.FindById(Convert.ToInt32(id));
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
            Booking booking = db.FindById(Convert.ToInt32(id));
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
