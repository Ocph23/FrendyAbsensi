using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesktopTests.ViewModels
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DateTime start = new DateTime(2018, 2, 1);
            DateTime end = new DateTime(2018, 2, 28);
            var liburcontext = new WebApi.Models.KehadiranContext(start, end);
            Assert.IsTrue(liburcontext.TotalLibur > 5);

        }
    }
}
