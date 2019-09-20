using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
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
        bool TryRegister(string data);
    }
}
