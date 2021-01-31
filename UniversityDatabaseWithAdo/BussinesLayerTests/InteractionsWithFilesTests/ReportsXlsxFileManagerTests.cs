using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using University;
using System.IO;

namespace InteractionsWithFilesTest
{
    
    [TestClass]
    public class ReportsXlsxFileManagerTests
    {
        private static List<ReportingReport> list = new List<ReportingReport>(){
            new ReportingReport(1,1,"Karp Alex DD","F",System.DateTime.Now,1,1,3),
            new ReportingReport(2,2,"NIkita Valer Gnusov","M",System.DateTime.Now,3,1,2),
            new ReportingReport(1,3,"MOyva JJu DD","M",System.DateTime.Now,1,1,10),
            new ReportingReport(2,4,"Prerk Tuk Tuc","F",System.DateTime.Now,2,1,8),
            new ReportingReport(3,5,"Munsd sdhf dfkj","F",System.DateTime.Now,2,3,8),
            new ReportingReport(3,6,"Larisa JJJ Ddsf","M",System.DateTime.Now,2,1,2),
            };
        [TestMethod]
        public void SaveResaltOfSessionsToXlsxTable_CorrectParams_newSlsxDocumentWillBeCreat()
        {
            bool actual;
            string filePath = @"..\..\..\SessionResultsGroupNumberStudentNameExamCodeSessionNumberGrade.xlsx";
            InteractionsWithFiles.ReportsXlsxFileManager.SaveResaltOfSessionsToXlsxTable(filePath, list);
            actual = File.Exists(filePath);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTableTest_CorrectParams_newSlsxDocumentWillBeCreat()
        {
            bool actual;
            string filePath = @"..\..\..\MinMaxAvgGropId.xlsx";
            InteractionsWithFiles.ReportsXlsxFileManager.SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(filePath, list);
            actual = File.Exists(filePath);
            Assert.IsTrue(actual);
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow(@"file.xlsx", null)]
        public void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTableTest_NullSetAsParams_ArgumentNullExceptionThrown(string filePath, List<ReportingReport> data)
        {
            bool actual = false;
            if (filePath == null)
            {
                data = new List<ReportingReport>();
            }
            try
            {
                InteractionsWithFiles.ReportsXlsxFileManager.SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(filePath, data);
            }
            catch (System.ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow(@"file.xlsx", null)]
        public void SaveResaltOfSessionsToXlsxTable_NullSetAsParams_ArgumentNullExceptionThrown(string filePath, List<ReportingReport> data)
        {
            bool actual = false;
            if (filePath == null)
            {
                data = new List<ReportingReport>();
            }
            try
            {
                InteractionsWithFiles.ReportsXlsxFileManager.SaveResaltOfSessionsToXlsxTable(filePath, data);
            }
            catch (System.ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }


    }
}
