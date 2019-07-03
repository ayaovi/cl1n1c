using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
  public  class VisitRepository
    {
        DefyClinicContxet db = new DefyClinicContxet();
        public void Add(Visit P)
        {
            db.Visits.Add(P);
            db.SaveChanges();
            //  throw new NotImplementedException();
        }

        public void Edit(Visit P)
        {
            db.Entry(P).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //throw new NotImplementedException();
        }

        public Visit FindById(int Id)
        {
            var result = (from x in db.Visits where x.App_Id == Id select x).SingleOrDefault();
            return result;
            // throw new NotImplementedException();
        }

        public IEnumerable<Visit> GetVisits()
        {
            return db.Visits;
            // throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
           Visit P = db.Visits.Find(Id);
            db.Visits.Remove(P);
            db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
