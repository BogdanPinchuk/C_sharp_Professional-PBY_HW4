using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    class Program
    {
        static void Main()
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            // запуск режстрації
            new Register();

            // створено по слабкій ссилці, так як не передбачається
            // в умові додаткових дій, а якщо необхідно робити розширення
            // то теобхідно створити екземпляр, з якого можна буде зчитати дані 
            // реєстрації і зберегти наприклад в БД (базу даних)

            // delay
            Console.ReadKey(true);
        }
    }
}
