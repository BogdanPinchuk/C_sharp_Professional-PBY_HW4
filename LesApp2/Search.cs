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
// https://uk.wikipedia.org/wiki/Прийменник
// https://zno.if.ua/?p=2716

namespace LesApp2
{
    /// <summary>
    /// Пошук даних на сайті
    /// </summary>
    static class Search
    {
        /// <summary>
        /// Назва файлу для збереження даних
        /// </summary>
        private static readonly string filename = "LesApp2.txt";
        /// <summary>
        /// Для синхтонізації доступу до файлу
        /// </summary>
        private static readonly object block = new object();
        /// <summary>
        /// Слово на яке треба зробити заміну
        /// </summary>
        private const string word = "ГАВ!";

        /// <summary>
        /// Шаблони RegEx для пошуку
        /// </summary>
        private static readonly List<string> words = new List<string>()
        {
            #region patterns
            "в ім’я",
            "з метою",
            "з нагоди",
            "з огляду",
            "з приводу",
            "за допомогою",
            "з-за",
            "з-під",
            "із-за",
            "на випадок",
            "незважаючи на",
            "під час",
            "у разі",
            "у результаті",
            "без",
            "біля",
            "близько",
            "в",
            "від",
            "внаслідок",
            "всупереч",
            "для",
            "до",
            "з",
            "за",
            "завдяки",
            "завинятком",
            "задля",
            "заради",
            "згідноз",
            "зі",
            "зо",
            "із",
            "коло",
            "крізь",
            "крім",
            "між",
            "на",
            "над",
            "наді",
            "напередодні",
            "наперекір",
            "незважаючина",
            "о",
            "об",
            "обабіч",
            "перед",
            "під",
            "підчас",
            "після",
            "по",
            "поза",
            "поміж",
            "поруч",
            "при",
            "про",
            "протягом",
            "у",
            "упродовж",
            "через",
            "щодо",
            "як",
	        #endregion
        };

        /// <summary>
        /// Створення паттерну
        /// </summary>
        /// <param name="word">слово для паттерна</param>
        /// <returns></returns>
        private static string DoPattern(string word)
            => $@"([ ]*\b){word}([^\w]|\b)";

        /// <summary>
        /// Створення текстового файла, якщо немає
        /// </summary>
        /// <param name="path">назва файла</param>
        /// <param name="address">адреса сайту який потрібно зберегти в текстовий файл</param>
        internal static void CreateFile(string path, string address)
        {
            // перевірка на явності файла
            if (File.Exists(path))
                return;

            // if the file is absent, we must create it
            try
            {
                // Відправка запиту
                HttpWebRequest request = WebRequest.CreateHttp(address);

                // Отримання відповіді, створення потоку, створення читача і записника
                using (HttpWebResponse responce = (HttpWebResponse)request.GetResponse())
                using (Stream streamR = responce.GetResponseStream())
                using (StreamReader readerW = new StreamReader(streamR, Encoding.GetEncoding(responce.CharacterSet)))
                using (Stream streamW = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(streamW, Encoding.Unicode))
                {
                    lock (block)
                    {
                        // збереження даних + блокування доступу до файла
                        writer.WriteLine(readerW.ReadToEnd());
                    }

                    // виведення сповіщення
                    Console.WriteLine($"\tФайл створено.");
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
        /// Створення текстового файла, якщо немає
        /// </summary>
        /// <param name="address">адреса сайту який потрібно зберегти в текстовий файл</param>
        internal static void CreateFile(string address)
            => CreateFile(filename, address);

        /// <summary>
        /// Парсинг даних в файлі
        /// </summary>
        /// <param name="path">Адреса файла</param>
        /// <returns></returns>
        internal static void CorectData(string path)
        {
            // перевірна наявності файла
            if (!File.Exists(path))
                return;

            try
            {
                // Cтворення потоку, створення читача і записника
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                {
                    string data = string.Empty;

                    // виведення сповіщення
                    Console.WriteLine($"\tФайл корегується.");

                    lock (block)
                    {
                        data = reader.ReadToEnd();
                    }
                    
                    // корегування даних
                    ReplacePreposition(ref data);

                    // збереження даних
                    lock (block)
                    {
                        writer.WriteLine(data);
                    }

                    // виведення сповіщення
                    Console.WriteLine($"\tФайл скореговано.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Парсинг даних на сайті
        /// </summary>
        /// <returns></returns>
        internal static void CorectData()
            => CorectData(filename);

        /// <summary>
        /// Заміна прийменників
        /// </summary>
        /// <param name="data">вхідні текстові дані</param>
        /// <returns></returns>
        internal static void ReplacePreposition(ref string data)
        {
            foreach (string preposition in words)
            {
                data = Regex.Replace(data, DoPattern(preposition), $" {word} ");
            }
        }

        /// <summary>
        /// Чи наявні шаблонні дані у вхідних даних (рядку)
        /// </summary>
        /// <param name="data">вхідні дані</param>
        /// <returns></returns>
        internal static bool IsPreposition(string data)
        {
            foreach (string preposition in words)
            {
                if (Regex.IsMatch(data, DoPattern(preposition)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Відкриття файлу з найденими даними
        /// </summary>
        internal static void OpenResultFile()
            => OpenResultFile(filename);

        /// <summary>
        /// Відкриття файлу з найденими даними
        /// </summary>
        internal static void OpenResultFile(string path)
            => Process.Start(path);

        /// <summary>
        /// Видалення файла
        /// </summary>
        internal static void DeleteFile()
            => File.Delete(filename);

    }
}
