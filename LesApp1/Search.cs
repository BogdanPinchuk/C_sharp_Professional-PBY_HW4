using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// http://wladm.narod.ru/C_Sharp/regex.html
// https://metanit.com/sharp/net/
// http://www.cyberforum.ru/csharp-beginners/thread1358433.html
// http://www.cyberforum.ru/csharp-beginners/thread1386200.html
// https://habr.com/ru/post/175329/

namespace LesApp1
{
    /// <summary>
    /// Пошук даних на сайті
    /// </summary>
    static class Search
    {
        /// <summary>
        /// Об'єкт пошуку
        /// </summary>
        internal enum ObjectForSearch
        {
            /// <summary>
            /// Посилання на інші сторінки
            /// </summary>
            Link,
            /// <summary>
            /// Номера телефонів
            /// </summary>
            Phone,
            /// <summary>
            /// Почтові адреси
            /// </summary>
            Email
        }

        /// <summary>
        /// Об'єкт для блокування/синхронізації доступу до файлу
        /// </summary>
        public static object blockFile = new object();
        /// <summary>
        /// Об'єкт для блокування/синхронізації доступу до консолі
        /// </summary>
        public static object blockConsole = new object();

        /// <summary>
        /// Назва файлу для збереження даних
        /// </summary>
        private static string filename = "LesApp1.Data.txt";

        /// <summary>
        /// Шаблони RegEx для пошуку
        /// </summary>
        private static List<string> patterns = new List<string>()
        {
            @"(\b\w+:\/\/\w+((\.\w)*\w+)*\.\w{2,3}(\/\w*|\.\w*|\?\w*\=\w*)*)",  // сайт
            @"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){7}",  // телефон
            @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)" + 
            @"*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",    // почта
        };

        /// <summary>
        /// Парсинг даних на сайті
        /// </summary>
        /// <param name="address">Адреса сайту</param>
        /// <param name="obj">Тип об'єкта пошуку</param>
        /// <returns></returns>
        internal static void ParsingSite(string address, ObjectForSearch obj)
            => ParsingSite(address, patterns[(int)obj]);

        /// <summary>
        /// Парсинг даних на сайті
        /// </summary>
        /// <param name="address">Адреса сайту</param>
        /// <param name="pattern">Шаблон пошуку</param>
        /// <returns></returns>
        internal static void ParsingSite(string address, string pattern)
        {
            // найдені дані
            List<string> result = new List<string>();

            try
            {
                // Відправка запиту
                HttpWebRequest request = WebRequest.CreateHttp(address);

                // Отримання відповіді, створення потоку, створення читача і записника
                using (HttpWebResponse responce = (HttpWebResponse)request.GetResponse())
                using (Stream streamR = responce.GetResponseStream())
                using (StreamReader readerW = new StreamReader(streamR, Encoding.GetEncoding(responce.CharacterSet)))
                using (Stream streamW = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(streamW, Encoding.Unicode))
                using (StreamWriter writer = new StreamWriter(streamW, Encoding.Unicode))
                {
                    // виведення сповіщення
                    lock (blockConsole)
                    {
                        Console.WriteLine($"\n\tПочався пошук за адресою: {address}\n");
                    }

                    // екземпляр класа RegEx
                    var regex = new Regex(pattern);

                    // пошук даних
                    foreach (Match i in regex.Matches(readerW.ReadToEnd()))
                    {
                        result.Add(i.Value);
                    }

                    // збереження даних + блокування доступу до файла
                    lock (blockFile)
                    {
                        //writer.WriteLine(reader.ReadToEnd());
                        reader.ReadToEnd();
                        writer.WriteLine();

                        foreach (var i in result)
                        {
                            writer.WriteLine(i);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Відкриття файлу з найденими даними
        /// </summary>
        internal static void OpenResultFile()
            => Process.Start(filename);

    }
}
