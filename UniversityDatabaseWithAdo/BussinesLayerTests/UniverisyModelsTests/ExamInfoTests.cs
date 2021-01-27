using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using University;

namespace BussinesLayerTests
{
    [TestClass]
    public class ExamInfoTests
    {
        [TestMethod]
        public void NumberOfSessionTest_SetValueLessThenZero_ArgumentExceptionThrown()
        {
            bool actual = false;
            try
            {
                ExamInfo info = new ExamInfo() { NumberSession = -1 };
            }
            catch(ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void NumberOfSessionTest_CorectValue_ArgumentExceptionWillNotThrown()
        {
            bool actual = true;
            try
            {
                ExamInfo info = new ExamInfo() { NumberSession = 1 };
            }
            catch (ArgumentException)
            {
                actual = false ;
            }
            Assert.IsTrue(actual);
        }
    }
}
