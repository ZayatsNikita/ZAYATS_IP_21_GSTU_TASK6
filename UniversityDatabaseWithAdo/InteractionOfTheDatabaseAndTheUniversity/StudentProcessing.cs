using DAOLib;
using System;
using System.Collections.Generic;
using System.Linq;
using University;
namespace InteractionOfTheDatabaseAndTheUniversity
{
    public class StudentProcessing
    {
        private static DaoFactory<GroupOfStudent> groupOfStudentFactory;
        private static DaoFactory<Student> studentFactory;
        private static DaoFactory<Exam> examFactory;
        private static DaoFactory<ExamInfo> examInfoFactory;
        private static DaoFactory<StudentSessionIfno> studentSessionIfnoFactory;
        private static DaoFactory<UniversitySessionInfo> universitySessionInfoFactory;

        private static IDao<GroupOfStudent> groupOfStudentDao;
        private static IDao<Student> studentDao;
        private static IDao<Exam> examDao;
        private static IDao<ExamInfo> examInfoDao;
        private static IDao<StudentSessionIfno> studentSessionIfnoDao;
        private static IDao<UniversitySessionInfo> universitySessionInfoDao;

        private static List<GroupOfStudent> groupOfStudents;
        private static List<Student> students;
        private static List<Exam> exams;
        private static List<ExamInfo> examInfos;
        private static List<StudentSessionIfno> studentSessionIfnos;
        private static List<UniversitySessionInfo> universitySessionInfos;

        public StudentProcessing(string connectionString)
        {

            groupOfStudentDao = groupOfStudentFactory.CreateDao("TransatSqlDao", connectionString);
            studentDao = studentFactory.CreateDao("TransatSqlDao", connectionString);
            examDao = examFactory.CreateDao("TransatSqlDao", connectionString);
            examInfoDao = examInfoFactory.CreateDao("TransatSqlDao", connectionString);
            studentSessionIfnoDao = studentSessionIfnoFactory.CreateDao("TransatSqlDao", connectionString);
            universitySessionInfoDao = universitySessionInfoFactory.CreateDao("TransatSqlDao", connectionString);

        }

        static StudentProcessing()
        {
            groupOfStudentFactory = new DaoFactory<GroupOfStudent>();
            studentFactory = new DaoFactory<Student>();
            examFactory = new DaoFactory<Exam>();
            examInfoFactory = new DaoFactory<ExamInfo>();
            studentSessionIfnoFactory = new DaoFactory<StudentSessionIfno>();
            universitySessionInfoFactory = new DaoFactory<UniversitySessionInfo>();
        }
        public List<Student> ExtractFromTheDatabaseOfStudentsForExpulsion()
        {
            groupOfStudents = groupOfStudentDao.ReadAll();
            students = studentDao.ReadAll();
            exams = examDao.ReadAll();
            examInfos = examInfoDao.ReadAll();
            studentSessionIfnos = studentSessionIfnoDao.ReadAll();
            universitySessionInfos = universitySessionInfoDao.ReadAll();

            if (groupOfStudents.Count == 0 || students.Count == 0 || exams.Count == 0 || examInfos.Count == 0 || studentSessionIfnos.Count == 0 || universitySessionInfos.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var badStudents = studentSessionIfnos.Where(x => x.Mark < 4).Join(students, y => y.StudentId, t => t.Id, (y, t) => new { FIO = t.FIO, Sex = t.Sex, Id = t.Id, GroupId = t.GroupId, Bithday = t.BirthDay });
            var info =  badStudents.Select(x=>new Student(){Id = x.Id, BirthDay = x.Bithday, Sex = x.Sex, FIO = x.FIO, GroupId =x.GroupId } ).GroupBy(y=>y.GroupId);

            List<Student> res = new List<Student>();
            if (info.Count()>0)
            {
                foreach (var item in info)
                {
                    item.
                }   
            }
            //else
            //{
                throw new InvalidOperationException();
            //}


            
        }
    }
}
