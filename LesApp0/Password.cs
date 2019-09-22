using System.Text.RegularExpressions;

namespace LesApp0
{
    /// <summary>
    /// Пароль
    /// </summary>
    internal class Password : IRegistration
    {
        // шаблон перевірки (\w - не підходть так як нічого не скахано про символ підкреслення в умові)
        private readonly string pattern = @"^[A-Za-z0-9]+$";

        /// <summary>
        /// Перевірка пароля
        /// </summary>
        /// <param name="data">вхідні дані</param>
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
            var res = regex.IsMatch(result);
            return res;
        }
    }
}