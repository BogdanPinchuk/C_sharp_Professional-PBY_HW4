using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// дозвіл доступу для тестування
[assembly: InternalsVisibleTo("LesApp3.Tests")]

namespace LesApp3
{
    class Program
    {
        static void Main()
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            // адрес файла
            string path = "CheckFile";

            // Оновлюємо курс валют згідно НБУ - з офіційного сайту
            NBU.Update();
            // Виводимо курс валют
            Show(NBU.ToString());
            Console.WriteLine("\n" + new string('#', 80) + "\n");

            // Створення "текстового чеку" - електроного чека і збереження його в файл
            Check check = new Check();

            #region Занесення даних
            check.Products.Add(new Position()
            {
                Name = "IFresh",
                Count = 1,
                Price = 7.99,
                Volume = 0.5,
            });
            check.Products.Add(new Position()
            {
                Name = "Meat",
                Count = 1,
                Price = 225.00,
                Weigth = 0.575,
            });
            check.Products.Add(new Position()
            {
                Name = "Bread white",
                Count = 1,
                Price = 15.00,
            }); 
            #endregion

            // збереження
            check.SaveCheckFile(path);

            // показуємо файл
            //Process.Start(path + ".txt");
            //Console.ReadKey(true);

            // створення нового електронного чека з одночасним розпізнаванням
            Check newCheck = new Check(path);

            // виведення укр версії в консоль
            Show("\tУкраїнська версія (в гривнях):\n");
            Console.WriteLine(newCheck.ToString());

            // виведення англ версії в консоль
            newCheck.SetDolar();
            Show("\tАмериканська версія (в доларах):\n");
            Console.WriteLine(newCheck.ToString());

            // delay
            Console.ReadKey(true);
        }

        /// <summary>
        /// Показ повідомлення в зеленому кольорі
        /// </summary>
        /// <param name="s">Повідомлення</param>
        private static void Show(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }
}
