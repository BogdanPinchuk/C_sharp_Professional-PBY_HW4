using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LesApp3
{
    /// <summary>
    /// National Bank of Ukraine
    /// </summary>
    static class NBU
    {
        /// <summary>
        /// Офіційна адреса сайта НБУ
        /// </summary>
        private static readonly string address =
            "https://bank.gov.ua/markets/exchangerates/";

        /// <summary>
        /// Дата оновлення курсу валют за НБУ
        /// </summary>
        public static DateTime Date { get; private set; }
            = new DateTime(2019, 09, 24);
        /// <summary>
        /// Курс валюти по відношенню до гривень
        /// </summary>
        public static double Rate { get; private set; }
            = 2432.7195;
        /// <summary>
        /// Кількість одиниць у валюті
        /// </summary>
        public static int Unit { get; private set; }
            = 100;
        /// <summary>
        /// Код літерний
        /// </summary>
        public static string Code { get; set; }
            = "USD";

        /// <summary>
        /// Оновлення курсу валют через НБУ
        /// </summary>
        public static void Update()
        {
            try
            {
                // Відправка запиту
                HttpWebRequest request = WebRequest.CreateHttp(address);

                // Отримання відповіді, створення потоку, створення читача і записника
                using (HttpWebResponse responce = (HttpWebResponse)request.GetResponse())
                using (Stream stream = responce.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(responce.CharacterSet)))
                {
                    // рядок даних для аналізу
                    string line = string.Empty;

                    do
                    {
                        // зчитуємо рядок
                        line = reader.ReadLine();

                        // перевірка наявності необхідного шаблону
                        if (Regex.IsMatch(line, $@">{Code}<"))
                        {
                            // зчитуємо рядок з кількістю грн
                            line = reader.ReadLine();
                            // перезаписуємо
                            line = Regex.Match(line, $@"\d+").Value;

                            // записуємо кількість одиниць
                            int unit;
                            if (int.TryParse(line, out unit))
                            {
                                Unit = unit;
                            }

                            // пропускаємо рядок і зчитуємо ще один
                            reader.ReadLine();
                            line = reader.ReadLine();

                            // перезаписуємо
                            line = Regex.Match(line, @"\d+[.,]\d+").Value;

                            // записуємо курс
                            double rate;
                            if (double.TryParse(line.Replace(".", ","), out rate))
                            {
                                Rate = rate;
                            }

                            // виведення сповіщення
                            Date = DateTime.Now;
                            Console.WriteLine($"\nКурс валют оновлено.");
                        }

                    } while (line != null);
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
        /// Вивід інформації про курс валют
        /// </summary>
        /// <returns></returns>
        public static new string ToString()
            => new StringBuilder("\n\tNational Bank of Ukraine")
            .Append($"\nDate: " + Date.ToShortDateString())
            .Append($"\nCode alpha: {Code}")
            .Append($"\nUnit: {Unit:N0}")
            .Append($"\nRate: {Rate:N4}")
            .ToString();

    }
}
