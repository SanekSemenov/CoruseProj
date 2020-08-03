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
    public class DecrWordController : Controller
    {
        // GET: DecrWord
        public static string dewordtxt = "";
        public static string dewordtxt_out = "";
        public ActionResult DecryptWORD()
        {
            ViewBag.Message = "Your application description page.";
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/decrypt_word.docx"), true))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Body body
                        = wordDoc.MainDocumentPart.Document.Body;
                    dewordtxt = body.InnerText;
                }

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/decrypt_word_out.docx"), true))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Body body
                        = wordDoc.MainDocumentPart.Document.Body;
                    dewordtxt_out = body.InnerText;
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult DeUploadWORD(HttpPostedFileBase deuploadword)
        {
            if (deuploadword != null)
            {
                // получаем имя файла
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                string fileName = "decrypt_word.docx";
                // сохраняем файл в папку Files в проекте
                deuploadword.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("DecryptWORD");
        }

        public ActionResult DecryptingWord(string key_decrypt_word)
        {
            string totaltext = "";
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/decrypt_word.docx"), true))
            {
                DocumentFormat.OpenXml.Wordprocessing.Body body
                    = wordDoc.MainDocumentPart.Document.Body;
                totaltext = body.InnerText;
            }

            using (WordprocessingDocument wordDocument =
                       WordprocessingDocument.Create(Server.MapPath("~/Files/decrypt_word_out.docx"), WordprocessingDocumentType.Document))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text(Decrypt.DecryptWord(totaltext, key_decrypt_word)));
                mainPart.Document.Save();
            }

            return RedirectToAction("DecryptWORD");
        }
            public ActionResult DeDownloadword()
        {
            try
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Response.AppendHeader("Content-Disposition", "attachment; filename=encrypt.docx");
                Response.TransmitFile(Server.MapPath("~/Files/decrypt_word_out.docx"));
                Response.End();

                FileInfo fi1 = new FileInfo(Server.MapPath("~/Files/decrypt_word.docx"));
                fi1.Delete();
                FileInfo fi2 = new FileInfo(Server.MapPath("~/Files/decrypt_word_out.docx"));
                fi2.Delete();
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}