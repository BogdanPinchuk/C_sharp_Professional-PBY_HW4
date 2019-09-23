using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LesApp0.Tests.Stub;

namespace LesApp0.Tests
{
    /// <summary>
    /// Перевірка реєстрації логіна
    /// </summary>
    [TestClass]
    public class CheckLoginTest
    {
        /// <summary>
        /// Екземпляр для логіна
        /// </summary>
        private static IRegistration login;
        /// <summary>
        /// Делегат для методу тестування вхідних даних логіна/пароля 
        /// </summary>
        /// <param name="stub">Заглушка</param>
        /// <param name="wait">Очікуваний результат</param>
        /// <param name="logpass">Екземпляр логіна/пароля</param>
        private delegate void CheckRegister(string stub, bool wait, IRegistration logpass);
        /// <summary>
        /// Зв'язування метода
        /// </summary>
        private static CheckRegister delLogin_Test;

        /// <summary>
        /// Запускається один раз перед запуском методів
        /// </summary>
        [ClassInitialize]
        public static void InitializeClass(TestContext _)
        {
            // arrange
            login = new Login();
            delLogin_Test = TestMethods.LogPass_Test;
        }

        [TestMethod]
        public void Login_SuccessEntry_TrueReturn()
            => delLogin_Test(StubObjects.LoginTrue, true, login);

        [TestMethod]
        public void Login_AddNumber_FalseReturn()
            => delLogin_Test(StubObjects.LoginFalse0, false, login);

        [TestMethod]
        public void Login_AddOtherLetter_FalseReturn()
            => delLogin_Test(StubObjects.LoginFalse1, false, login);

        [TestMethod]
        public void Login_Empty_FalseReturn()
            => delLogin_Test(StubObjects.LoginFalse2, false, login);

        [TestMethod]
        public void Login_Space_FalseReturn()
            => delLogin_Test(StubObjects.LoginFalse3, false, login);

        [TestMethod]
        public void Login_NullAble_FalseReturn()
            => delLogin_Test(StubObjects.LoginFalse4, false, login);

        [TestMethod]
        public void Login_AddSign_FalseReturn()
            => delLogin_Test(StubObjects.LoginFalse5, false, login);

    }
}
