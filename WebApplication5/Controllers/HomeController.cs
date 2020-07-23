using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        char[] characters = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                            'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                            'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                            'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                            '8', '9', '0' };

        private int N; //длина алфавита
        public string key = "";

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
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                string fileName = "forCrypt";
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
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
                Response.TransmitFile(Server.MapPath("~/Files/Result.txt"));
                Response.End();
            
            return RedirectToAction("Index");
        }

        public ActionResult Someclick(string parameterName)
        {
            N = characters.Length;
            key = parameterName;
            
            if (key.Length > 0)//textBoxKeyWord.Text.Length > 0)
            {
                string s;
                Response.Write("<script>alert('" + key + "')</script>");

                StreamReader sr = new StreamReader(Server.MapPath("~/Files/in.txt"));
                StreamWriter sw = new StreamWriter(Server.MapPath("~/Files/out.txt"));

                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    sw.WriteLine(Encode(s, key));
                    // sw.Write(WebApplication5.Controllers.HomeController.Encode(s, textBoxKeyWord.Text), true, System.Text.Encoding.GetEncoding("windows-1251"));
                }


                sr.Close();
                sw.Close();
            }
            else if ( key == null)
            {
                Response.Write("<script>alert('Заполните поле ключ!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Заполните поле ключ!')</script>");
            }

            //return View();
            return RedirectToAction("Index");
        }

        public string Encode(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                int c = (Array.IndexOf(characters, symbol) +
                    Array.IndexOf(characters, keyword[keyword_index])) % N;

                result += characters[c];

                keyword_index++;

                if ((keyword_index + 1) == keyword.Length)
                    keyword_index = 0;
            }

            return result;
        }
    }
}