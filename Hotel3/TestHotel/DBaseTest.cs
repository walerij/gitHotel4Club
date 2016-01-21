using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel3;

namespace TestHotel
{
    [TestClass]
    public class DBaseTest
    {
        /// <summary>
        /// Тестирование соединения
        /// </summary>
        [TestMethod]
        public void TestSQLConnection()
        {
            MySQL sql;
            sql = new MySQL("localhost", "root", "111", "hotel2");
            string result = sql.Scalar("SELECT 5+12");
            Assert.AreEqual(result, "17");

        }
    }
}
