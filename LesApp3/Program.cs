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

            

            // delay
            Console.ReadKey(true);
        }
    }
}
