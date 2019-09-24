using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0.Tests
{
    internal static class TestMethods
    {
        /// <summary>
        /// Тестування паролів
        /// </summary>
        /// <param name="stub">Заглушка</param>
        /// <param name="wait">Очікуваний результат</param>
        /// <param name="logpass">Екземпляр логіна/пароля</param>
        internal static void LogPass_Test(string stub, bool wait, IRegistration logpass)
        {
            // вихідний результат
            string result;

            // act
            bool actual = logpass.TryRegister(stub, out result);

            // assert
            if (wait)
            {
                Assert.IsTrue(actual);
            }
            else
            {
                Assert.IsFalse(actual);
                return;
            }
            //Assert.AreSame(stub, result);
            Assert.AreEqual(stub, result);
        }
    }
}
