using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication5.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication5.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private HomeController controller;
        private ViewResult result;


        [TestMethod]
        public void IndexViewResultNotNull()
        {
            controller = new HomeController();
            result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CryptWORDTest()
        {
            controller = new HomeController();
            result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}