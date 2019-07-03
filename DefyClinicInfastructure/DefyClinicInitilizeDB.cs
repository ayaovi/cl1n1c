using System.Data.Entity;

namespace DefyClinicInfastructure
{
  public class DefyClinicInitilizeDB : DropCreateDatabaseIfModelChanges<DefyClinicContxet>
  {
    protected override void Seed(DefyClinicContxet context) {}
  }
}