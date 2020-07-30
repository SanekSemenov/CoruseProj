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

            //List<Files> files = new List<Files>();
            //string folderPath = Server.MapPath("~/Files/");
            //string[] filePaths = Directory.GetFiles(folderPath, "*.txt");
            //List<string> CollectionFileNames = new List<string>();
            //for (int i = 0; i < filePaths.Length; i++)
            //{
            //    files.Add(new Files() { Id = i, Name = Path.GetFileName(filePaths[i]) }); ;
            //}

            //ViewBag.Users = new SelectList(files, "Id", "Name");
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult CategoryChosen(string lol)
        {
            string result = "";
            result = "Вы выбрали: " + lol;
            return Content(result);

            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult CryptWORD()
        {
            ViewBag.Message = "Your application description page.";
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/1.docx"), true))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Body body
                        = wordDoc.MainDocumentPart.Document.Body;
                    wordtxt = body.InnerText;
                }

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/3.docx"), true))
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

            selectList.Add("1");
            selectList.Add("2");
           foreach(var el in selectList)
            {
                ddl.Items.Add(new System.Web.UI.WebControls.ListItem(el));
            }

            List<Files> files = new List<Files>();
            string folderPath = Server.MapPath("~/Files/");
            string[] filePaths = Directory.GetFiles(folderPath, "*.txt");
            List<string> CollectionFileNames = new List<string>();
            for (int i = 0; i < filePaths.Length; i++)
            {
                files.Add(new Files() { Id = i, Name = Path.GetFileName(filePaths[i]) }); ;
            }

            ViewBag.Users = new SelectList(files, "Id", "Name");
            ViewData["Users"] = new SelectList(files, "Id", "Name");

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
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                string fileName = "encrypt.txt";
                // сохраняем файл в папку Files в проекте
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
                string fileName = "1.docx";
                // сохраняем файл в папку Files в проекте
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
        //[HttpPost]
        //public ActionResult Show(string parameterName)
        //{
        //    // тут что то делаешь с этим параметром.
        //    Response.Write("<script>alert('" + parameterName + "')</script>");
        //    key = parameterName;
        //    return View();

        //}


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
                Response.TransmitFile(Server.MapPath("~/Files/3.docx"));
                Response.End();


                FileInfo fi1 = new FileInfo(Server.MapPath("~/Files/1.docx"));
                fi1.Delete();
                FileInfo fi2 = new FileInfo(Server.MapPath("~/Files/3.docx"));
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
                //Response.ContentType = "text/txt";
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

        public ActionResult Encrypting(string parameterName)
        {
            string m = System.IO.File.ReadAllText(Server.MapPath("~/Files/encrypt.txt"));
            string k = parameterName;
            
            int nomer; // Номер в алфавите
            int d; // Смещение
            string s; //Результат
            int j, f; // Переменная для циклов
            int t = 0; // Преременная для нумерации символов ключа.

            char[] massage = m.ToCharArray(); // Превращаем сообщение в массив символов.
            char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

            char[] alfavit = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

            // Перебираем каждый символ сообщения
            for (int i = 0; i < massage.Length; i++)
            {
                // Ищем индекс буквы
                for (j = 0; j < alfavit.Length; j++)
                {
                    if (massage[i] == alfavit[j])
                    {
                        break;
                    }
                }

                if (j != 33) // Если j равно 33, значит символ не из алфавита
                {
                    nomer = j; // Индекс буквы

                    // Ключ закончился - начинаем сначала.
                    if (t > key.Length - 1) { t = 0; }

                    // Ищем индекс буквы ключа
                    for (f = 0; f < alfavit.Length; f++)
                    {
                        if (key[t] == alfavit[f])
                        {
                            break;
                        }
                    }

                    t++;

                    if (f != 33) // Если f равно 33, значит символ не из алфавита
                    {
                        d = nomer + f;
                    }
                    else
                    {
                        d = nomer;
                    }

                    // Проверяем, чтобы не вышли за пределы алфавита
                    if (d > 32)
                    {
                        d = d - 33;
                    }

                    massage[i] = alfavit[d]; // Меняем букву
                }
            }

            s = new string(massage); // Собираем символы обратно в строку.
            System.IO.File.WriteAllText(Server.MapPath("~/Files/encrypt_out.txt"), s);
            /* try
             {
                 if (parameterName.Length > 0)//textBoxKeyWord.Text.Length > 0)
                 {
                     string inputText = "";

                     var cipher = new VigenereCipher("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");

                     StreamReader sr = new StreamReader(Server.MapPath("~/Files/encrypt.txt"));
                     StreamWriter sw = new StreamWriter(Server.MapPath("~/Files/encrypt_out.txt"));

                     while (!sr.EndOfStream)
                     {
                         inputText = sr.ReadLine().ToUpper();
                         sw.Write(cipher.Encrypt(inputText, parameterName.ToUpper()));
                     }

                     //sw.WriteLine(cipher.Encrypt(inputText, parameterName));
                     sr.Close();
                     sw.Close();
                 }
                 else if (parameterName == null)
                 {
                     Response.Write("<script>alert('Заполните поле ключ!')</script>");
                 }
                 else
                 {
                     Response.Write("<script>alert('Заполните поле ключ!')</script>");
                 }

             }
             catch(Exception ex)
             {

             }
            */
            return RedirectToAction("Crypt");
        }

        public ActionResult Decrypting(string parameterName)
        {

            if (parameterName.Length > 0)//textBoxKeyWord.Text.Length > 0)
            {
                string m = System.IO.File.ReadAllText(Server.MapPath("~/Files/decrypt.txt"));
                string k = parameterName;

                int nomer; // Номер в алфавите
                int d; // Смещение
                string s; //Результат
                int j, f; // Переменная для циклов
                int t = 0; // Преременная для нумерации символов ключа.

                char[] massage = m.ToCharArray(); // Превращаем сообщение в массив символов.
                char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

                char[] alfavit = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

                // Перебираем каждый символ сообщения
                for (int i = 0; i < massage.Length; i++)
                {
                    // Ищем индекс буквы
                    for (j = 0; j < alfavit.Length; j++)
                    {
                        if (massage[i] == alfavit[j])
                        {
                            break;
                        }
                    }

                    if (j != 33) // Если j равно 33, значит символ не из алфавита
                    {
                        nomer = j; // Индекс буквы

                        // Ключ закончился - начинаем сначала.
                        if (t > key.Length - 1) { t = 0; }

                        // Ищем индекс буквы ключа
                        for (f = 0; f < alfavit.Length; f++)
                        {
                            if (key[t] == alfavit[f])
                            {
                                break;
                            }
                        }

                        t++;

                        if (f != 33) // Если f равно 33, значит символ не из алфавита
                        {
                            d = nomer + alfavit.Length - f;
                        }
                        else
                        {
                            d = nomer;
                        }

                        // Проверяем, чтобы не вышли за пределы алфавита
                        if (d > 32)
                        {
                            d = d - 33;
                        }

                        massage[i] = alfavit[d]; // Меняем букву
                    }
                }

                s = new string(massage); // Собираем символы обратно в строку.
                System.IO.File.WriteAllText(Server.MapPath("~/Files/decrypt_out.txt"), s);
            }
            else if (parameterName == null)
            {
                Response.Write("<script>alert('Заполните поле ключ!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Заполните поле ключ!')</script>");
            }


            //return View();
            return RedirectToAction("Decrypt");
        }


        public ActionResult EncryptingWord(string keyword)
        {


            string totaltext = "";
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/1.docx"), true))
            {
                DocumentFormat.OpenXml.Wordprocessing.Body body
                    = wordDoc.MainDocumentPart.Document.Body;
                totaltext = body.InnerText;
            }

            string m = totaltext; //File.ReadAllText("1.docx");
            string k = keyword;

            int nomer; // Номер в алфавите
            int d; // Смещение
            string s; //Результат
            int j, f; // Переменная для циклов
            int t = 0; // Преременная для нумерации символов ключа.

            char[] massage = m.ToCharArray(); // Превращаем сообщение в массив символов.
            char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

            char[] alfavit = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

            // Перебираем каждый символ сообщения
            for (int i = 0; i < massage.Length; i++)
            {
                // Ищем индекс буквы
                for (j = 0; j < alfavit.Length; j++)
                {
                    if (massage[i] == alfavit[j])
                    {
                        break;
                    }
                }

                if (j != 33) // Если j равно 33, значит символ не из алфавита
                {
                    nomer = j; // Индекс буквы
                    // Ключ закончился - начинаем сначала.
                    if (t > key.Length - 1) { t = 0; }

                    // Ищем индекс буквы ключа
                    for (f = 0; f < alfavit.Length; f++)
                    {
                        if (key[t] == alfavit[f])
                        {
                            break;
                        }
                    }

                    t++;

                    if (f != 33) // Если f равно 33, значит символ не из алфавита
                    {
                        //d = nomer + alfavit.Length - f;
                         d = nomer + f;
                        //Console.WriteLine("f = " + f);
                        //Console.WriteLine(d);
                    }
                    else
                    {
                        d = nomer;
                    }

                    // Проверяем, чтобы не вышли за пределы алфавита
                    if (d > 32)
                    {
                        d = d - 33;
                    }
                    //Console.WriteLine(d);
                    massage[i] = alfavit[d]; // Меняем букву
                }
            }

            s = new string(massage); // Собираем символы обратно в строку.
            //Console.WriteLine(s);
            //File.WriteAllText("3.docx", s); // Записываем результат в файл.


            // Create Document
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(Server.MapPath("~/Files/3.docx"), WordprocessingDocumentType.Document))
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

        
            return RedirectToAction("CryptWORD");
        }


        public ActionResult DecryptingWord(string keyword999)
        {


            string totaltext = "";
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Server.MapPath("~/Files/decrypt_word.docx"), true))
            {
                DocumentFormat.OpenXml.Wordprocessing.Body body
                    = wordDoc.MainDocumentPart.Document.Body;
                totaltext = body.InnerText;
            }

            string m = totaltext; //File.ReadAllText("1.docx");
            string k = keyword999;

            int nomer; // Номер в алфавите
            int d; // Смещение
            string s; //Результат
            int j, f; // Переменная для циклов
            int t = 0; // Преременная для нумерации символов ключа.

            char[] massage = m.ToCharArray(); // Превращаем сообщение в массив символов.
            char[] key = k.ToCharArray(); // Превращаем ключ в массив символов.

            char[] alfavit = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

            // Перебираем каждый символ сообщения
            for (int i = 0; i < massage.Length; i++)
            {
                // Ищем индекс буквы
                for (j = 0; j < alfavit.Length; j++)
                {
                    if (massage[i] == alfavit[j])
                    {
                        break;
                    }
                }

                if (j != 33) // Если j равно 33, значит символ не из алфавита
                {
                    nomer = j; // Индекс буквы
                    // Ключ закончился - начинаем сначала.
                    if (t > key.Length - 1) { t = 0; }

                    // Ищем индекс буквы ключа
                    for (f = 0; f < alfavit.Length; f++)
                    {
                        if (key[t] == alfavit[f])
                        {
                            break;
                        }
                    }

                    t++;

                    if (f != 33) // Если f равно 33, значит символ не из алфавита
                    {
                        d = nomer + alfavit.Length - f;
                        //d = nomer + f;
                        //Console.WriteLine("f = " + f);
                        //Console.WriteLine(d);
                    }
                    else
                    {
                        d = nomer;
                    }

                    // Проверяем, чтобы не вышли за пределы алфавита
                    if (d > 32)
                    {
                        d = d - 33;
                    }
                    //Console.WriteLine(d);
                    massage[i] = alfavit[d]; // Меняем букву
                }
            }

            s = new string(massage); // Собираем символы обратно в строку.
            //Console.WriteLine(s);
            //File.WriteAllText("3.docx", s); // Записываем результат в файл.


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
