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
            Thread[] threads = new Thread[]
            {
                new Thread(() => Search.ParsingSite(address, Search.ObjectForSearch.Link)),
                new Thread(() => Search.ParsingSite(address, Search.ObjectForSearch.Phone)),
                new Thread(() => Search.ParsingSite(address, Search.ObjectForSearch.Email))
            };

            // запускаємо
            foreach (var thread in threads)
            {
                thread.Start();
            }

            // чекаємо
            foreach (var thread in threads)
            {
                thread.Join();
            }

            // відкриваємо результат
            Search.OpenResultFile();

            // delay
            //Console.ReadKey(true);
        }
    }
}
