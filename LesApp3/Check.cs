using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LesApp3
{
    /// <summary>
    /// Створення чека
    /// </summary>
    class Check
    {
        /// <summary>
        /// Культура
        /// </summary>
        private CultureInfo region;
        /// <summary>
        /// Для випадкових чисел
        /// </summary>
        private readonly Random rnd = new Random();

        /// <summary>
        /// Список продуктів - покупок
        /// </summary>
        public List<Position> Products { get; set; }
        /// <summary>
        /// Регіональні налаштування
        /// </summary>
        public string Region
        {
            get { return (region ?? new CultureInfo("uk-UA")).Name; }
            set { region = new CultureInfo(value); }
        }

        /// <summary>
        /// Установка доларової валюти
        /// </summary>
        public void SetDolar()
        {
            Region = "en-US";
            Money = Currency.Other;
        }

        /// <summary>
        /// Назва компанії
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Адреса
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Додаткова інформація
        /// </summary>
        public string AddInfo { get; set; }
        /// <summary>
        /// Дата створення чека
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// Номер каси
        /// </summary>
        public int Cashier { get; set; }
        /// <summary>
        /// Чек
        /// </summary>
        public string Receipt { get; set; }

        /// <summary>
        /// Тип валюти в залежнсоті від регіону
        /// </summary>
        public Currency Money { get; set; }

        /// <summary>
        /// Сформувати чек за замовчуванням
        /// </summary>
        public Check()
        {
            Products = new List<Position>();
            Date = DateTime.Now;
            CompanyName = "Pinchuk";
            Address = "c. Kyiv";
            AddInfo = "My home work";
            Cashier = rnd.Next(0, short.MaxValue);
            Receipt = rnd.Next(0, int.MaxValue).ToString();
            Money = Currency.Hryvnia;
        }

        /// <summary>
        /// Сформувати чек із текстового файла (вказувати без розширення файла .txt)
        /// </summary>
        /// <param name="path"></param>
        public Check(string path)
        {
            Products = new List<Position>();
            RecognitionCheckFile(path);
        }

        /// <summary>
        /// Збереження чека в файл
        /// </summary>
        /// <param name="path">повна назва файла без розширення</param>
        public void SaveCheckFile(string path)
        {
            if (Products.Count == 0)
                return;

            try
            {
                using (Stream stream = new FileStream(path + ".txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                {
                    writer.WriteLine(this.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Розпізнавання файла чека
        /// </summary>
        /// <param name="path">повна назва файла без розширення</param>
        public void RecognitionCheckFile(string path)
        {
            if (!File.Exists(path + ".txt"))
                return;

            try
            {
                using (Stream stream = new FileStream(path + ".txt", FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                {
                    // змінна даних для розпізнавання
                    string data;

                    // очищення списку
                    Products.Clear();

                    // припускаємо, що користувач не залазив у файл і не міняв структури
                    #region Шапка
                    // розпізнавання файла
                    data = reader.ReadLine();
                    CompanyName = data.Replace("Compaty: ", string.Empty);
                    data = reader.ReadLine();
                    Address = data.Replace("Address: ", string.Empty);
                    data = reader.ReadLine();
                    AddInfo = data.Replace("Info: ", string.Empty);
                    reader.ReadLine();
                    data = reader.ReadLine();
                    Cashier = int.Parse(data.Replace("Cashier: ", string.Empty));
                    data = reader.ReadLine();
                    Receipt = data.Replace("Receipt: ", string.Empty);
                    data = reader.ReadLine();
                    Date = DateTime.Parse(data.Replace("Date: ", string.Empty));
                    reader.ReadLine();
                    #endregion

                    #region Продукти
                    Position product;
                    data = reader.ReadLine();
                    while (!Regex.IsMatch(data, @"^-+"))
                    {
                        product = new Position();
                        string line;

                        #region Назва продукта (якщо є лише вага)
                        if (Regex.IsMatch(data, @"# [ \w]+ ваг"))
                        {
                            line = Regex.Match(data, @"# [ \w]+ ваг").Value;
                            line = line.Replace("ваг", string.Empty)
                                .TrimStart('#').Trim(' ');
                            product.Name = line;
                            data = data.Replace(line, string.Empty).TrimStart('#').TrimStart(' ');

                            // знаходимо вагу
                            if (Regex.IsMatch(data, @"ваг \d+[.,]\d+ "))
                            {
                                line = Regex.Match(data, @"ваг \d+[.,]\d+ ").Value;
                                data = data.Replace(line, string.Empty).TrimStart(' ');
                                line = line.Replace("ваг", string.Empty);
                                product.Weigth = double.Parse(line.Replace(".", ",").Trim(' '));
                            }
                        }
                        #endregion

                        #region Назва продукта (якщо є лише об'єм)
                        if (Regex.IsMatch(data, @"# [ \w]+ \d+[.,]\d+"))
                        {
                            line = Regex.Match(data, @"# [ \w]+ \d+[.,]\d+").Value;
                            line = Regex.Replace(line, @"\d+[.,]\d+", string.Empty)
                                .TrimStart('#').Trim(' ');
                            product.Name = line;
                            data = data.Replace(line, string.Empty).TrimStart('#').TrimStart(' ');

                            // знаходимо об'єм
                            if (Regex.IsMatch(data, @"\d+[.,]\d+ л"))
                            {
                                line = Regex.Match(data, @"\d+[.,]\d+ л").Value;
                                data = data.Replace(line, string.Empty).TrimStart(' ');
                                line = line.Replace("л", string.Empty);
                                product.Volume = double.Parse(line.Replace(".", ",").Trim(' '));
                            }
                        }
                        #endregion

                        #region Назва продукта (якщо немає ні ваги ні об'єму)
                        if (Regex.IsMatch(data, @"# [ \w]+ \d+ шт."))
                        {
                            line = Regex.Match(data, @"# [ \w]+ \d+ шт.").Value;
                            line = Regex.Replace(line, @"\d+ шт.", string.Empty)
                                .TrimStart('#').Trim(' ');
                            product.Name = line;
                            data = data.Replace(line, string.Empty).TrimStart('#').TrimStart(' ');
                        }
                        #endregion

                        #region Кількість товару "штук"
                        if (Regex.IsMatch(data, @"\d+ шт."))
                        {
                            line = Regex.Match(data, @"\d+ шт.").Value;
                            data = data.Replace(line, string.Empty).TrimStart(' ');
                            line = line.Replace("шт.", string.Empty).Trim(' ');
                            product.Count = int.Parse(line);
                        }
                        #endregion

                        #region Ціна продукту
                        if (Regex.IsMatch(data, @"x[ \W]+\d+[.,]\d+[ \W]+="))
                        {
                            line = Regex.Match(data, @"x[ \W]+\d+[.,]\d+[ \W]+=").Value;
                            #region Валюта
                            // http://qaru.site/questions/8953198/regex-to-separate-different-currencies-and-numeric-value
                            // Примітка. На той випадок якщо необхідно буде розширити програму,
                            // але згідно умови сказано, що чек на українській мові, точніше в гривневому еквіваленті
#if false
                            {
                                string pat = @"((?<CurrencySymbol>(?<=\s)[^\d\-+\.,]{1,3}) *(?<CurrencyValue>[0-9\.,]+))|((?<CurrencyValue>[0-9\.,]+) *(?<CurrencyCode>[A-Z]{3}))";
                                string valute = Regex.Match(line, pat).Groups["CurrencySymbol"].Value;
                            } 
#endif
                            // додатковий патерн @"(\P{Sc})+"
                            // https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/character-classes-in-regular-expressions
                            #endregion
                            data = data.Replace(line, string.Empty).TrimStart(' ');
                            line = Regex.Match(line, @"\d+[.,]\d+").Value;
                            product.Price = double.Parse(line.Replace(".", ",").Trim(' '));
                        }
                        #endregion

                        // занесення продукту в список
                        Products.Add(product);
                        data = reader.ReadLine();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public override string ToString()
        {
            // шапка
            var check = new StringBuilder($"Compaty: {CompanyName}\n")
                .Append($"Address: {Address}\n")
                .Append($"Info: {AddInfo}\n")
                .Append(new string('-', 27) + "\n")
                .Append($"Cashier: {Cashier}\n")
                .Append($"Receipt: {Receipt}\n")
                .Append($"Date: {Date.ToString()}\n")
                .Append(new string('-', 27) + "\n");

            // тимчасова змінна для втсановлення параметрів відобаження
            Position temp;
            // загальна сума і ПДВ
            double sum = default(double),
                vat = default(double);

            foreach (var product in Products)
            {
                temp = product;

                // устновка властивостей
                temp.Region = this.Region;
                temp.Money = this.Money;

                // розрахунок суми і ПДВ
                sum += temp.Cost;
                vat += temp.VAT;

                // додавання продуктів в чек
                check.Append(temp.ToString() + "\n");
            }

            check.Append(new string('-', 27) + "\n");

            // виведення суми і ПДВ
            check.Append($"Total: {sum.ToString("C2", region)}\n")
                .Append($"VAT: {vat.ToString("C2", region)}\n");

            return check.ToString();
        }

    }
}
