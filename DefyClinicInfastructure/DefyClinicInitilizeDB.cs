using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicInfastructure
{
    public class DefyClinicInitilizeDB : DropCreateDatabaseIfModelChanges<DefyClinicContxet>
    {
        protected override void Seed(DefyClinicContxet context)
        {
        }
    }
}
