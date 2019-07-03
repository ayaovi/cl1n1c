using DefyClinicModels;
using System.Collections.Generic;
using System.Linq;

namespace DefyClinicInfastructure
{
  public class StaffRepository
  {
    readonly DefyClinicContxet _db = new DefyClinicContxet();
    public void Add(Staff staff)
    {
      _db.Staffs.Add(staff);
      _db.SaveChanges();
      //  throw new NotImplementedException();
    }

    public void Edit(Staff staff)
    {
      _db.Entry(staff).State = System.Data.Entity.EntityState.Modified;
      _db.SaveChanges();
      //throw new NotImplementedException();
    }

    public Staff FindById(string id)
    {
      var result = _db.Staffs.SingleOrDefault(x => x.EmpNo == id);
      return result;
      // throw new NotImplementedException();
    }

    public IEnumerable<Staff> GetStaffs()
    {
      return _db.Staffs;
      // throw new NotImplementedException();
    }

    public void Remove(string id)
    {
      var staff = _db.Staffs.Find(id);
      _db.Staffs.Remove(staff);
      _db.SaveChanges();
      //throw new NotImplementedException();
    }
  }
}