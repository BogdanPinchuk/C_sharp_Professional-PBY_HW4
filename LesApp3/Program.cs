using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LesApp3
{
    class Program
    {
        static void Main()
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            Position product = new Position()
            {
                Count = 1,
                Name = "IFresh",
                Price = 7.99,
                Volume = 0.5,
                Region = "en-US"
            };

            Position product0 = new Position()
            {
                Count = 1,
                Name = "Pizza",
                Price = 199.00,
                Weigth = 0.114,
                Region = "en-US"
            };

            Console.WriteLine(product.ToString());
            Console.WriteLine(product0.ToString());

            // delay
            Console.ReadKey(true);
        }
    }
}
