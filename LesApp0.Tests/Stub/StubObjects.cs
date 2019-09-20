using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0.Tests.Stub
{
    /// <summary>
    /// Заглушки
    /// </summary>
    internal static class StubObjects
    {
        #region Login
        /// <summary>
        /// Правильний ввід
        /// </summary>
        public static string LoginTrue
            => "Login";
        /// <summary>
        /// Цифра
        /// </summary>
        public static string LoginFalse0
            => "Login0";
        /// <summary>
        /// Не латинський алфавіт
        /// </summary>
        public static string LoginFalse1
            => "Логін";
        /// <summary>
        /// Пустий рядок
        /// </summary>
        public static string LoginFalse2
            => string.Empty;
        /// <summary>
        /// Пробіл
        /// </summary>
        public static string LoginFalse3
            => " ";
        /// <summary>
        /// Nullable
        /// </summary>
        public static string LoginFalse4
            => null;
        /// <summary>
        /// Недопустимі знаки
        /// </summary>
        public static string LoginFalse5
            => "Login#";
        #endregion

    }
}
