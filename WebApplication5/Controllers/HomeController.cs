using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Crypt()
        {
            ViewBag.Message = "Page for crypt.";

            return View();
        }

        public ActionResult Decrypt()
        {
            ViewBag.Message = "Page for decrypt.";

            return View();
        }

        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
        }


        public ActionResult Download()
        {
            
                Response.ContentType = "text/txt";
                Response.AppendHeader("Content-Disposition", "attachment; filename=Deshifrovka.txt");
                Response.TransmitFile(Server.MapPath("~/Files/Result.txt"));
                Response.End();
            
            return RedirectToAction("Index");
        }




    }
}