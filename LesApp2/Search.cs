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
        private static string filename = "LesApp2.txt";

        /// <summary>
        /// Шаблони RegEx для пошуку
        /// </summary>
        private static List<string> patterns = new List<string>()
        {
            #region patterns
		    @"^ без$",
            @"^ у$",
            @"^ в$",
            @"^ від$",
            @"^ для$",
            @"^ біля$",
            @"^ по$",
            @"^ з$",
            @"^ зі$",
            @"^ із$",
            @"^ за$",
            @"^ через$",
            @"^ при$",
            @"^ до$",
            @"^ як$",
            @"^ задля$",
            @"^ з-під$",
            @"^ із-за$",
            @"^ щодо$",
            @"^ близько$",
            @"^ внаслідок$",
            @"^ після$",
            @"^ поруч$",
            @"^ перед$",
            @"^ протягом$",
            @"^ під час$",
            @"^ крізь$",
            @"^ під$", 
	#endregion
        };

        /// <summary>
        /// Парсинг даних на сайті
        /// </summary>
        /// <param name="address">Адреса сайту</param>
        /// <param name="pattern">Шаблон пошуку</param>
        /// <returns></returns>
        internal static void SaveData(string address, string pattern)
        {
            try
            {
                // Відправка запиту
                HttpWebRequest request = WebRequest.CreateHttp(address);

                // Отримання відповіді, створення потоку, створення читача і записника
                using (HttpWebResponse responce = (HttpWebResponse)request.GetResponse())
                using (Stream streamR = responce.GetResponseStream())
                using (StreamReader reader = new StreamReader(streamR, Encoding.GetEncoding(responce.CharacterSet)))
                using (Stream streamW = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(streamW, Encoding.Unicode))
                {
                    // виведення сповіщення
                    Console.WriteLine($"\n\tПочався пошук за адресою: {address}\n");

                    // збереження даних + блокування доступу до файла
                    writer.WriteLine(reader.ReadToEnd());
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

        internal static void CorectData(string path)
        {

        }

        /// <summary>
        /// Відкриття файлу з найденими даними
        /// </summary>
        internal static void OpenResultFile()
            => Process.Start(filename);

    }
}
