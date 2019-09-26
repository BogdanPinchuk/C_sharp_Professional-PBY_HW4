using System;
using System.Collections.Generic;
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

            string data = "# IFresh fresh 0,50 л 1 шт. x 7,99₴ = 4,00₴";
            Console.WriteLine(data);

            Console.WriteLine(Regex.Match(data, @"# [ \w]+ ").Value.Trim('#').Trim(' '));

            // delay
            Console.ReadKey(true);
        }
    }
}
