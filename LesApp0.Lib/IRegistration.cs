using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// дозвіл доступу для тестування
[assembly: InternalsVisibleTo("LesApp0.Tests")]

namespace LesApp0.Lib
{
    /// <summary>
    /// Реєстрація за певними даними
    /// </summary>
    interface IRegistration
    {
        /// <summary>
        /// Реєстрація
        /// </summary>
        /// <param name="data">Вхідні дані: логін/пароль...</param>
        /// <returns></returns>
        bool TryRegister(string data, out string result);

    }
}
