using System.Linq;

namespace DefyClinicInfastructure
{
  public class Logic
  {
    readonly DefyClinicContxet db = new DefyClinicContxet();

    public int CountBookings(string email)
    {
      //int count = 0;

      var app = db.Bookings.ToList().FindAll(x => x.PatientDetails.Equals(email));
      return app.Count;
    }

    public bool IsFree(string email)
    {
      return CountBookings(email) >= 4;
    }
  }
}
