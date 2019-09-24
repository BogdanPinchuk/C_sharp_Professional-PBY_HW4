using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp3.Tests.Resources
{
    /// <summary>
    /// Заглушки
    /// </summary>
    internal static class StubObjects
    {
        /// <summary>
        /// Англійська версія сайту
        /// </summary>
        public static string ENRates
            => Properties.Resources.ENRate;

        /// <summary>
        /// Українська версія сайту
        /// </summary>
        public static string UARates
            => Properties.Resources.UARate;

        /// <summary>
        /// Кількість одиниць у валюті
        /// </summary>
        public static int Unit
            => 100;
        /// <summary>
        /// Курс валюти по відношенню до гривень
        /// </summary>
        public static double Rate
            => 2432.7195;
    }
}
