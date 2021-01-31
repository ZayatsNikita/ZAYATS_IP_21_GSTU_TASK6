using DAOLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
namespace DaoLibTests
{
    [TestClass]
    public class XlsxDaoTests
    {
        [TestMethod]
        public void ConstructorTests_CorrectParams_NewObjectOfXlsxDaoClass()
        {
            bool actual = true;
            try
            {
                XlsxDao dao = new XlsxDao(new string[] { "hello world", "Secondworkds" });
            }
            catch (Exception)
            {
                actual = false;
            }
            Assert.IsTrue(actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]

        public void ConstructorTests_ThereAreNullInCreatingParams_ArgumentNullExceptionThrown(int mode)
        {
            bool actual = false;
            string[] data = null;
            switch (mode)
            {
                case 1:
                    break;
                case 2:
                    data = new string[] { "Hello", "By", null };
                    break;
            }
            try
            {
                XlsxDao dao = new XlsxDao(data);
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }


        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]

        public void SaveDataToFileTests_ThereAreNullInParams_ArgumentNullExceptionThrown(int mode)
        {
            XlsxDao dao = new XlsxDao(new string[] { "1", "2" });
            bool actual = false;
            try
            {
                switch (mode)
                {
                    case 1:
                        dao.SaveDataToFile(null, new List<Object>());
                        break;
                    case 2:
                        dao.SaveDataToFile("hello world.xlsx", null);
                        break;
                    case 3:
                        dao.SaveDataToFile("hello world.xlsx", new List<Object>() { 1, 2, null, "hello" });
                        break;
                }
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SaveDataToFileTests_CorrectParams_NewXlsxDocumentWillBeCreated()
        {
            XlsxDao dao = new XlsxDao(new string[] { "1", "2" });
            bool actual = false;
            string filePath = "data.xlsx";
            try
            {
                dao.SaveDataToFile(filePath, new List<object> { "nikita", "dima", 12, 45 });
                actual = File.Exists(filePath);
            }
            catch (Exception)
            {
                actual = false;
            }
            Assert.IsTrue(actual);
        }




    }
}
