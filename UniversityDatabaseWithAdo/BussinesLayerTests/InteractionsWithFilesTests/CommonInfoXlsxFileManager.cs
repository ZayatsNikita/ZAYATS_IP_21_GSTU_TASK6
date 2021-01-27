using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using University;
using System.IO;

namespace InteractionsWithFilesTest
{
    
    [TestClass]
    public class CommonInfoXlsxFileManager
    {
        private static List<CommonInfo> list = new List<CommonInfo>(){
            new CommonInfo(1,1,"Karp Alex DD","F",System.DateTime.Now,1,1,3),
            new CommonInfo(2,2,"NIkita Valer Gnusov","M",System.DateTime.Now,3,1,2),
            new CommonInfo(1,3,"MOyva JJu DD","M",System.DateTime.Now,1,1,10),
            new CommonInfo(2,4,"Prerk Tuk Tuc","F",System.DateTime.Now,2,1,8),
            new CommonInfo(3,5,"Munsd sdhf dfkj","F",System.DateTime.Now,2,3,8),
            new CommonInfo(3,6,"Larisa JJJ Ddsf","M",System.DateTime.Now,2,1,2),
            };
        [TestMethod]
        public void SaveResaltOfSessionsToXlsxTable_CorrectParams_newSlsxDocumentWillBeCreat()
        {
            bool actual;
            string filePath = @"..\..\..\SessionResultsGroupNumberStudentNameExamCodeSessionNumberGrade.xlsx";
            InteractionsWithFiles.CommonInfoXlsxFileManager.SaveResaltOfSessionsToXlsxTable(filePath, list);
            actual = File.Exists(filePath);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTableTest_CorrectParams_newSlsxDocumentWillBeCreat()
        {
            bool actual;
            string filePath = @"..\..\..\MinMaxAvgExamIDGropId.xlsx";
            InteractionsWithFiles.CommonInfoXlsxFileManager.SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(filePath, list);
            actual = File.Exists(filePath);
            Assert.IsTrue(actual);
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow(@"file.xlsx", null)]
        public void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTableTest_NullSetAsParams_ArgumentNullExceptionThrown(string filePath, List<CommonInfo> data)
        {
            bool actual = false;
            if (filePath == null)
            {
                data = new List<CommonInfo>();
            }
            try
            {
                InteractionsWithFiles.CommonInfoXlsxFileManager.SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(filePath, data);
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
        public void SaveResaltOfSessionsToXlsxTable_NullSetAsParams_ArgumentNullExceptionThrown(string filePath, List<CommonInfo> data)
        {
            bool actual = false;
            if (filePath == null)
            {
                data = new List<CommonInfo>();
            }
            try
            {
                InteractionsWithFiles.CommonInfoXlsxFileManager.SaveResaltOfSessionsToXlsxTable(filePath, data);
            }
            catch (System.ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }


    }
}
