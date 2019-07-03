using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
   public class Logic
    {
        DefyClinicContxet db = new DefyClinicContxet();

        public int countBookings(string email)
        {
            //int count = 0;

            List<Booking> app = db.Bookings.ToList().FindAll(x => x.P_Details.Equals(email));
            return app.Count;
        }

        public bool isFree(string email)
        {
            bool free = false;

            if (countBookings(email) >= 4)
            {
                free = true;
            }

            return free;
        }
    }
}
