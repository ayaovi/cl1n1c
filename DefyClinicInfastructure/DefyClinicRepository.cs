using DefyClinicCore.App;
using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
   public class DefyClinicRepository
    {
        DefyClinicContxet db = new DefyClinicContxet();
        public void Add(PreSlot P)
        {
            db.Slots.Add(P);
            db.SaveChanges();
            //  throw new NotImplementedException();
        }

        public void Edit(PreSlot P)
        {
            db.Entry(P).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //throw new NotImplementedException();
        }

        public PreSlot FindById(int Id)
        {
            var result = (from x in db.Slots where x.Id == Id select x).SingleOrDefault();
            return result;
            // throw new NotImplementedException();
        }

        public IEnumerable<PreSlot> GetSlots()
        {
            return db.Slots;
            // throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            PreSlot P = db.Slots.Find(Id);
            db.Slots.Remove(P);
            db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
