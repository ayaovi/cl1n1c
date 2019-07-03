using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
  public  class BookingRepository
    {
        DefyClinicContxet db = new DefyClinicContxet();
        public void Add(Booking P)
        {
            db.Bookings.Add(P);
            db.SaveChanges();
            //  throw new NotImplementedException();
        }

        public void Edit(Booking P)
        {
            db.Entry(P).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //throw new NotImplementedException();
        }

        public Booking FindById(int Id)
        {
            var result = (from x in db.Bookings where x.App_Id == Id select x).SingleOrDefault();
            return result;
            // throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetBookings()
        {
            return db.Bookings;
            // throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
           Booking P = db.Bookings.Find(Id);
            db.Bookings.Remove(P);
            db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
