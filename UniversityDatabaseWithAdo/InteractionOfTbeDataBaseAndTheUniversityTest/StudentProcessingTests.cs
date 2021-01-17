using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InteractionOfTheDatabaseAndTheUniversity;
using System.Collections.Generic;
using University;
namespace InteractionOfTbeDataBaseAndTheUniversityTest
{
    [TestClass]
    public class StudentProcessingTests
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=UniversityDatabase; Integrated Security=True";

        [TestMethod]
        public void ExtractFromTheDatabaseOfStudentsForExpulsionTest_SuchStudentsExist_ListWithStudents()
        {
            bool actual = false;
            StudentProcessing processing = new StudentProcessing(connectionString);
            List<Student> list = processing.ExtractFromTheDatabaseOfStudentsForExpulsion();
            actual = list.Count == 1 && list[0].FIO == "Tsmyg Dmitry Alexandrovich";
            Assert.IsTrue(actual);
        }
    }
}
