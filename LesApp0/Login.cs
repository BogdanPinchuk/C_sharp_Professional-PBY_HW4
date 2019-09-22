using System;
using System.Text.RegularExpressions;

namespace LesApp0
{
    /// <summary>
    /// Логін
    /// </summary>
    internal class Login : IRegistration
    {
        // шаблон перевірки
        private readonly string pattern = @"^[A-Za-z]+$";

        /// <summary>
        /// Перевірка логіна
        /// </summary>
        /// <param name="data">вхідні дані</param>
        /// <param name="result">Повернення логіна (+обрізка пробілів)</param>
        /// <returns></returns>
        public bool TryRegister(string data, out string result)
        {
            if (string.IsNullOrEmpty(data))
            {
                result = string.Empty;
                return false;
            }

            // екземпляр класа RegEx
            var regex = new Regex(pattern);

            // виведення результату
            result = data.Trim(' ');

            // перевірка відповідності шаблону
            var res =  regex.IsMatch(result);
            return res;
        }

    }
}