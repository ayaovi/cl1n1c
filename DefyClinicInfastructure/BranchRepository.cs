using DefyClinicModels;
using System.Collections.Generic;
using System.Linq;

namespace DefyClinicInfastructure
{
  public class BranchRepository
  {
    readonly DefyClinicContxet _db = new DefyClinicContxet();
    public void Add(Branch patient)
    {
      _db.Branchs.Add(patient);
      _db.SaveChanges();
      //  throw new NotImplementedException();
    }

    public void Edit(Branch patient)
    {
      _db.Entry(patient).State = System.Data.Entity.EntityState.Modified;
      _db.SaveChanges();
      //throw new NotImplementedException();
    }

    public Branch FindById(string id)
    {
      var result = _db.Branchs.SingleOrDefault(x => x.BranchCode == id);
      return result;
      // throw new NotImplementedException();
    }

    public IEnumerable<Branch> GetBranches()
    {
      return _db.Branchs;
      // throw new NotImplementedException();
    }

    public void Remove(string id)
    {
      var patient = _db.Branchs.Find(id);
      _db.Branchs.Remove(patient);
      _db.SaveChanges();
      //throw new NotImplementedException();
    }
  }
}