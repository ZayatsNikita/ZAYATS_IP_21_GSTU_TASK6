using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BussinesLayerTests
{
    [TestClass]
    public class GroupOfStudentTests
    {
        [TestMethod]
        public void NameOfGroupTest_ZeroLentghName_ArgumentExceptionThrown()
        {
            bool actual = false;
            try
            {
                GroupOfStudent info = new GroupOfStudent() { NameOfGroup = "" };
            }
            catch (ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void NameOfGroupTest_NullISSetASName_ArgumentExceptionThrown()
        {
            bool actual = false;
            try
            {
                GroupOfStudent info = new GroupOfStudent() { NameOfGroup = null };
            }
            catch (NullReferenceException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void NameOfGroupTestTest_CorectValue_ExceptionWillNotThrown()
        {
            bool actual = true;
            try
            {
                GroupOfStudent info = new GroupOfStudent() { NameOfGroup = "Spanish" };
            }
            catch (Exception)
            {
                actual = false;
            }
            Assert.IsTrue(actual);
        }
    }
}
