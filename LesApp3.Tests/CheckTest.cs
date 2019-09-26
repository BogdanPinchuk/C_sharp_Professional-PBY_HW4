using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp3.Tests
{
    [TestClass]
    public class CheckTest
    {
        /// <summary>
        /// Чек
        /// </summary>
        Check check;
        readonly string path = "TestCheck";

        [TestInitialize]
        public void TestInitialize()
        {
            check = new Check();
        }

        [TestMethod]
        public void SaveCheckInFile_UA_True()
        {
            // arrange
            check.Products.Add(new Position()
            {
                Name = "IFresh",
                Count = 1,
                Price = 7.99,
                Volume = 0.5,
            });
            check.Products.Add(new Position()
            {
                Name = "Meat",
                Count = 1,
                Price = 225.00,
                Weigth = 0.575,
            });
            check.Products.Add(new Position()
            {
                Name = "Bread white",
                Count = 1,
                Price = 15.00,
            });

            // act
            check.SaveCheckFile(path);

            // assert
            Assert.IsTrue(File.Exists(path + ".txt"));

            // present
            Debug.WriteLine(check.ToString());

            // open
            Process.Start(path + ".txt");
        }

        [TestMethod]
        public void SaveCheckInFile_EN_True()
        {
            // arrange
            check.Products.Add(new Position()
            {
                Name = "IFresh",
                Count = 1,
                Price = 7.99,
                Volume = 0.5,
            });
            check.Products.Add(new Position()
            {
                Name = "Meat",
                Count = 1,
                Price = 225.00,
                Weigth = 0.575,
            });
            check.Products.Add(new Position()
            {
                Name = "Bread white",
                Count = 1,
                Price = 15.00,
            });

            // act
            check.SetDolar();
            check.SaveCheckFile(path);

            // assert
            Assert.IsTrue(File.Exists(path + ".txt"));

            // present
            Debug.WriteLine(check.ToString());

            // open
            Process.Start(path + ".txt");
        }

        [TestMethod]
        public void RecognizationCheck_UA()
        {
            // arrange
            check.Products.Add(new Position()
            {
                Name = "IFresh",
                Count = 1,
                Price = 7.99,
                Volume = 0.5,
            });
            check.Products.Add(new Position()
            {
                Name = "Meat",
                Count = 1,
                Price = 225.00,
                Weigth = 0.575,
            });
            check.Products.Add(new Position()
            {
                Name = "Bread white",
                Count = 1,
                Price = 15.00,
            });

            // act
            check.SaveCheckFile(path);

            Check newCheck = new Check(path);

            // present
            Debug.WriteLine(newCheck.ToString());
        }


    }
}
