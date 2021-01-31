using DAOLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using University;

namespace DaoLibTests
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////                                           !!!ATTENTION -- 1!!!                                               //////
    ///////To work correctly, you need to have a database created and populated with scripts from the same repository    //////
    ///////                     The scripts are located in "the scripts for database deployment" folder                  //////
    ///////                                           !!!ATTENTION -- 2!!!                                               //////                       
    ///////If necessary change the database "connectionString" ConnectionString.connectionString                         //////
    ///////ConnectionString.connectionString used in classes: TransatSqlDaoTest, TransatSqlDaoFactoryTests,              //////
    ///////                                          InteractionOfComomnInfoAndDatabseBaseTests.                         //////
    ///////                                                                                                              //////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class TransatSqlDaoFactoryTests
    {
        private static string connectionString = ConnectionString.connectionString;

        [TestMethod]
        public void CreateDaoTest_NullLenghtStringIsSetAsConnectionsString_ArgumentExceptionThrown()
        {
            bool actual = false;
            DaoFactory<ReportingReport> daoFactory = new DaoFactory<ReportingReport>();
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
            DaoFactory<ReportingReport> daoFactory = new DaoFactory<ReportingReport>();
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
            IDao<ReportingReport> dao=null;
            DaoFactory<ReportingReport> daoFactory = new DaoFactory<ReportingReport>();
            try
            {
                dao = daoFactory.CreateDao("TransatSqlDao", connectionString);
            }
            catch (Exception)
            {
                actual = false;
            }
            Assert.IsTrue(actual);
        }
    }
}
