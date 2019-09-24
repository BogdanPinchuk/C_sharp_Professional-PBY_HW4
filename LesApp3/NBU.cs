using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Делегат для виведення інформації
        /// </summary>
        /// <param name="value">текстова інформація</param>
        internal delegate void DelWriteLine(string value);
        // Примітка. Коли запускати на виконання код із тестування, де не моде запускатись консольне  вікно
        // виводитиметься наступна помилка: (Приклад спеціально оставлений Register_EntryTLP)
        // Сообщение: 
        // Test method LesApp0.Tests.CheckRegister.Register_EntryTLP threw exception: 
        // System.IO.IOException: The handle is invalid.
        // Трассировка стека: 
        // в __Error.WinIOError(Int32 errorCode, String maybeFullPath)
        // в Console.GetBufferInfo(Boolean throwOnNoConsole, Boolean& succeeded)
        // в Console.Clear()
        // Щоб цього не було, просто можна скористатися делегатами подібно до інтерфейсів
        // і підміняти фінкції ввиведення в залежності від середовища виконання
        // наприклад для тестів подібний виклик Console.WriteLine() - Debug.WriteLine()
        // Якщо запускати в режимі виконання тесту - помилка, якщо в режимі отладки - все нормально

        /// <summary>
        /// Офіційна адреса сайта НБУ
        /// </summary>
        private static readonly string address =
            "https://bank.gov.ua/markets/exchangerates/";

        /// <summary>
        /// Дата оновлення курсу валют за НБУ
        /// </summary>
        internal static DateTime Date { get; private set; }
            = new DateTime(2019, 09, 24);
        /// <summary>
        /// Курс валюти по відношенню до гривень
        /// </summary>
        internal static double Rate { get; private set; }
            = 2432.7195;
        /// <summary>
        /// Кількість одиниць у валюті
        /// </summary>
        internal static int Unit { get; private set; }
            = 100;
        /// <summary>
        /// Код літерний
        /// </summary>
        internal static string Code { get; set; }
            = "USD";

        /// <summary>
        /// Виведення переданого рядка
        /// </summary>
        internal static DelWriteLine WriteLine { get; set; }
            = Console.WriteLine;

        /// <summary>
        /// Оновлення курсу валют через НБУ
        /// </summary>
        internal static void Update()
        {
            try
            {
                // Відправка запиту
                HttpWebRequest request = WebRequest.CreateHttp(address);

                // Отримання відповіді, створення потоку, створення читача і записника
                using (HttpWebResponse responce = (HttpWebResponse)request.GetResponse())
                using (Stream stream = responce.GetResponseStream())
                {
                    GetData(stream, Encoding.GetEncoding(responce.CharacterSet));
                }
                
            }
            catch (WebException ex)
            {
                WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Отримання даних із потоку
        /// </summary>
        /// <param name="stream">Стрім</param>
        /// <param name="code">кодіровка</param>
        internal static void GetData(Stream stream, Encoding code)
        {
            try
            {
                using (StreamReader reader = new StreamReader(stream, code))
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
                            WriteLine($"\nКурс валют оновлено.");
                        }

                    } while (line != null);
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Конвертація із гривень в іноземну валюту
        /// </summary>
        /// <param name="currency">Гроші</param>
        /// <returns></returns>
        internal static double ConvertTo(double currency)
            => currency * Unit / Rate;

        /// <summary>
        /// Конвертація із іноземної валюти в гривні
        /// </summary>
        /// <param name="currency">Гроші</param>
        /// <returns></returns>
        internal static double ConvertFrom(double currency)
            => currency * Rate / Unit;

        /// <summary>
        /// Вивід інформації про курс валют
        /// </summary>
        /// <returns></returns>
        internal static new string ToString()
            => new StringBuilder("\nNational Bank of Ukraine\n")
            .Append($"\n\tDate: " + Date.ToShortDateString())
            .Append($"\n\tCode alpha: {Code}")
            .Append($"\n\tUnit: {Unit:N0}")
            .Append($"\n\tRate: {Rate:N4}")
            .ToString();

    }
}
