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
        #region NBU
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
        #endregion

        #region Position
        /// <summary>
        /// Бутилка солодкої води
        /// </summary>
        public static string WaterEN
            => "# IFresh 0,500 л 1 шт. x $7.99 = $4.00";

        /// <summary>
        /// Піцца
        /// </summary>
        public static string PizzaEN
            => "# Miami ваг 0,114 1 шт. x $199.00 = $22.69";

        /// <summary>
        /// Бутилка солодкої води
        /// </summary>
        public static string WaterUA_space
            => "# IFresh 0,50 л 1 шт. x 7,99 ₴ = 4,00 ₴";
        /// <summary>
        /// Бутилка солодкої води (без пробіла)
        /// </summary>
        public static string WaterUA
            => "# IFresh 0,500 л 1 шт. x 7,99 ₴ = 4,00 ₴";

        /// <summary>
        /// Піцца
        /// </summary>
        public static string PizzaUA_space
            => "# Miami ваг 0,11 1 шт. x 199,00 ₴ = 22,69 ₴";
        /// <summary>
        /// Піцца (без пробіла)
        /// </summary>
        public static string PizzaUA
            => "# Miami ваг 0,114 1 шт. x 199,00 ₴ = 22,69 ₴";

        /// <summary>
        /// Бутилка солодкої води
        /// </summary>
        public static string WaterENCur
            => "# IFresh 0,500 л 1 шт. x $0.33 = $0.16";

        /// <summary>
        /// Піцца
        /// </summary>
        public static string PizzaENCur
            => "# Miami ваг 0,114 1 шт. x $8.18 = $0.93";
        

        #endregion
    }
}
