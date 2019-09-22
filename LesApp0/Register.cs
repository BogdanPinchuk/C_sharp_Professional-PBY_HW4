using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    /// <summary>
    /// Реєстрація
    /// </summary>
    class Register
    {
        /// <summary>
        /// Перевірка логіна/пароля
        /// </summary>
        private IRegistration logpass;
        /// <summary>
        /// Логін
        /// </summary>
        private string login = string.Empty;
        /// <summary>
        /// Пароль
        /// </summary>
        private string password = string.Empty;

        /// <summary>
        /// Успішність реєстрації
        /// </summary>
        public bool Successful { get; private set; } = false;
        /// <summary>
        /// Логін
        /// </summary>
        public string Login => login;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password => password;

        /// <summary>
        /// Конструктор для реєстрації
        /// </summary>
        public Register()
        {
            // очищення консолі
            Console.Clear();

            // установка перевірки логіна
            logpass = new Login();

            // логін
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\tВведеіть логін: ");
                Console.ResetColor();

                // аналіз введених даних
                if (logpass.TryRegister(Console.ReadLine(), out login))
                {
                    break;
                }

                // очищення
                login = string.Empty;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n\tЛогін введено невірно. Повторити введення? [y, n]");
                Console.ResetColor();

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.Y)
                {
                    continue;
                }
                else if (key == ConsoleKey.N)
                {
                    return;
                }
            }

            // установка перевірки пароля
            logpass = new Password();

            // пароль
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\tВведеіть пароль: ");
                Console.ResetColor();

                // аналіз введених даних
                if (logpass.TryRegister(Console.ReadLine(), out password))
                {
                    break;
                }

                // очищення
                password = string.Empty;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n\tПароль введено невірно. Повторити введення? [y, n]");
                Console.ResetColor();

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.Y)
                {
                    continue;
                }
                else if (key == ConsoleKey.N)
                {
                    login = string.Empty;
                    return;
                }
            }

            // вивід результату
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tВітаємо з успішною реєстрацією: ");
            Console.ResetColor();
            Console.WriteLine($"\tLogin: {Login}");
            Console.WriteLine($"\tPassword: {Password}");
        }

    }
}
