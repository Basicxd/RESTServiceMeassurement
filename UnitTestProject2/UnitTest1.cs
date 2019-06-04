using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTServiceMeassurement;
using RESTServiceMeassurement.Controllers;
using System.Collections.Generic;
using System;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AmountOfDataRows()
        {
            MeassurementsController mc = new MeassurementsController();

            IList<Meassurement> result = mc.GetAll();

            var counted = result.Count;

            Assert.AreEqual(4, counted);
        }

        [TestMethod]
        public void OneIdCorrect()
        {
            Meassurement res = GetCustomersAsyncid(1).Result;

            Thread.Sleep(1000);

            var ress = res.Pressure;

            Assert.AreEqual(20, ress);
        }
    }
}
