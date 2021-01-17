using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using University;
namespace BussinesLayerTests
{
    [TestClass]
    public class StudentsTests
    {
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("12345")]
        public void FIOTest_VrongParamsUse_ExceptionThrown(string str)
        {
            bool actual = false;
            try
            {
                Student student = new Student() { FIO = str };
            }
            catch (Exception)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void SexTest_NullISSetASSex_ArgumentExceptionThrown()
        {
            bool actual = false;
            try
            {
                Student student = new Student() { Sex = null };
            }
            catch (NullReferenceException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void FIOTestTest_CorectValue_ExceptionWillNotThrown()
        {
            bool actual = true;
            try
            {
                Student student = new Student() { FIO = "asfdjksd" };
            }
            catch (Exception)
            {
                actual = false;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void SexTestTest_CorectValue_ExceptionWillNotThrown()
        {
            bool actual = true;
            try
            {
                Student student = new Student() { Sex = "M" };
            }
            catch (Exception)
            {
                actual = false;
            }
            Assert.IsTrue(actual);
        }
    }
}
