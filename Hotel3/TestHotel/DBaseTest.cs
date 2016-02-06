using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel3;
using Hotel3.Model;

namespace TestHotel
{
    [TestClass]
    public class DBaseTest
    {
        MySQL sql;

        public DBaseTest()
        {
            sql = new MySQL("localhost", "root", "", "hotel2");

        }

        /// <summary>
        /// Тестирование соединения
        /// </summary>
        [TestMethod]
        public void TestSQLConnection()
        {            
            string result = sql.Scalar("SELECT 5+12");
            Assert.AreEqual(result, "17");
        }

        /// <summary>
        /// Тестирование добавления дат в модели Calendar
        /// </summary>
        [TestMethod]
        public void TestCalenderAddDays()
        {
            sql.Update("DELETE FROM calendar WHERE YEAR(day)=2016");
            Calendar calendar = new Calendar(sql);
            calendar.InsertDays(2016);
            string days= sql.Scalar("SELECT COUNT(*) FROM calendar WHERE YEAR(day)=2016");
            sql.Update("DELETE FROM calendar WHERE YEAR(day)=2016");
          
            Assert.AreEqual(days, "366");

        }
    }
}
