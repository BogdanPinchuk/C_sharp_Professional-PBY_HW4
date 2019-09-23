using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesApp1
{
    class Program
    {
        static void Main()
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            // Задаємо адресу
            string address = "https://pbf.kpi.ua/ua/contacts/";

            // робимо пошук
            Search.ParsingSite(address, Search.ObjectForSearch.Link);
            Search.ParsingSite(address, Search.ObjectForSearch.Phone);
            Search.ParsingSite(address, Search.ObjectForSearch.Email);

            // відкриваємо результат
            Search.OpenResultFile();

            // delay
            //Console.ReadKey(true);
        }
    }
}
