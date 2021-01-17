using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DAOLib.SqlDao;
using DAOLib;
using University;
namespace DaoLibTests
{



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////                                           !!!ATTENTION -- 1!!!                                               //////
    ///////To work correctly, you need to have a database created and populated with scripts from the same repository    //////
    ///////                     The scripts are located in the scripts for database deployment folder                    //////
    ///////                                           !!!ATTENTION -- 2!!!                                               //////                       
    ///////If necessary change the database "connectionString" in the static constructor of the TransatSqlDaoTest class  //////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [TestClass]
    public class TransatSqlDaoTest
    {
        private static string connectionString;
        static TransatSqlDaoTest()
        {
            connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=UniversityDatabase; Integrated Security=True";
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
                    actual = list.Where(x => x.TypeOfExam == "Exam" && x.NameOfExam == "MMA").Count() == 1 && list.Where(x => x.TypeOfExam == "Test" && x.NameOfExam == "Mechanics").Count() == 1;
                    
                    break;
                case 2:
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Where(x => x.NameOfGroup == "IP-21").Count() == 1 && groups.Where(x => x.NameOfGroup == "ML-42").Count() == 1;
                    
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
            GroupOfStudent group = null;
            bool actual = false;
            switch (mode)
            {
                case 1:
                    Exam spanish = new Exam() { NameOfExam = "Chiniess", TypeOfExam = "Exam" };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    spanish.Id = Convert.ToInt32(daoForExam.Create(spanish));
                    List<Exam> list = daoForExam.ReadAll();
                    actual = list.Count(x=>x.Id == spanish.Id && x.NameOfExam == "Chiniess" && x.TypeOfExam == "Exam")==1;
                    
                    break;
                case 2:               
                    group = new GroupOfStudent() {NameOfGroup = "ZK-37" };
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    group.Id = Convert.ToInt32(daoForGroup.Create(group));
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Count(x=>x.Id == group.Id && x.NameOfGroup == "ZK-37") ==1;
                    
                    break;
                case 3:
                    Student student = new Student() {Sex = "F", BirthDay= DateTime.Parse("2002-07-09"), FIO ="Artem Nikita Konstantinovna", GroupId = 3 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    student.Id = Convert.ToInt32(daoForStudent.Create(student));
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count(x=>x.Id == student.Id && x.Sex == student.Sex && x.FIO == student.FIO && x.GroupId == student.GroupId)==1;
                    
                    break;
            }
            Assert.IsTrue(actual);
        }

        
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void DeleteTest_ExsistElementInDatase_OneRowWillBeRemovedFromTheTableInTheDatabase(int mode)
        {
            GroupOfStudent group = null;
            bool actual = false;
            switch (mode)
            {
                case 1:
                    Student student = new Student() { Sex = "F", BirthDay = DateTime.Parse("2002-07-09"), FIO = "Akrickiys Dabb tt", GroupId = 3, Id = 4 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    student.Id = Convert.ToInt32(daoForStudent.Create(student));
                    daoForStudent.Delete(student);
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count(x=> x.FIO == student.FIO)==0;
                    
                    break;
                case 2:
                    Exam exam = new Exam() { NameOfExam = "hhhhhhh", TypeOfExam = "Exam", Id = 3 };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    exam.Id = Convert.ToInt32(daoForExam.Create(exam));
                    daoForExam.Delete(exam);
                    List<Exam> list = daoForExam.ReadAll();
                    actual = list.Count(x => x.NameOfExam == "hhhhhhh" && x.TypeOfExam == "Exam")==0;
                    
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
            Assert.AreEqual(actual,expected);
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
                    actual = students.Where(x => x.FIO == "jjjjjjj Alex Igoreevna" && x.BirthDay == DateTime.Parse("2002-10-01")).Count() == 0 && students.Where(x => x.FIO == "Domorad Maksim Vladimirovich" && x.BirthDay == DateTime.Parse("2002-07-09")).Count() == 0;
                    daoForStudent.Update(studentOld, studentNew);
                    students = daoForStudent.ReadAll();
                    actual = actual && students.Where(x => x.FIO == "jjjjjjj Alex Igoreevna" && x.BirthDay == DateTime.Parse("2002-10-01")).Count() == 0 && students.Where(x => x.FIO == "Domorad Maksim Vladimirovich" && x.BirthDay == DateTime.Parse("2002-07-09")).Count() == 0;
                    
                    break;
                case 2:
                    Exam oldExam = new Exam() { NameOfExam = "Grecks", TypeOfExam = "Test", Id = 2 };
                    Exam newExam = new Exam() { NameOfExam = "Grecks", TypeOfExam = "Exam", Id = 2 };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    List<Exam> list = daoForExam.ReadAll();

                    actual = list.Where(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Test").Count() == 0 && list.Where(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Exam").Count() == 0;
                    daoForExam.Update(oldExam, newExam);
                    list = daoForExam.ReadAll();
                    actual = actual && list.Where(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Test").Count() == 0 && list.Where(x => x.NameOfExam == "Grecks" && x.TypeOfExam == "Exam").Count() == 0;
                    
                    break;
            }
            Assert.IsTrue(actual);
        }


    }
}
