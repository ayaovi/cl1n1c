using DefyClinicModels;
using System.Collections.Generic;
using System.Linq;

namespace DefyClinicInfastructure
{
  public class BookingRepository
  {
    readonly DefyClinicContxet _db = new DefyClinicContxet();
    public void Add(Booking patient)
    {
      _db.Bookings.Add(patient);
      _db.SaveChanges();
      //  throw new NotImplementedException();
    }

    public void Edit(Booking P)
    {
      _db.Entry(P).State = System.Data.Entity.EntityState.Modified;
      _db.SaveChanges();
      //throw new NotImplementedException();
    }

    public Booking FindById(int id)
    {
      var result = _db.Bookings.SingleOrDefault(x => x.AppId == id);
      return result;
      // throw new NotImplementedException();
    }

    public IEnumerable<Booking> GetBookings()
    {
      return _db.Bookings;
      // throw new NotImplementedException();
    }

    public void Remove(int id)
    {
      var patient = _db.Bookings.Find(id);
      _db.Bookings.Remove(patient);
      _db.SaveChanges();
      //throw new NotImplementedException();
    }
  }
}
