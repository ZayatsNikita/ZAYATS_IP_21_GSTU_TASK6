using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InteractionOfTheDatabaseAndTheUniversity;
using System.IO;
namespace InteractionOfTbeDataBaseAndTheUniversityTest
{
    [TestClass]
    public class XlsxFileManagerTests
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=UniversityDatabase; Integrated Security=True";
        [TestMethod]
        public void SaveTheResultsOfEachSessionByGroupToTableTest_EachTableInTheDatabaseContainsData_newSlsxDocumentWillBeCreat()
        {
            bool actual = false;
            XlsxFileManager xlsxFile= new XlsxFileManager(connectionString);
            xlsxFile.SaveTheResultsOfEachSessionByGroupToTable(@"..\..\..\SessionResultsGroupNumberStudentNameExamCodeSessionNumberGrade.xlsx");
            actual = File.Exists(@"..\..\..\SessionResultsGroupNumberStudentNameExamCodeSessionNumberGrade.xlsx");
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTableTest_EachTableInTheDatabaseContainsData_newSlsxDocumentWillBeCreat()
        {
            bool actual = false;
            XlsxFileManager xlsxFile = new XlsxFileManager(connectionString);
            xlsxFile.SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(@"..\..\..\MinMaxAvgExamIDGropId.xlsx");
            actual = File.Exists(@"..\..\..\MinMaxAvgExamIDGropId.xlsx");
            Assert.IsTrue(actual);
        }
    }
}
