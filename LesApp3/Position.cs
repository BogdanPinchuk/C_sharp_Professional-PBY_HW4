using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Примітка. В структуру заносяться дані в гривнях, лише на виведенні на чек
// дані вітоматично конвертуватимуться у валюту згідно курсу НБУ

namespace LesApp3
{
    /// <summary>
    /// Тип валюти
    /// </summary>
    public enum Currency
    {
        /// <summary>
        /// Гривні
        /// </summary>
        Hryvnia,
        /// <summary>
        /// Іноземна валюта
        /// </summary>
        Other
    }

    /// <summary>
    /// Товар замовлення
    /// </summary>
    struct Position
    {
        /// <summary>
        /// Культура
        /// </summary>
        private CultureInfo region;

        /// <summary>
        /// Регіональні налаштування
        /// </summary>
        public string Region
        {
            get { return (region ?? new CultureInfo("uk-UA")).Name; }
            set { region = new CultureInfo(value); }
        }

        /// <summary>
        /// Назва
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Об'єм
        /// </summary>
        public double? Volume { get; set; }
        /// <summary>
        /// Вага
        /// </summary>
        public double? Weigth { get; set; }
        /// <summary>
        /// Кількість товару
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Нормована ціна за одиницю/вагу/об'єм товару
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Загальна сума/вартість
        /// </summary>
        public double Cost
        {
            get
            {
                double cost = default(double);
                // Примітка. В деяких магазинах об'єм іде як довідкова інфа
                // але в деяких продають по об'єму, тому нехай об'єм пишуть суди, або
                // ж просто в назву товару, тому в даному випадку враховуватиметься об'єм
                // і на коритувачу лишається відповідальність за ввід ваги або об'єму
                if (Volume != null)
                {
                    cost = (double)Volume;
                }

                if (Weigth != null)
                {
                    cost = (double)Weigth;
                }

                cost = (cost == 0 ? 1 : cost) * Count * Price;
                return (Money == Currency.Hryvnia) ? cost : NBU.ConvertTo(cost);
            }
        }
        /// <summary>
        /// Тип валюти в залежнсоті від регіону
        /// </summary>
        public Currency Money { get; set; }
  
        /// <summary>
        /// Податок (ПДВ - Value-added tax)
        /// </summary>
        public double VAT
            => 0.2 * Cost;

        /// <summary>
        /// Виведення інформації в кодованому вигляді
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => new StringBuilder("# ")
            .Append($"{((Name == string.Empty || Name == null) ? "None" : Name)} ")
            .Append((Volume == null) ? string.Empty : $"{Volume:N3} л ")
            .Append((Weigth == null) ? string.Empty : $"ваг {Weigth:N3} ")
            .Append($"{((Count < 2) ? 1 : Count)} шт. ")
            .Append($"x {((Money == Currency.Hryvnia) ? Price : NBU.ConvertTo(Price)).ToString("C2", region)} = ")
            .Append($"{Cost.ToString("C2", region)}")
            .ToString();

    }
}
