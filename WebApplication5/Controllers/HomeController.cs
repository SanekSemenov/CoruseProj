using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Management.Instrumentation;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public class Files
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public ActionResult Index()
        {
            //ViewBag.Message = "Hello world!";
            return View();
        }
    }
}
