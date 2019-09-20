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
        private IRegistration login;
        /// <summary>
        /// Вихідний результат
        /// </summary>
        private string result;

        /// <summary>
        /// Запускається один раз перед запуском петодів
        /// </summary>
        [ClassInitialize]
        private void InitializeTests()
        {
            // arrange
            login = new Login();
            result = string.Empty;
        }

        [TestMethod]
        public void Login_SuccessEntry_TrueReturn()
        {
            // act
            bool actual = login.TryRegister(StubObjects.LoginTrue, out result);

            // assert
            Assert.IsTrue(actual);
            Assert.AreNotSame(StubObjects.LoginTrue, result);
            
        }
    }
}
