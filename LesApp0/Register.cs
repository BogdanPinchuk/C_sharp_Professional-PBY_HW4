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
        /// Делегат для введення логіна/пароля
        /// </summary>
        /// <returns></returns>
        protected delegate string ReadLine();
        /// <summary>
        /// Делегат на вихід із перевіки логіна/пароля
        /// </summary>
        /// <param name="hide">Не показувати натиснуту клавішу</param>
        /// <returns></returns>
        protected delegate ConsoleKeyInfo ReadKey(bool hide);

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
        public bool Successful { get; private set; }
        /// <summary>
        /// Логін
        /// </summary>
        public string Login => login;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password => password;
        /// <summary>
        /// Метод для введення логіна/пароля
        /// </summary>
        protected ReadLine delReadline { get; set; } 
            = Console.ReadLine;
        /// <summary>
        /// Метод який керує повторним введенням логіна/пароля або виходом
        /// </summary>
        protected ReadKey delReadKey { get; set; } 
            = Console.ReadKey;

        /// <summary>
        /// Конструктор для реєстрації
        /// </summary>
        public void RegisterUser()
        {
            // очищення консолі
            Console.Clear();
            Successful = false;

            // змінна, для керування ходом реєстрації + введення логіна
            bool exit = AnalysisEnterData(new Login(), "Логін");

            // перевірка чи коректно введено логін
            if (!exit)
            {
                login = string.Empty;
                return;
            }

            // введення пароля
            exit = AnalysisEnterData(new Password(), "Пароль");

            // перевірка чи коректно введено пароль
            if (!exit)
            {
                login = string.Empty;
                password = string.Empty;
                return;
            }
            
            // вивід результату
            Console.Clear();
            Successful = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tВітаємо з успішною реєстрацією: ");
            Console.ResetColor();
            Console.WriteLine($"\tLogin: {Login}");
            Console.WriteLine($"\tPassword: {Password}");
        }

        /// <summary>
        /// Аналіз введених даних
        /// </summary>
        /// <param name="logpass">Перевірка правильності введення логіна/пароля</param>
        /// <param name="part">Що перевіряється логін/пароль, вводити з великої букви</param>
        /// <returns></returns>
        protected bool AnalysisEnterData(IRegistration logpass, string part)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n\tВведеіть {part.ToLower()}: ");
                Console.ResetColor();

                // аналіз введених даних
                if (logpass.TryRegister(delReadline(), out login))
                {
                    return true;
                }

                // очищення
                login = string.Empty;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n\t{part} введено невірно. Повторити введення? [y, n]");
                Console.ResetColor();

                var key = delReadKey(true).Key;

                if (key == ConsoleKey.Y)
                {
                    continue;
                }
                else if (key == ConsoleKey.N)
                {
                    return false;
                }
            }
        }

    }
}
