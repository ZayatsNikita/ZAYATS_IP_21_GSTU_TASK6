using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaoLibTests
{
    ///////                                           !!!ATTENTION -- 1!!!
    ///////To work correctly, you need to have a database created and populated with scripts from the same repository
    ///////                                           !!!ATTENTION -- 2!!!
    ///////If necessary change the database "connectionString" in the static constructor of the TransatSqlDaoTest class

    [TestClass]
    public class TransatSqlDaoTest
    {
        private static string connectionString;
        static TransatSqlDaoTest()
        {
            connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=UniversityDatabase; Integrated Security=True";
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void ReadAllTest_GetDataFromDatabase_ExsistElements(int mode)
        {

           bool actual=false;
           switch(mode)
           {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
           }

            Assert.IsTrue(actual);
            
        }
    }
}
