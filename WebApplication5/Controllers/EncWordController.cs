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
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    public class EncWordController : Controller
    {
        // GET: EncWord
        public static string wordtxt = "";
        public static string wordtxt_out = "";
        public ActionResult CryptWORD()
        {
            ViewBag.Message = "Your application description page.";
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/encrypt.docx"), true))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Body body
                        = wordDoc.MainDocumentPart.Document.Body;
                    wordtxt = body.InnerText;
                }

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/encrypt_out.docx"), true))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Body body
                        = wordDoc.MainDocumentPart.Document.Body;
                    wordtxt_out = body.InnerText;
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        public ActionResult UploadWORD(HttpPostedFileBase uploadword)
        {
            if (uploadword != null)
            {
                // получаем имя файла
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                string fileName = "encrypt.docx";
                uploadword.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("CryptWORD");
        }

        public ActionResult EncryptingWord(string key_crypt_word)
        {
            string totaltext = "";
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/encrypt.docx"), true))
            {
                DocumentFormat.OpenXml.Wordprocessing.Body body
                    = wordDoc.MainDocumentPart.Document.Body;
                totaltext = body.InnerText;
            }
            
            using (WordprocessingDocument wordDocument =
                       WordprocessingDocument.Create(Server.MapPath("~/Files/encrypt_out.docx"), WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text(Encrypt.EncryptWord(totaltext, key_crypt_word)));
                mainPart.Document.Save();
            }
            return RedirectToAction("CryptWORD");
        }
            public ActionResult Downloadword()
        {
            try
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Response.AppendHeader("Content-Disposition", "attachment; filename=encrypt.docx");
                Response.TransmitFile(Server.MapPath("~/Files/encrypt_out.docx"));
                Response.End();

                FileInfo fi1 = new FileInfo(Server.MapPath("~/Files/encrypt.docx"));
                fi1.Delete();
                FileInfo fi2 = new FileInfo(Server.MapPath("~/Files/encrypt_out.docx"));
                fi2.Delete();
            }
            catch (Exception ex)
            {

            }
            return View();
        }

    }
}