using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LesApp3.Tests.Resources;
using System.Diagnostics;

namespace LesApp3.Tests
{
    [TestClass]
    public class PositionTest
    {
        Position product;

        [TestInitialize]
        public void TestInitialize()
        {
            product = new Position();
        }

        #region Тестування виведення в доларах
        [TestMethod]
        public void Position_WaterData_EN_Equal()
        {
            // arrange
            product.Count = 1;
            product.Name = "IFresh";
            product.Price = 7.99;
            product.Volume = 0.5;
            product.Region = "en-US";

            // act + assert
            Assert.AreEqual(StubObjects.WaterEN, product.ToString());

            // Вивід
            Debug.WriteLine(product.ToString());
        }

        [TestMethod]
        public void Position_PizzaData_EN_Equal()
        {
            // arrange
            product.Count = 1;
            product.Name = "Miami";
            product.Price = 199.00;
            product.Weigth = 0.114;
            product.Region = "en-US";

            // act + assert
            Assert.AreEqual(StubObjects.PizzaEN, product.ToString());

            // Вивід
            Debug.WriteLine(product.ToString());
        }
        #endregion

        #region Тестування виведення в гривнях
        [TestMethod]
        public void Position_WaterData_UA_Equal()
        {
            // arrange
            product.Name = "IFresh";
            product.Count = 1;
            product.Price = 7.99;
            product.Volume = 0.5;

            // act + assert
            Assert.AreEqual(StubObjects.WaterUA, product.ToString());

            // Вивід
            Debug.WriteLine(product.ToString());
        }

        [TestMethod]
        public void Position_PizzaData_UA_Equal()
        {
            // arrange
            product.Name = "Miami";
            product.Count = 1;
            product.Price = 199.00;
            product.Weigth = 0.114;

            // act + assert
            Assert.AreEqual(StubObjects.PizzaUA, product.ToString());

            // Вивід
            Debug.WriteLine(product.ToString());
        }
        #endregion

        #region Тестування виведення в доларах із врахуванням курса валют НБУ
        [TestMethod]
        public void Position_WaterData_ENAddCur_Equal()
        {
            // arrange
            product.Count = 1;
            product.Name = "IFresh";
            product.Price = 7.99;
            product.Volume = 0.5;
            product.Region = "en-US";
            product.Money = Currency.Other;

            // act + assert
            Assert.AreEqual(StubObjects.WaterENCur, product.ToString());

            // Вивід
            Debug.WriteLine(product.ToString());
        }

        [TestMethod]
        public void Position_PizzaData_ENAddCur_Equal()
        {
            // arrange
            product.Count = 1;
            product.Name = "Miami";
            product.Price = 199.00;
            product.Weigth = 0.114;
            product.Region = "en-US";
            product.Money = Currency.Other;

            // act + assert
            Assert.AreEqual(StubObjects.PizzaENCur, product.ToString());

            // Вивід
            Debug.WriteLine(product.ToString());
        } 
        #endregion

    }
}
