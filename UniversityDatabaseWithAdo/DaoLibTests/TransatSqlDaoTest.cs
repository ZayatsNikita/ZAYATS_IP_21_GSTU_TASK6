using DAOLib;
using DAOLib.SqlDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using University;
namespace DaoLibTests
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////                                           !!!ATTENTION -- 1!!!                                               //////
    ///////To work correctly, you need to have a database created and populated with scripts from the same repository    //////
    ///////                     The scripts are located in "the scripts for database deployment" folder                  //////
    ///////                                           !!!ATTENTION -- 2!!!                                               //////                       
    ///////If necessary change the database "connectionString" ConnectionString.connectionString                         //////
    ///////ConnectionString.connectionString used in classes: TransatSqlDaoTest, TransatSqlDaoFactoryTests,              //////
    ///////                                          InteractionOfComomnInfoAndDatabseBaseTests.                         //////
    ///////                                                                                                              //////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [TestClass]
    public class TransatSqlDaoTest
    {
        private static string connectionString;
        static TransatSqlDaoTest()
        {
            connectionString = ConnectionString.connectionString;
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void ReadAllTest_GetDataFromDatabase_ListWithElements(int mode)
        {

            bool actual = false;
            switch (mode)
            {
                case 1:
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    List<Exam> list = daoForExam.ReadAll();
                    actual = list.Count(x => x.TypeOfExam == "Exam" && x.NameOfExam == "MMA") == 1 && list.Count(x => x.TypeOfExam == "Test" && x.NameOfExam == "Mechanics") == 1;
                    break;
                case 2:
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Count(x => x.NameOfGroup == "IP-21") == 1 && groups.Count(x => x.NameOfGroup == "ML-42") == 1;
                    break;
            }
            Assert.IsTrue(actual);

        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void CreateTest_SetDataToDatabase_NewElementInDatabase(int mode)
        {
            bool actual = false;
            switch (mode)
            {
                case 1:
                    Exam spanish = new Exam() { NameOfExam = "Chiniess", TypeOfExam = "Exam" };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    List<Exam> list = daoForExam.ReadAll();
                    actual = list.Count(x => x.NameOfExam == "Chiniess") == 0;
                    spanish.Id = Convert.ToInt32(daoForExam.Create(spanish));
                    list = daoForExam.ReadAll();
                    actual = actual && list.Count(x => x.Id == spanish.Id && x.NameOfExam == "Chiniess" && x.TypeOfExam == "Exam") == 1;
                    daoForExam.Delete(spanish);
                    break;
                case 2:
                    GroupOfStudent group = new GroupOfStudent() { NameOfGroup = "ZK-37" };
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Count(x => x.NameOfGroup == "ZK-37") == 0;
                    group.Id = Convert.ToInt32(daoForGroup.Create(group));
                    groups = daoForGroup.ReadAll();
                    actual = actual && groups.Count(x => x.Id == group.Id && x.NameOfGroup == "ZK-37") == 1;
                    daoForGroup.Delete(group);

                    break;
                case 3:
                    Student student = new Student() { Sex = "F", BirthDay = DateTime.Parse("2002-07-09"), FIO = "Artem Nikita Konstantinovna", GroupId = 2 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count(x => x.FIO == "Artem Nikita Konstantinovna") == 0;
                    student.Id = Convert.ToInt32(daoForStudent.Create(student));
                    students = daoForStudent.ReadAll();
                    actual = actual && students.Count(x => x.Id == student.Id && x.Sex == student.Sex && x.FIO == student.FIO && x.GroupId == student.GroupId) == 1;
                    daoForStudent.Delete(student);
                    break;
            }
            Assert.IsTrue(actual);
        }


        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void DeleteTest_ExsistElementInDatase_OneRowWillBeRemovedFromTheTableInTheDatabase(int mode)
        {
            bool actual = false;
            switch (mode)
            {
                case 1:

                    Student student = new Student() { Sex = "F", BirthDay = DateTime.Parse("2002-07-09"), FIO = "Akrickiys Dabb tt", GroupId = 2, Id = 4 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    student.Id = Convert.ToInt32(daoForStudent.Create(student));
                    daoForStudent.Delete(student);
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count(x => x.FIO == student.FIO) == 0;
                    break;
                case 2:
                    Exam exam = new Exam() { NameOfExam = "hhhhhhh", TypeOfExam = "Exam", Id = 3 };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    exam.Id = Convert.ToInt32(daoForExam.Create(exam));
                    daoForExam.Delete(exam);
                    List<Exam> list = daoForExam.ReadAll();
                    actual = list.Count(x => x.NameOfExam == exam.NameOfExam && x.TypeOfExam == exam.TypeOfExam) == 0;
                    break;
            }
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void UpdateTest_ExsistRowInDatase_RowWillBeChenged()
        {
            bool actual = false;
            GroupOfStudent oldGroup = new GroupOfStudent() { NameOfGroup = "GIP-21" };
            GroupOfStudent newGroup = new GroupOfStudent() { NameOfGroup = "ITI-22" };
            DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
            IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
            oldGroup.Id = Convert.ToInt32(daoForGroup.Create(oldGroup));
            newGroup.Id = oldGroup.Id;
            List<GroupOfStudent> groups = daoForGroup.ReadAll();
            actual = !groups.Exists(x => x.NameOfGroup == "ITI-22");
            daoForGroup.Update(oldGroup, newGroup);
            groups = daoForGroup.ReadAll();
            actual = actual && groups.Exists(x => x.NameOfGroup == "ITI-22") && !groups.Exists(x => x.NameOfGroup == "GIP-21");
            daoForGroup.Delete(newGroup);
            Assert.IsTrue(actual);
        }


        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void DeleteTest_NonExsistElementInDatase_TheDatabaseWillNotChange(int mode)
        {
            int actual = 0, expected = 0;
            switch (mode)
            {
                case 1:
                    Student student = new Student() { Sex = "F", BirthDay = DateTime.Parse("2002-07-09"), FIO = "Tarelko Denis Victorivich", GroupId = 3, Id = 4 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count();
                    daoForStudent.Delete(student);
                    students = daoForStudent.ReadAll();
                    actual = students.Count - actual;
                    break;
                case 2:
                    List<Exam> list;
                    Exam russian = new Exam() { NameOfExam = "Russia", TypeOfExam = "Exam", Id = 3 };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);

                    list = daoForExam.ReadAll();
                    actual = list.Count;
                    daoForExam.Delete(russian);
                    list = daoForExam.ReadAll();
                    actual = actual - list.Count;
                    break;
            }
            Assert.AreEqual(actual, expected);
        }


        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void UpdateTest_NonExsistRowInDatase_TheDatabaseWillNotChange(int mode)
        {
            bool actual = false;
            switch (mode)
            {
                case 1:
                    Student studentNew = new Student() { Sex = "F", FIO = "jjjjjjj Alex Igoreevna", GroupId = 1, Id = 2, BirthDay = DateTime.Parse("2002-10-01") };
                    Student studentOld = new Student() { Sex = "M", BirthDay = DateTime.Parse("2002-07-09"), FIO = "Domorad Maksim Vladimirovich", GroupId = 1, Id = 2 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count(x => x.FIO == "jjjjjjj Alex Igoreevna" && x.BirthDay == DateTime.Parse("2002-10-01")) == 0 && students.Count(x => x.FIO == "Domorad Maksim Vladimirovich" && x.BirthDay == DateTime.Parse("2002-07-09")) == 0;
                    daoForStudent.Update(studentOld, studentNew);
                    students = daoForStudent.ReadAll();
                    actual = actual && students.Count(x => x.FIO == "jjjjjjj Alex Igoreevna" && x.BirthDay == DateTime.Parse("2002-10-01")) == 0 && students.Count(x => x.FIO == "Domorad Maksim Vladimirovich" && x.BirthDay == DateTime.Parse("2002-07-09")) == 0;
                    break;
                case 2:
                    Exam oldExam = new Exam() { NameOfExam = "Grecks", TypeOfExam = "Test", Id = 2 };
                    Exam newExam = new Exam() { NameOfExam = "Grecks", TypeOfExam = "Exam", Id = 2 };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    List<Exam> list = daoForExam.ReadAll();

                    actual = list.Count(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Test") == 0 && list.Count(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Exam") == 0;
                    daoForExam.Update(oldExam, newExam);
                    list = daoForExam.ReadAll();
                    actual = actual && list.Count(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Test") == 0 && list.Count(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Exam") == 0;
                    break;
            }
            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void ReadAll_PerformingOperationsWithClosedConnection_InvalidOperationException()
        {
            bool actual = false;
            TransactSqlDao<List<string>> dao = TransactSqlDao<List<string>>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            dao.CloseConnect();
            try
            {
                dao.ReadAll();
            }
            catch(InvalidOperationException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void UpdateTest_PerformingOperationsWithClosedConnection_InvalidOperationException()
        {
            bool actual = false;
            TransactSqlDao<List<int>> dao = TransactSqlDao<List<int>>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            dao.CloseConnect();
            try
            {
                dao.Update(null,null);
            }
            catch (InvalidOperationException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CreateTest_PerformingOperationsWithClosedConnection_InvalidOperationException()
        {
            bool actual = false;
            TransactSqlDao<System.Text.StringBuilder> dao = TransactSqlDao<System.Text.StringBuilder>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            dao.CloseConnect();
            try
            {
                dao.Create(null);
            }
            catch (InvalidOperationException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void DeleteTest_PerformingOperationsWithClosedConnection_InvalidOperationException()
        {
            bool actual = false;
            TransactSqlDao<List<double>> dao = TransactSqlDao<List<double>>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            dao.CloseConnect();
            try
            {
                dao.Delete(null);
            }
            catch (InvalidOperationException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void UpdateTest_NullIsUseAsObject_ArgumentNullExceptionThrown()
        {
            bool actual = false;
            TransactSqlDao<List<float>> dao = TransactSqlDao<List<float>>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            try
            {
                dao.Update(null, null);
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CreateTest__NullIsUseAsObject_ArgumentNullExceptionThrown()
        {
            bool actual = false;
            TransactSqlDao<List<float>> dao = TransactSqlDao<List<float>>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            try
            {
                dao.Create(null);
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void DeleteTest_NullIsUseAsObject_ArgumentNullExceptionThrown()
        {
            bool actual = false;
            TransactSqlDao<List<float>> dao = TransactSqlDao<List<float>>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            try
            {
                dao.Delete(null);
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void GetDaoTests_UsingClassWithoutPUblicProperties_ArgumentExceptionsThrown()
        {
            bool actual = false;
            TransactSqlDao<List<float>> dao = TransactSqlDao<List<float>>.GetDao(new System.Data.SqlClient.SqlConnection(connectionString));
            try
            {
                dao.Delete(null);
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }

    }
}
