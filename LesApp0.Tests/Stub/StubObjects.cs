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
            => "Login5";
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

        #region Password
        /// <summary>
        /// Правильний ввід
        /// </summary>
        public static string PasswordTrue
            => "Password";
        /// <summary>
        /// Цифра
        /// </summary>
        public static string PasswordFalse0
            => "Password27";
        /// <summary>
        /// Не латинський алфавіт
        /// </summary>
        public static string PasswordFalse1
            => "Логін";
        /// <summary>
        /// Пустий рядок
        /// </summary>
        public static string PasswordFalse2
            => string.Empty;
        /// <summary>
        /// Пробіл
        /// </summary>
        public static string PasswordFalse3
            => " ";
        /// <summary>
        /// Nullable
        /// </summary>
        public static string PasswordFalse4
            => null;
        /// <summary>
        /// Недопустимі знаки
        /// </summary>
        public static string PasswordFalse5
            => "Password#";
        #endregion

        #region Data for AnalysisEnterData
        #region Правильний ввід ЛП
        /// <summary>
        /// Правильний ввід ЛП
        /// </summary>
        public static Queue<string> Register_tLP
           => new Queue<string>(new List<string>()
           {
               LoginTrue,
               PasswordTrue
           });
        /// <summary>
        /// Правильний ввід Keys
        /// </summary>
        public static Queue<ConsoleKeyInfo> Register_Key_tLP
           => new Queue<ConsoleKeyInfo>(new List<ConsoleKeyInfo>()
           {
               new ConsoleKeyInfo((char)ConsoleKey.Y, ConsoleKey.Y, false, false, false),
               new ConsoleKeyInfo((char)ConsoleKey.Y, ConsoleKey.Y, false, false, false),
           });
        #endregion

        #region Неправильний ввід ЛП
        /// <summary>
        /// Неправильний ввід ЛП
        /// </summary>
        public static Queue<string> Register_fLP
           => new Queue<string>(new List<string>()
           {
               LoginFalse5,
               PasswordFalse5
           });
        /// <summary>
        /// Неправильний ввід Keys
        /// </summary>
        public static Queue<ConsoleKeyInfo> Register_Key_fLP
           => new Queue<ConsoleKeyInfo>(new List<ConsoleKeyInfo>()
           {
               new ConsoleKeyInfo((char)ConsoleKey.N, ConsoleKey.N, false, false, false),
               new ConsoleKeyInfo((char)ConsoleKey.N, ConsoleKey.N, false, false, false),
           });
        #endregion

        #endregion

    }
}
