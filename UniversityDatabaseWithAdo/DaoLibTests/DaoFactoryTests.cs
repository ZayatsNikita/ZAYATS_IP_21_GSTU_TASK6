using DAOLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using University;
namespace DaoLibTests
{
    [TestClass]
    public class DaoFactoryTests
    {

        [DataTestMethod]
        [DataRow("incroo")]
        [DataRow("afsdasfoo")]
        public void CreateDaoTest_NotExsistDaoName_ArgumentExceptionThrown(string str)
        {
            bool actual = false;
            DaoFactory<ReportingReport> daoFactory = new DaoFactory<ReportingReport>();
            try
            {
                daoFactory.CreateDao(str, "str");
            }
            catch(ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void CreateDaoTest_NullIsSetAsDaoType_ArgumentNullExceptionThrown()
        {
            bool actual = false;
            DaoFactory<int> daoFactory = new DaoFactory<int>();
            try
            {
                daoFactory.CreateDao(null, "str");
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
    }
}
