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
    class Register : IRegister
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
        protected virtual ReadLine delReadline { get; set; } 
            = Console.ReadLine;
        /// <summary>
        /// Метод який керує повторним введенням логіна/пароля або виходом
        /// </summary>
        protected virtual ReadKey delReadKey { get; set; } 
            = Console.ReadKey;

        /// <summary>
        /// Реєстрація
        /// </summary>
        public void RegisterUser()
        {
            // очищення консолі
            Console.Clear();
            Successful = false;

            // змінна, для керування ходом реєстрації + введення логіна
            bool exit = AnalysisEnterData(new Login(), "Логін", out login);

            // перевірка чи коректно введено логін
            if (!exit)
            {
                login = string.Empty;
                return;
            }

            // введення пароля
            exit = AnalysisEnterData(new Password(), "Пароль", out password);

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
            Console.WriteLine("\n\tВітаємо з успішною реєстрацією:\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\tLogin: {Login}");
            Console.WriteLine($"\tPassword: {Password}");
            Console.ResetColor();
        }

        /// <summary>
        /// Аналіз введених даних
        /// </summary>
        /// <param name="logpass">Перевірка правильності введення логіна/пароля</param>
        /// <param name="part">Що перевіряється логін/пароль, вводити з великої букви</param>
        /// <returns></returns>
        protected bool AnalysisEnterData(IRegistration logpass, string part, out string returned)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n\tВведеіть {part.ToLower()}: ");
                Console.ResetColor();

                // аналіз введених даних
                if (logpass.TryRegister(delReadline(), out returned))
                {
                    return true;
                }

                // очищення
                returned = string.Empty;

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
