using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using University;
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
        public void NameOfGroupTest_NullISSetASName_ArgumentNullExceptionThrown()
        {
            bool actual = false;
            try
            {
                GroupOfStudent info = new GroupOfStudent() { NameOfGroup = null };
            }
            catch (ArgumentNullException)
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
