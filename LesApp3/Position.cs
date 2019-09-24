using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp3
{
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
                    cost = (double)Volume * Price;
                }

                if (Weigth != null)
                {
                    cost = (double)Weigth * Price;
                }

                return cost * Count;
            }
        }

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
            .Append($"{Name} ")
            .Append((Volume == null) ? string.Empty : $"{Volume:N2} л ")
            .Append((Weigth == null) ? string.Empty : $"ваг {Weigth:N2} ")
            .Append($"{((Count < 2) ? 1 : Count)} шт. ")
            .Append($"x {Price.ToString("C2", region)} = {Cost.ToString("C2", region)}")
            .ToString();

    }
}
