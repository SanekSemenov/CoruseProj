using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    public class EncTxtController : Controller
    {
        // GET: EncTxt
       /* public ActionResult Index()
        {
            return View();
        }
       */
        public ActionResult Crypt()
        {
            ViewBag.Message = "Page for crypt.";

            return View();
        }

        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // Get file name
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                string fileName = "encrypt.txt";
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Crypt");
        }

        public ActionResult Encrypting(string key_crypt_txt)
        {
            System.IO.File.WriteAllText(Server.MapPath("~/Files/encrypt_out.txt"), Encrypt.EncryptTxt(System.IO.File.ReadAllText(Server.MapPath("~/Files/encrypt.txt")), key_crypt_txt));
            return RedirectToAction("Crypt");

        }
        
        public ActionResult Download()
        {
            try
            {
                Response.ContentType = "text/txt";
                Response.AppendHeader("Content-Disposition", "attachment; filename=encrypt.txt");
                Response.TransmitFile(Server.MapPath("~/Files/encrypt_out.txt"));
                Response.End();

                FileInfo fi1 = new FileInfo(Server.MapPath("~/Files/encrypt.txt"));
                fi1.Delete();
                FileInfo fi2 = new FileInfo(Server.MapPath("~/Files/encrypt_out.txt"));
                fi2.Delete();
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}