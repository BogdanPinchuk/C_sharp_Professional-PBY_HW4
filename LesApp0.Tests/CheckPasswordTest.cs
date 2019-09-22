using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LesApp0.Tests.Stub;

namespace LesApp0.Tests
{
    [TestClass]
    public class CheckPasswordTest
    {
        /// <summary>
        /// Екземпляр для пароля
        /// </summary>
        private static IRegistration password;
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
        private static CheckRegister delPassword_Test;

        /// <summary>
        /// Запускається один раз перед запуском методів
        /// </summary>
        [ClassInitialize]
        public static void InitializeClass(TestContext _)
        {
            // arrange
            password = new Password();
            delPassword_Test = TestMethods.LogPass_Test;
        }

        [TestMethod]
        public void Password_SuccessEntry_TrueReturn()
            => delPassword_Test(StubObjects.PasswordTrue, true, password);

        [TestMethod]
        public void Password_AddNumber_FalseReturn()
            => delPassword_Test(StubObjects.PasswordFalse0, true, password);

        [TestMethod]
        public void Password_AddOtherLetter_FalseReturn()
            => delPassword_Test(StubObjects.PasswordFalse1, false, password);

        [TestMethod]
        public void Password_Empty_FalseReturn()
            => delPassword_Test(StubObjects.PasswordFalse2, false, password);

        [TestMethod]
        public void Password_Space_FalseReturn()
            => delPassword_Test(StubObjects.PasswordFalse3, false, password);

        [TestMethod]
        public void Password_NullAble_FalseReturn()
            => delPassword_Test(StubObjects.PasswordFalse4, false, password);

        [TestMethod]
        public void Password_AddSign_FalseReturn()
            => delPassword_Test(StubObjects.PasswordFalse5, false, password);

    }
}
