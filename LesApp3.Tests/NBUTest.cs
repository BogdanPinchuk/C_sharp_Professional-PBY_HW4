using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LesApp3.Tests.Resources;

namespace LesApp3.Tests
{
    [TestClass]
    public class NBUTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            // arrange

            // Перемикаємо виведення в консолі на виведення в отладчик
            NBU.WriteLine = s => Debug.WriteLine(s);
        }

        [TestMethod]
        public void NBUTest_ENRate_TrueReturned()
            => NBUTestMethod(StubObjects.ENRates);

        [TestMethod]
        public void NBUTest_UARate_TrueReturned()
            => NBUTestMethod(StubObjects.UARates);

        /// <summary>
        /// Спільний метод тестування різних версій сайтів
        /// </summary>
        /// <param name="stub">заглушка</param>
        private void NBUTestMethod(string stub)
        {
            // act
            UpdateFile(stub);

            // assert
            Assert.AreEqual(StubObjects.Unit, NBU.Unit);
            Assert.AreEqual(StubObjects.Rate, NBU.Rate);

            // виведення результатів
            Debug.WriteLine(NBU.ToString());
        }

        /// <summary>
        /// Оновлення курсу валют через текстовий файл який має кодування із сайту НБУ банку
        /// </summary>
        /// <param name="stub">Заглушка - копія сайту НБУ</param>
        internal static void UpdateFile(string stub)
        {
            try
            {
                // Отримання відповіді, створення потоку, створення читача і записника
                using (FileStream stream = new FileStream(stub, FileMode.Open, FileAccess.Read))
                {
                    NBU.GetData(stream, Encoding.GetEncoding(stub));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
