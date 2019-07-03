using DefyClinicInfastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicTest
{
    [TestClass]
    public class DefyClinicRepositoryTest
    {

        DefyClinicRepository rep;
        [TestInitialize]
        public void TestSetUp()
        {
            DefyClinicInitilizeDB db = new DefyClinicInitilizeDB();
            System.Data.Entity.Database.SetInitializer(db);
            rep = new DefyClinicRepository();
        }
        [TestMethod]
        public void NumberOfData()
        {
            var result = rep.GetSlots();
            Assert.IsNotNull(result);
            var Record = result.ToList().Count;
            Assert.AreEqual(1, Record);
        }
    }
}
