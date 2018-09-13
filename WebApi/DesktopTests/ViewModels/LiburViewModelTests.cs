using Microsoft.VisualStudio.TestTools.UnitTesting;
using Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Desktop.ViewModels.Tests
{
    [TestClass()]
    public class LiburViewModelTests
    {

        [TestMethod()]
        public void FirstDateTest()
        {
            var model = new DateManage(DateTime.Now);
            Assert.IsTrue(model.FirstDate == new DateTime(2018, 5, 1));
        }

        [TestMethod()]
        public void LastDateTest()
        {
            var model = new DateManage(DateTime.Now);
            Assert.IsTrue(model.LastDate== new DateTime(2018, 5, 31));
        }


        [TestMethod()]
        public void WeeksInMonth()
        {
            var model = new DateManage(DateTime.Now);
            Assert.IsTrue(model.Weeks == 8);
        }

        [TestMethod()]
        public void FirstDateInWeek()
        {
            var model = new DateManage(DateTime.Now);
            Assert.IsTrue(model.FirstDayOfWeek.Day==30);
        }

        [TestMethod()]
        public void LastDateInWeek()
        {
            var model = new DateManage(DateTime.Now);
            Assert.IsTrue(model.LastDayOfWeek.Day == 5);
        }
    }
}