using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LesApp0.Tests.Stub;
using Moq;

namespace LesApp0.Tests
{
    [TestClass]
    public class CheckRegister
    {
        /// <summary>
        /// Клас реєстрації
        /// </summary>
        private static RegisterTest register;
        /// <summary>
        /// Дані для підстановки
        /// </summary>
        const string login = "Логін",
            password = "Пароль";

        /// <summary>
        /// Введений правильний логін
        /// </summary>
        [TestMethod]
        public void Register_EntryTL_TrueReturn()
        {
            // arrange
            register = new RegisterTest()
            {
                ReadLineData = StubObjects.Register_tLP,
                ReadKeyData = StubObjects.Register_Key_tLP,
            };

            // act
            string returned = string.Empty;
            bool actual = register.AnalysisEnterDataTest(new Login(), login, out returned);

            // assert
            Assert.IsTrue(actual);
            Assert.AreEqual(StubObjects.LoginTrue, returned);
        }

        /// <summary>
        /// Введений правильний пароль
        /// </summary>
        [TestMethod]
        public void Register_EntryTP_TrueReturn()
        {
            // arrange
            var tempLP = StubObjects.Register_tLP;
            tempLP.Dequeue();
            var tempK = StubObjects.Register_Key_tLP;
            tempK.Dequeue();

            register = new RegisterTest()
            {
                ReadLineData = tempLP,
                ReadKeyData = tempK,
            };

            // act
            string returned = string.Empty;
            bool actual = register.AnalysisEnterDataTest(new Password(), password, out returned);

            // assert
            Assert.IsTrue(actual);
            Assert.AreEqual(StubObjects.PasswordTrue, returned);
        }

        /// <summary>
        /// Введений неправильний логін
        /// </summary>
        [TestMethod]
        public void Register_EntryFL_FalseReturn()
        {
            // arrange (необхідно зняти верхній елемент)
            register = new RegisterTest()
            {
                ReadLineData = StubObjects.Register_fLP,
                ReadKeyData = StubObjects.Register_Key_fLP,
            };

            // act
            string returned = string.Empty;
            bool actual = register.AnalysisEnterDataTest(new Login(), login, out returned);

            // assert
            Assert.IsFalse(actual);
            Assert.AreEqual(string.Empty, returned);

        }

        /// <summary>
        /// Введений неправильний пароль
        /// </summary>
        [TestMethod]
        public void Register_EntryFP_FalseReturn()
        {
            // arrange (необхідно зняти верхній елемент)
            var tempLP = StubObjects.Register_fLP;
            tempLP.Dequeue();
            var tempK = StubObjects.Register_Key_fLP;
            tempK.Dequeue();

            register = new RegisterTest()
            {
                ReadLineData = tempLP,
                ReadKeyData = tempK,
            };

            // act
            string returned = string.Empty;
            bool actual = register.AnalysisEnterDataTest(new Password(), password, out returned);

            // assert
            Assert.IsFalse(actual);
            Assert.AreEqual(string.Empty, returned);
        }

        /// <summary>
        /// Введено: Пр Л, Пр П (Пр - правильний, Нр - неправильний, Л - логін, П - пароль)
        /// </summary>
        [TestMethod]
        public void Register_EntryTLP()
        {
            // arrange
            register = new RegisterTest()
            {
                ReadLineData = StubObjects.Register_tLP,
                ReadKeyData = StubObjects.Register_Key_tLP,
            };

            // act
            register.RegisterUser();

            // assert
            Assert.IsTrue(register.Successful);
            Assert.AreEqual(StubObjects.LoginTrue, register.Login);
            Assert.AreEqual(StubObjects.PasswordTrue, register.Password);
        }

    }


    internal class RegisterTest : Register
    {
        /// <summary>
        /// Метод для введення логіна/пароля
        /// </summary>
        protected override ReadLine delReadline { get; set; }
            = ReadLineM;
        /// <summary>
        /// Метод який керує повторним введенням логіна/пароля або виходом
        /// </summary>
        protected override ReadKey delReadKey { get; set; }
            = ReadKeyM;

        /// <summary>
        /// Дані для вводу логіна пароля
        /// </summary>
        private static Queue<string> readLineData;
        public Queue<string> ReadLineData
        {
            get { return readLineData; }
            set { readLineData = value; }
        }

        /// <summary>
        /// Дані для управління ходом реєстрації
        /// </summary>
        private static Queue<ConsoleKeyInfo> readKeyData;
        public Queue<ConsoleKeyInfo> ReadKeyData
        {
            get { return readKeyData; }
            set { readKeyData = value; }
        }

        /// <summary>
        /// Метод введення логіна/пароля
        /// </summary>
        /// <returns></returns>
        internal static string ReadLineM()
            => readLineData.Dequeue();

        /// <summary>
        /// Метод який керує повторним введенням логіна/пароля або виходом
        /// </summary>
        internal static ConsoleKeyInfo ReadKeyM(bool _)
            => readKeyData.Dequeue();

        /// <summary>
        /// Аналіз введених даних
        /// </summary>
        /// <param name="logpass">Перевірка правильності введення логіна/пароля</param>
        /// <param name="part">Що перевіряється логін/пароль, вводити з великої букви</param>
        /// <returns></returns>
        internal bool AnalysisEnterDataTest(IRegistration logpass, string part, out string returned)
            => AnalysisEnterData(logpass, part, out returned);

    }
}
