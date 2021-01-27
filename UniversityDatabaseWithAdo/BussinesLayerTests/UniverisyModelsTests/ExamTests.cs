using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using University;
namespace BussinesLayerTests
{

    [TestClass]
    public class ExamTests
    {
        [TestMethod]
        public void NameOfExamTest_ZeroLentghName_ArgumentExceptionThrown()
        {
            bool actual = false;
            try
            {
                Exam info = new Exam() { NameOfExam = "" };
            }
            catch (ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void NameOfExamTest_NullISSetASName_ArgumentExceptionThrown()
        {
            bool actual = false;
            try
            {
                Exam info = new Exam() { NameOfExam = null };
            }
            catch (NullReferenceException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void NameOfExamTestTest_CorectValue_ExceptionWillNotThrown()
        {
            bool actual = true;
            try
            {
                Exam info = new Exam() { NameOfExam = "Spanish" };
            }
            catch (Exception)
            {
                actual = false;
            }
            Assert.IsTrue(actual);
        }
    }
}
