using DAOLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using University;

namespace DaoLibTests
{

    class TransatSqlDaoFactoryTests
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=InteractionOfComomnInfoAndDatabseBaseTest; Integrated Security=True";

        [TestMethod]
        public void CreateDaoTest_NullLenghtStringIsSetAsConnectionsString_ArgumentExceptionThrown()
        {
            bool actual = false;
            DaoFactory<CommonInfo> daoFactory = new DaoFactory<CommonInfo>();
            try
            {
                daoFactory.CreateDao("TransatSqlDao", "");
            }
            catch (ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void CreateDaoTest_NullIsSetAsConnectionsString_NullReferenceExceptionThrown()
        {
            bool actual = false;
            DaoFactory<CommonInfo> daoFactory = new DaoFactory<CommonInfo>();
            try
            {
                daoFactory.CreateDao("TransatSqlDao", null);
            }
            catch (NullReferenceException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void CreateDao_CorrectParams_ExceptionWillBeNotThrown()
        {
            bool actual = true;
            IDao<CommonInfo> dao=null;
            DaoFactory<CommonInfo> daoFactory = new DaoFactory<CommonInfo>();
            try
            {
                dao = daoFactory.CreateDao("TransatSqlDao", connectionString);
            }
            catch (Exception)
            {
                actual = false;
            }
            dao.CloseConnect();
            Assert.IsTrue(actual);
        }
    }
}
