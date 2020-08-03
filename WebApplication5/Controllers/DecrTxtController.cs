using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Management.Instrumentation;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    public class DecrTxtController : Controller
    {
        // GET: DecrTxt
        public ActionResult Decrypt()
        {
            ViewBag.Message = "Page for decrypt.";

            return View();
        }

        public ActionResult DeUpload(HttpPostedFileBase deupload)
        {
            if (deupload != null)
            {
                // получаем имя файла
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                string fileName = "decrypt.txt";
                // сохраняем файл в папку Files в проекте
                deupload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Decrypt");
        }
        public ActionResult Decrypting(string key_decrypt_txt)
        {
            System.IO.File.WriteAllText(Server.MapPath("~/Files/decrypt_out.txt"), Encrypt.EncryptTxt(System.IO.File.ReadAllText(Server.MapPath("~/Files/decrypt.txt")), key_decrypt_txt));
            return RedirectToAction("Decrypt");
        }

            public ActionResult DeDownload()
        {

            Response.ContentType = "text/txt";
            Response.AppendHeader("Content-Disposition", "attachment; filename=decrypt.txt");
            Response.TransmitFile(Server.MapPath("~/Files/decrypt_out.txt"));
            Response.End();

            FileInfo fi1 = new FileInfo(Server.MapPath("~/Files/decrypt.txt"));
            fi1.Delete();
            FileInfo fi2 = new FileInfo(Server.MapPath("~/Files/decrypt_out.txt"));
            fi2.Delete();

            return View();
        }
    }
}