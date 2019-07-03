using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
   public class StaffRepository
    {
        DefyClinicContxet db = new DefyClinicContxet();
        public void Add(Staff P)
        {
            db.Staffs.Add(P);
            db.SaveChanges();
            //  throw new NotImplementedException();
        }

        public void Edit(Staff P)
        {
            db.Entry(P).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //throw new NotImplementedException();
        }

        public Staff FindById(string Id)
        {
            var result = (from x in db.Staffs where x.EmpNo == Id select x).SingleOrDefault();
            return result;
            // throw new NotImplementedException();
        }

        public IEnumerable<Staff> GetStaffs()
        {
            return db.Staffs;
            // throw new NotImplementedException();
        }

        public void Remove(string Id)
        {
            Staff P = db.Staffs.Find(Id);
            db.Staffs.Remove(P);
            db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
