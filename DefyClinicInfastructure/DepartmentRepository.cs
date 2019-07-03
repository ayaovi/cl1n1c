using DefyClinicModels;
using System.Collections.Generic;
using System.Linq;

namespace DefyClinicInfastructure
{
  public class DepartmentRepository
  {
    readonly DefyClinicContxet _db = new DefyClinicContxet();
    public void Add(Department department)
    {
      _db.Departments.Add(department);
      _db.SaveChanges();
      //  throw new NotImplementedException();
    }

    public void Edit(Department P)
    {
      _db.Entry(P).State = System.Data.Entity.EntityState.Modified;
      _db.SaveChanges();
      //throw new NotImplementedException();
    }

    public Department FindById(string id)
    {
      var result = _db.Departments.SingleOrDefault(x => x.D_ID == id);
      return result;
      // throw new NotImplementedException();
    }

    public IEnumerable<Department> GetDepartments()
    {
      return _db.Departments;
      // throw new NotImplementedException();
    }

    public void Remove(string id)
    {
      var department = _db.Departments.Find(id);
      _db.Departments.Remove(department);
      _db.SaveChanges();
      //throw new NotImplementedException();
    }
  }
}