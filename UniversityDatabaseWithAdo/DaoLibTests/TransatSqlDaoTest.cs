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
        [DataRow(3)]
        public void ReadAllTest_GetDataFromDatabase_ExsistElements(int mode)
        {

            bool actual = false;
            switch (mode)
            {
                case 1:
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    List<Exam> list = daoForExam.ReadAll();
                    actual = list.Count == 2 && list[0].Id == 1 && list[1].Id == 2 && list[0].TypeOfExam == "Exam" && list[1].TypeOfExam == "Test" && list[0].NameOfExam == "MMA" && list[1].NameOfExam == "Mechanics";
                    break;
                case 2:
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Count == 2 && groups[0].Id==1 && groups[0].NameOfGroup == "IP-21" && groups[1].Id == 2 && groups[1].NameOfGroup == "ML-42";
                    break;
                case 3:
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    List<Student> students = daoForStudent.ReadAll();
                    
                    actual = students.Count == 3 && students[0].Id == 1 && students[0].Sex == "M" && students[0].FIO == "Zayats Nikita Sergeevich" && students[0].GroupId == 1 && students[0].BirthDay == DateTime.Parse("01.10.2001")
                    && students[2].Id == 3 && students[2].Sex == "M" && students[2].FIO == "Tsmyg Dmitry Alexandrovich" && students[2].GroupId == 2 && students[2].BirthDay == DateTime.Parse("1998-09-13");
                    break;
            }

            Assert.IsTrue(actual);
         
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]/*Переименовать этот метод после перзаписи данных в бд*/
        public void CreateTest_SetValidDataToDatabase_NewElementInDatabase(int mode)
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
                    actual = list.Count == 3 && list[2].Id == 3 && list[2].Id == spanish.Id && list[2].NameOfExam == "Chiniess" && list[2].TypeOfExam == "Exam";
                    break;
                case 2:               
                    group = new GroupOfStudent() {NameOfGroup = "ZK-37" };
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    group.Id = Convert.ToInt32(daoForGroup.Create(group));
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Count == 3 && groups[2].Id == 3 && groups[2].Id == group.Id && groups[2].NameOfGroup == "ZK-37";
                    break;
                case 3:
                    Student student = new Student() {Sex = "F", BirthDay= DateTime.Parse("2002-07-09"), FIO ="Chernix Nikita Konstantinovna", GroupId = 3 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    student.Id = Convert.ToInt32(daoForStudent.Create(student));
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count == 4 && students[3].Id == 4 && students[3].Sex == "F" && students[3].FIO == "Chernix Nikita Konstantinovna" && students[3].GroupId == 3 && students[3].BirthDay == DateTime.Parse("2002-07-09");
                    break;
            }
            Assert.IsTrue(actual);
        }

        //добавить второй модуль только после выполнения второго этапа
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void DeleteTest_ExsistRowInDatase_NewElementInDatabase_OneRowWillBeRemovedFromTheTableInTheDatabase(int mode)
        {
            GroupOfStudent group = null;
            bool actual = false;
            switch (mode)
            {
                case 1:
                    Student student = new Student() { Sex = "F", BirthDay = DateTime.Parse("2002-07-09"), FIO = "Chernix Nikita Konstantinovna", GroupId = 3, Id = 4 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    daoForStudent.Delete(student);
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count == 3 && !students.Contains(student);
                    break;
                case 2:
                    Exam spanish = new Exam() { NameOfExam = "Chiniess", TypeOfExam = "Exam", Id = 3 };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    daoForExam.Delete(spanish);
                    List<Exam> list = daoForExam.ReadAll();
                    actual = list.Count == 2 && !list.Contains(spanish);
                    break;
                case 3:
                    group = new GroupOfStudent() { NameOfGroup = "ZK-37", Id= 3 };
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    daoForGroup.Delete(group);
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Count == 2 && !groups.Contains(group);
                    break;
            }
            Assert.IsTrue(actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void UpdateTest_ExsistRowInDatase_RowWillBeChenged(int mode)
        {
            bool actual = false;
            switch (mode)
            {
                case 1:
                    Student studentOld = new Student() { Sex = "F", FIO = "Pishuck Alex Igoreevna", GroupId = 1, Id = 2, BirthDay = DateTime.Parse("2002-10-01") };
                    Student studentNew = new Student() { Sex = "F", BirthDay = DateTime.Parse("2002-07-09"), FIO = "Chernix Nikita Konstantinovna", GroupId = 1, Id = 2 };
                    DaoFactory<Student> daoFactoryForStudent = new DaoFactory<Student>();
                    IDao<Student> daoForStudent = daoFactoryForStudent.CreateDao("TransatSqlDao", connectionString);
                    List<Student> students = daoForStudent.ReadAll();
                    actual = students.Count == 3 && students.Where(x => x.FIO == "Pishuck Alex Igoreevna" && x.BirthDay == DateTime.Parse("2002-10-01")).Count()==1 && students.Where(x => x.FIO == "Chernix Nikita Konstantinovna" && x.BirthDay == DateTime.Parse("2002-07-09")).Count() == 0;
                    daoForStudent.Update(studentOld,studentNew);
                    students = daoForStudent.ReadAll();
                    actual = actual && students.Count == 3 && students.Where(x => x.FIO == "Pishuck Alex Igoreevna" && x.BirthDay == DateTime.Parse("2002-10-01")).Count() == 0 && students.Where(x => x.FIO == "Chernix Nikita Konstantinovna" && x.BirthDay == DateTime.Parse("2002-07-09")).Count() == 1;
                    break;
                case 2:
                    Exam oldExam = new Exam() { NameOfExam = "Mechanics", TypeOfExam = "Test", Id = 2 };
                    Exam newExam = new Exam() { NameOfExam = "Mechanics", TypeOfExam = "Exam", Id = 2 };
                    DaoFactory<Exam> daoFactoryForExam = new DaoFactory<Exam>();
                    IDao<Exam> daoForExam = daoFactoryForExam.CreateDao("TransatSqlDao", connectionString);
                    List<Exam> list = daoForExam.ReadAll();

                    actual =  list.Where(x => x.NameOfExam == "Mechanics" && x.TypeOfExam == "Test").Count() == 1 && list.Where(x => x.NameOfExam == "Mechanics" && x.TypeOfExam == "Exam").Count() == 0;
                    daoForExam.Update(oldExam,newExam);
                    list = daoForExam.ReadAll();
                    actual =  actual  && list.Count == 2 && list.Where(x => x.NameOfExam == "Mechanics" && x.TypeOfExam == "Test").Count() == 0 && list.Where(x => x.NameOfExam == "Mechanics" && x.TypeOfExam == "Exam").Count() == 1;
                    break;

                case 3:
                    GroupOfStudent oldGroup = new GroupOfStudent() { NameOfGroup = "IP-21", Id = 1 };
                    GroupOfStudent newGroup = new GroupOfStudent() { NameOfGroup = "ITI-22", Id = 1 };
                    DaoFactory<GroupOfStudent> daoFactoryForGroup = new DaoFactory<GroupOfStudent>();
                    IDao<GroupOfStudent> daoForGroup = daoFactoryForGroup.CreateDao("TransatSqlDao", connectionString);
                    List<GroupOfStudent> groups = daoForGroup.ReadAll();
                    actual = groups.Count == 2 && !groups.Exists(x => x.NameOfGroup == "ITI-22");
                    daoForGroup.Update(oldGroup, newGroup);
                    groups = daoForGroup.ReadAll();
                    actual = actual && groups.Count == 2 && groups.Exists(x => x.NameOfGroup == "ITI-22") && !groups.Exists(x => x.NameOfGroup == "IP-21"); 
                    break;
            }
            Assert.IsTrue(actual);
        }

    }
}
