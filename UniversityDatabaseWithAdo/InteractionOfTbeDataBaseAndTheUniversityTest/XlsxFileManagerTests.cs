using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InteractionOfTheDatabaseAndTheUniversity;
using System.IO;
using UserLayer;
namespace UserLayerTest
{
    [TestClass]
    public class XlsxFileManagerTests
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=UniversityDatabase; Integrated Security=True";
        private static InteractionOfComomnInfoAndDatabseBase interaction = new InteractionOfComomnInfoAndDatabseBase(connectionString);
        [TestMethod]
        public void SaveTheResultsOfEachSessionByGroupToTableTest_EachTableInTheDatabaseContainsData_newSlsxDocumentWillBeCreat()
        {
            bool actual;
            string filePath = @"..\..\..\SessionResultsGroupNumberStudentNameExamCodeSessionNumberGrade.xlsx";
            CommonInfoXlsxFileManager.SaveComonInfoToXLSX(filePath, interaction.GetCommonInfoFromDatabase());
            actual = File.Exists(filePath);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTableTest_EachTableInTheDatabaseContainsData_newSlsxDocumentWillBeCreat()
        {
            bool actual;
            string filePath = @"..\..\..\MinMaxAvgExamIDGropId.xlsx";
            CommonInfoXlsxFileManager.SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(filePath, interaction.GetCommonInfoFromDatabase());
            actual = File.Exists(filePath);
            Assert.IsTrue(actual);
        }
    }
}
