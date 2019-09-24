using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// дозвіл доступу для тестування
[assembly: InternalsVisibleTo("LesApp2.Tests")]

namespace LesApp2
{
    class Program
    {
        static void Main()
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            // Задаємо адресу (де є якість приклади)
            string //address0 = "https://uk.wikipedia.org/wiki/Прийменник",
                address1 = "https://zno.if.ua/?p=2716";

            // видаляємо файл
            Search.DeleteFile();

            // скачуємо дані в файл
            Search.CreateFile(address1);

            // відкриваємо файл для пергляду
            Search.OpenResultFile();

            // корегуємо дані
            Search.CorectData();

            // delay
            Console.ReadKey(true);

            // відкриваємо файл для пергляду
            Search.OpenResultFile(); 


            // delay
            Console.ReadKey(true);
        }
    }
}
