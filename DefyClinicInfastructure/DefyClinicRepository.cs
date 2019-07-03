using DefyClinicModels;
using System.Collections.Generic;
using System.Linq;

namespace DefyClinicInfastructure
{
  public class DefyClinicRepository
  {
    readonly DefyClinicContxet _db = new DefyClinicContxet();
    public void Add(PreSlot slot)
    {
      _db.Slots.Add(slot);
      _db.SaveChanges();
      //  throw new NotImplementedException();
    }

    public void Edit(PreSlot P)
    {
      _db.Entry(P).State = System.Data.Entity.EntityState.Modified;
      _db.SaveChanges();
      //throw new NotImplementedException();
    }

    public PreSlot FindById(int id)
    {
      var result = _db.Slots.SingleOrDefault(x => x.Id == id);
      return result;
      // throw new NotImplementedException();
    }

    public IEnumerable<PreSlot> GetSlots()
    {
      return _db.Slots;
      // throw new NotImplementedException();
    }

    public void Remove(int id)
    {
      var slot = _db.Slots.Find(id);
      _db.Slots.Remove(slot);
      _db.SaveChanges();
      //throw new NotImplementedException();
    }
  }
}