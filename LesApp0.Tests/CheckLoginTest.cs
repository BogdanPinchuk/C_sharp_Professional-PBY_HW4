using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LesApp0.Lib;
using LesApp0.Tests.Stub;

namespace LesApp0.Tests
{
    [TestClass]
    public class CheckLoginTest
    {
        /// <summary>
        /// Екземпляр для логіна
        /// </summary>
        private static IRegistration login;
        /// <summary>
        /// Вихідний результат
        /// </summary>
        private string result;

        /// <summary>
        /// Запускається один раз перед запуском петодів
        /// </summary>
        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            // arrange
            login = new Login();
        }

        /// <summary>
        /// Зепускається перед виконанням кожного методу
        /// </summary>
        [TestInitialize]
        public void InitializeTests()
        {
            // arrange
            result = string.Empty;
        }

        [TestMethod]
        public void Login_SuccessEntry_TrueReturn()
            => Login_Test(StubObjects.LoginTrue, true);

        [TestMethod]
        public void Login_AddNumber_FalseReturn()
            => Login_Test(StubObjects.LoginFalse0, false);

        [TestMethod]
        public void Login_AddOtherLetter_FalseReturn()
            => Login_Test(StubObjects.LoginFalse1, false);

        [TestMethod]
        public void Login_Empty_FalseReturn()
            => Login_Test(StubObjects.LoginFalse2, false);

        [TestMethod]
        public void Login_Space_FalseReturn()
            => Login_Test(StubObjects.LoginFalse3, false);

        /// <summary>
        /// Тестування логінів
        /// </summary>
        /// <param name="stub">Заглушка</param>
        /// <param name="wait">Очікуваний результат</param>
        private void Login_Test(string stub, bool wait)
        {
            // act
            bool actual = login.TryRegister(stub, out result);

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
