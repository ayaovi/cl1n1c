using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
   public class DepartmentRepository
    {
        DefyClinicContxet db = new DefyClinicContxet();
        public void Add(Department P)
        {
            db.Departments.Add(P);
            db.SaveChanges();
            //  throw new NotImplementedException();
        }

        public void Edit(Department P)
        {
            db.Entry(P).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //throw new NotImplementedException();
        }

        public Department FindById(string Id)
        {
            var result = (from x in db.Departments where x.D_ID == Id select x).SingleOrDefault();
            return result;
            // throw new NotImplementedException();
        }

        public IEnumerable<Department> GetDepartments()
        {
            return db.Departments;
            // throw new NotImplementedException();
        }

        public void Remove(string Id)
        {
            Department P = db.Departments.Find(Id);
            db.Departments.Remove(P);
            db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
