using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication5.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication5.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private HomeController controller;
        private ViewResult result;


        [TestMethod]
        public void IndexViewResultNotNull()
        {
            controller = new HomeController();
            result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CryptWORDTest()
        {
            controller = new HomeController();
            result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void EncryptingTest()
        {
            string m = "поздравляю, ты получил исходный текст!!!";
            string k = "скорпион";

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
            string result = "бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!";
            Assert.AreEqual(s, result);
        }

        [TestMethod()]
        public void DecryptingTest()
        {
            string m = "бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!";
            string k = "скорпион";

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
            string result = "поздравляю, ты получил исходный текст!!!";
            Assert.AreEqual(s, result);
        }

        
    }
}