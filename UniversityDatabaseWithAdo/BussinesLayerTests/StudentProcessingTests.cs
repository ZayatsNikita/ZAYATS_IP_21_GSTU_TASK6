using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using University;
namespace BussinesLayerTests
{
    [TestClass]
    public class StudentProcessingTests
    {
        private static List<ReportingReport> list = new List<ReportingReport>() {
            
            new ReportingReport(1,1,"Karp Alex DD","F",DateTime.Now,1,1,3), 
            new ReportingReport(2,2,"NIkita Valer Gnusov","M",DateTime.Now,3,1,2),
            new ReportingReport(1,3,"MOyva JJu DD","M",DateTime.Now,1,1,10),
            new ReportingReport(2,4,"Prerk Tuk Tuc","F",DateTime.Now,2,1,8), 
            new ReportingReport(3,5,"Munsd sdhf dfkj","F",DateTime.Now,2,3,8),
            new ReportingReport(3,6,"Larisa JJJ Ddsf","M",DateTime.Now,2,1,2),
            };
        [TestMethod]
        public void ExtractStudentsForExpulsionTest_ThrereAre3Student_DictionaryWithStudentsAndGroup()
        {
            bool actual = false;
            Dictionary<int, List<Student>> groupingStudents = StudentProcessing.ExtractStudentsForExpulsion(list);
            actual = groupingStudents.Count == 3 && groupingStudents[1].First().FIO == "Karp Alex DD" && groupingStudents[1].Count ==1
                 && groupingStudents[2].First().FIO == "NIkita Valer Gnusov" && groupingStudents[2].Count == 1
                 && groupingStudents[3].First().FIO == "Larisa JJJ Ddsf" && groupingStudents[3].Count == 1;
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void ExtractStudentsForExpulsionTest_NullList_ArgumentNullExceptionThrown()
        {
            bool actual = false;
            try
            {
                StudentProcessing.ExtractStudentsForExpulsion(null);
            }
            catch(ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void ExtractStudentsForExpulsionTest_ZeroLentghList_ArgumentExceptionThrown()
        {
            bool actual = false;
            try
            {
                StudentProcessing.ExtractStudentsForExpulsion(new List<ReportingReport>());
            }
            catch (ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
    }
}
