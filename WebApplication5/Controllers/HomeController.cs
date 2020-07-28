using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Management.Instrumentation;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public static List<string> selectList = new List<string>();
        public static DropDownList ddl = new DropDownList();
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

        public ViewResult CategoryChosen(string Mov)
        {
            ViewBag.Message = Mov;
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

            selectList.Add("1");
            selectList.Add("2");
           foreach(var el in selectList)
            {
                ddl.Items.Add(new ListItem(el));
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
                string fileName = "forCrypt";
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
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
            
                Response.ContentType = "text/txt";
                Response.AppendHeader("Content-Disposition", "attachment; filename=Deshifrovka.txt");
                Response.TransmitFile(Server.MapPath("~/Files/out.txt"));
                Response.End();
            
            return RedirectToAction("Index");
        }

        public ActionResult Encrypting(string parameterName)
        {

            if (parameterName.Length > 0)//textBoxKeyWord.Text.Length > 0)
            {
                string inputText = "";

                var cipher = new VigenereCipher("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");

                StreamReader sr = new StreamReader(Server.MapPath("~/Files/in.txt"));
                StreamWriter sw = new StreamWriter(Server.MapPath("~/Files/out.txt"));

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

          
            //return View();
            return RedirectToAction("Crypt");
        }

        public ActionResult Decrypting(string parameterName)
        {

            if (parameterName.Length > 0)//textBoxKeyWord.Text.Length > 0)
            {
                string inputText = "";

                var cipher = new VigenereCipher("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");

                StreamReader sr = new StreamReader(Server.MapPath("~/Files/out.txt"));
                //StreamReader sr = new StreamReader(Server.MapPath("~/Files/Result.docx"));
                StreamWriter sw = new StreamWriter(Server.MapPath("~/Files/decrypt.txt"));
                //StreamWriter sw = new StreamWriter(Server.MapPath("~/Files/Result1.docx"));

                while (!sr.EndOfStream)
                {
                    inputText = sr.ReadLine().ToUpper();
                    sw.Write(cipher.Decrypt(inputText, parameterName.ToUpper()));
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


            //return View();
            return RedirectToAction("Decrypt");
        }



    }

    public class VigenereCipher
    {
        const string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        readonly string letters;

        public VigenereCipher(string alphabet = null)
        {
            letters = string.IsNullOrEmpty(alphabet) ? defaultAlphabet : alphabet;
        }

        //генерация повторяющегося пароля
        private string GetRepeatKey(string s, int n)
        {
            var p = s;
            while (p.Length < n)
            {
                p += p;
            }

            return p.Substring(0, n);
        }

        private string Vigenere(string text, string password, bool encrypting = true)
        {
            var gamma = GetRepeatKey(password, text.Length);
            var retValue = "";
            var q = letters.Length;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);
                var codeIndex = letters.IndexOf(gamma[i]);
                if (letterIndex < 0)
                {
                    //если буква не найдена, добавляем её в исходном виде
                    retValue += text[i].ToString();
                }
                else
                {
                    retValue += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
                }
            }

            return retValue;
        }

        //шифрование текста
        public string Encrypt(string plainMessage, string password)
            => Vigenere(plainMessage, password);

        //дешифрование текста
        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }
}
