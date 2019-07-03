using DefyClinicModels;
using System.Collections.Generic;
using System.Linq;

namespace DefyClinicInfastructure
{
  public class VisitRepository
  {
    readonly DefyClinicContxet _db = new DefyClinicContxet();
    public void Add(Visit visit)
    {
      _db.Visits.Add(visit);
      _db.SaveChanges();
      //  throw new NotImplementedException();
    }

    public void Edit(Visit visit)
    {
      _db.Entry(visit).State = System.Data.Entity.EntityState.Modified;
      _db.SaveChanges();
      //throw new NotImplementedException();
    }

    public Visit FindById(int id)
    {
      var result = _db.Visits.SingleOrDefault(x => x.App_Id == id);
      return result;
      // throw new NotImplementedException();
    }

    public IEnumerable<Visit> GetVisits()
    {
      return _db.Visits;
      // throw new NotImplementedException();
    }

    public void Remove(int id)
    {
      var visit = _db.Visits.Find(id);
      _db.Visits.Remove(visit);
      _db.SaveChanges();
      //throw new NotImplementedException();
    }
  }
}