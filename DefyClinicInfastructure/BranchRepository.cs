using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
   public class BranchRepository
    {
        DefyClinicContxet db = new DefyClinicContxet();
        public void Add(Branch P)
        {
            db.Branchs.Add(P);
            db.SaveChanges();
            //  throw new NotImplementedException();
        }

        public void Edit(Branch P)
        {
            db.Entry(P).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //throw new NotImplementedException();
        }

        public Branch FindById(string Id)
        {
            var result = (from x in db.Branchs where x.BranchCode == Id select x).SingleOrDefault();
            return result;
            // throw new NotImplementedException();
        }

        public IEnumerable<Branch> GetBranches()
        {
            return db.Branchs;
            // throw new NotImplementedException();
        }

        public void Remove(string Id)
        {
            Branch P = db.Branchs.Find(Id);
            db.Branchs.Remove(P);
            db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
