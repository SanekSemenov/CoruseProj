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
        public static List<string> selectList = new List<string>();
        public static DropDownList ddl = new DropDownList();
        public static string wordtxt = "";
        public static string wordtxt_out = "";

        public static string dewordtxt = "";
        public static string dewordtxt_out = "";

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Files
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public ActionResult Create()
        {
            List<User> users = new List<User>
        {
            new User {Id=1, Name="Tom", Age=35 },
            new User {Id=2, Name="Alice", Age=29 },
            new User {Id=3, Name="Sam", Age=36 },
            new User {Id=4, Name="Bob", Age=31 },
        };
            ViewBag.Users = new SelectList(users, "Id", "Name");
            ViewData["Users"] = new SelectList(users, "Id", "Name");
                       
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Hello world!";
            return View();
        }
        
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
            catch(Exception ex)
            {

            }
            
            return View();
        }

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
                // Get file name
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                string fileName = "encrypt.txt";
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Crypt");
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
            catch(Exception ex)
            {

            }
            return View();
        }

        public ActionResult Downloadword()
        {
            try
            {
                //Response.ContentType = "text/txt";
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

        public ActionResult Encrypting(string key_crypt_txt)
        {
            string m = System.IO.File.ReadAllText(Server.MapPath("~/Files/encrypt.txt"));
            string k = key_crypt_txt;
            
            int number; // Номер в алфавите
            int d; // Смещение
            string s; //Результат
            int j, f; // Переменная для циклов
            int t = 0; // Преременная для нумерации символов ключа.

            char[] message = m.ToCharArray(); // Превращаем сообщение в массив символов.
            char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

            char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

            // Перебираем каждый символ сообщения
            for (int i = 0; i < message.Length; i++)
            {
                // Ищем индекс буквы
                for (j = 0; j < alphabet.Length; j++)
                {
                    if (message[i] == alphabet[j])
                    {
                        break;
                    }
                }

                if (j != 33) // Если j равно 33, значит символ не из алфавита
                {
                    number = j; // Индекс буквы

                    // Ключ закончился - начинаем сначала.
                    if (t > key.Length - 1) { t = 0; }

                    // Ищем индекс буквы ключа
                    for (f = 0; f < alphabet.Length; f++)
                    {
                        if (key[t] == alphabet[f])
                        {
                            break;
                        }
                    }

                    t++;

                    if (f != 33) // Если f равно 33, значит символ не из алфавита
                    {
                        d = number + f;
                    }
                    else
                    {
                        d = number;
                    }

                    // Проверяем, чтобы не вышли за пределы алфавита
                    if (d > 32)
                    {
                        d = d - 33;
                    }

                    message[i] = alphabet[d]; // Меняем букву
                }
            }

            s = new string(message); // Собираем символы обратно в строку.
            System.IO.File.WriteAllText(Server.MapPath("~/Files/encrypt_out.txt"), s);
           
            return RedirectToAction("Crypt");
        }

        public ActionResult Decrypting(string key_decrypt_txt)
        {

            if (key_decrypt_txt.Length > 0)//textBoxKeyWord.Text.Length > 0)
            {
                string m = System.IO.File.ReadAllText(Server.MapPath("~/Files/decrypt.txt"));
                string k = key_decrypt_txt;
                int number; // Номер в алфавите
                int d; // Смещение
                string s; //Результат
                int j, f; // Переменная для циклов
                int t = 0; // Преременная для нумерации символов ключа.

                char[] message = m.ToCharArray(); // Превращаем сообщение в массив символов.
                char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

                char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

                // Перебираем каждый символ сообщения
                for (int i = 0; i < message.Length; i++)
                {
                    // Ищем индекс буквы
                    for (j = 0; j < alphabet.Length; j++)
                    {
                        if (message[i] == alphabet[j])
                        {
                            break;
                        }
                    }

                    if (j != 33) // Если j равно 33, значит символ не из алфавита
                    {
                        number = j; // Индекс буквы

                        // Ключ закончился - начинаем сначала.
                        if (t > key.Length - 1) { t = 0; }

                        // Ищем индекс буквы ключа
                        for (f = 0; f < alphabet.Length; f++)
                        {
                            if (key[t] == alphabet[f])
                            {
                                break;
                            }
                        }

                        t++;

                        if (f != 33) // Если f равно 33, значит символ не из алфавита
                        {
                            d = number + alphabet.Length - f;
                        }
                        else
                        {
                            d = number;
                        }

                        // Проверяем, чтобы не вышли за пределы алфавита
                        if (d > 32)
                        {
                            d = d - 33;
                        }

                        message[i] = alphabet[d]; // Меняем букву
                    }
                }

                s = new string(message); // Собираем символы обратно в строку.
                System.IO.File.WriteAllText(Server.MapPath("~/Files/decrypt_out.txt"), s);
            }
            else if (key_decrypt_txt == null)
            {
                Response.Write("<script>alert('Заполните поле ключ!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Заполните поле ключ!')</script>");
            }
            return RedirectToAction("Decrypt");
        }

        public ActionResult EncryptingWord(string key_crypt_word)
        {
            try
            {
                string totaltext = "";
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/encrypt.docx"), true))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Body body
                        = wordDoc.MainDocumentPart.Document.Body;
                    totaltext = body.InnerText;
                }

                string m = totaltext;
                string k = key_crypt_word;

                int number; // Номер в алфавите
                int d; // Смещение
                string s; //Результат
                int j, f; // Переменная для циклов
                int t = 0; // Преременная для нумерации символов ключа.

                char[] message = m.ToCharArray(); // Превращаем сообщение в массив символов.
                char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

                char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

                // Перебираем каждый символ сообщения
                for (int i = 0; i < message.Length; i++)
                {
                    // Ищем индекс буквы
                    for (j = 0; j < alphabet.Length; j++)
                    {
                        if (message[i] == alphabet[j])
                        {
                            break;
                        }
                    }

                    if (j != 33) // Если j равно 33, значит символ не из алфавита
                    {
                        number = j; // Индекс буквы
                                    // Ключ закончился - начинаем сначала.
                        if (t > key.Length - 1) { t = 0; }

                        // Ищем индекс буквы ключа
                        for (f = 0; f < alphabet.Length; f++)
                        {
                            if (key[t] == alphabet[f])
                            {
                                break;
                            }
                        }

                        t++;

                        if (f != 33) // Если f равно 33, значит символ не из алфавита
                        {
                            d = number + f;
                        }
                        else
                        {
                            d = number;
                        }

                        // Проверяем, чтобы не вышли за пределы алфавита
                        if (d > 32)
                        {
                            d = d - 33;
                        }
                        message[i] = alphabet[d]; // Меняем букву
                    }
                }

                s = new string(message); // Собираем символы обратно в строку.
                                         // Create Document
                using (WordprocessingDocument wordDocument =
                    WordprocessingDocument.Create(Server.MapPath("~/Files/encrypt_out.docx"), WordprocessingDocumentType.Document))
                {
                    // Add a main document part. 
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    // Create the document structure and add some text.
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());
                    Paragraph para = body.AppendChild(new Paragraph());
                    Run run = para.AppendChild(new Run());
                    run.AppendChild(new Text(s));
                    mainPart.Document.Save();
                }
            }
            catch(Exception ex)
            {

            }
            return View("CryptWORD");
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

            string m = totaltext; //File.ReadAllText("1.docx");
            string k = key_decrypt_word;

            int number; // Номер в алфавите
            int d; // Смещение
            string s; //Результат
            int j, f; // Переменная для циклов
            int t = 0; // Преременная для нумерации символов ключа.

            char[] message = m.ToCharArray(); // Превращаем сообщение в массив символов.
            char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

            char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

            // Перебираем каждый символ сообщения
            for (int i = 0; i < message.Length; i++)
            {
                // Ищем индекс буквы
                for (j = 0; j < alphabet.Length; j++)
                {
                    if (message[i] == alphabet[j])
                    {
                        break;
                    }
                }

                if (j != 33) // Если j равно 33, значит символ не из алфавита
                {
                    number = j; // Индекс буквы
                    // Ключ закончился - начинаем сначала.
                    if (t > key.Length - 1) { t = 0; }

                    // Ищем индекс буквы ключа
                    for (f = 0; f < alphabet.Length; f++)
                    {
                        if (key[t] == alphabet[f])
                        {
                            break;
                        }
                    }

                    t++;

                    if (f != 33) // Если f равно 33, значит символ не из алфавита
                    {
                        d = number + alphabet.Length - f;
                    }
                    else
                    {
                        d = number;
                    }

                    // Проверяем, чтобы не вышли за пределы алфавита
                    if (d > 32)
                    {
                        d = d - 33;
                    }
                    //Console.WriteLine(d);
                    message[i] = alphabet[d]; // Меняем букву
                }
            }

            s = new string(message); // Собираем символы обратно в строку.

            // Create Document
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
                run.AppendChild(new Text(s));
                mainPart.Document.Save();
            }


            return RedirectToAction("DecryptWORD");
        }


    }


       
    
}
