using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Services
{
    public class Decrypt
    {
        public static string DecryptTxt(string content, string key_decrypt_txt)
        {
                string m = content;
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
            return s;
        }


        public static string DecryptWord(string content, string key_decrypt_word)
        {
                string m = content; //File.ReadAllText("1.docx");
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
                return s;
        }

    }
}