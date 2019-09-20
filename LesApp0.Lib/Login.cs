using System;
using System.Text.RegularExpressions;

namespace LesApp0.Lib
{
    /// <summary>
    /// Логін
    /// </summary>
    internal class Login : IRegistration
    {
        // шаблон перевірки
        private readonly string pattern = @"[A-Za-z]";

        /// <summary>
        /// Перевірка логіна
        /// </summary>
        /// <param name="data">вхідні дані</param>
        /// <param name="result">Повернення логіна (+обрізка пробілів)</param>
        /// <returns></returns>
        public bool TryRegister(string data, out string result)
        {
            // екземпляр класа RegEx
            var regex = new Regex(pattern);

            // виведення результату
            result = data.Trim(' ');

            // перевірка відповідності шаблону
            return regex.IsMatch(result);
        }

    }
}