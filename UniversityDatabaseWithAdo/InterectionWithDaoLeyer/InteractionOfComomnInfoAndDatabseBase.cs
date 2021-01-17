using DAOLib;
using System.Collections.Generic;
using System.Linq;
using University;

namespace InteractionOfTheDatabaseAndTheUniversity
{
    public class InteractionOfComomnInfoAndDatabseBase
    {
        private static DaoFactory<GroupOfStudent> groupOfStudentFactory;
        private static DaoFactory<Student> studentFactory;
        private static DaoFactory<Exam> examFactory;
        private static DaoFactory<ExamInfo> examInfoFactory;
        private static DaoFactory<StudentSessionIfno> studentSessionIfnoFactory;
        private static DaoFactory<UniversitySessionInfo> universitySessionInfoFactory;

        private IDao<GroupOfStudent> groupOfStudentDao;
        private IDao<Student> studentDao;
        private IDao<Exam> examDao;
        private IDao<ExamInfo> examInfoDao;
        private IDao<StudentSessionIfno> studentSessionIfnoDao;
        private IDao<UniversitySessionInfo> universitySessionInfoDao;

        private static List<GroupOfStudent> groupOfStudents;
        private static List<Student> students;
        private static List<Exam> exams;
        private static List<ExamInfo> examInfos;
        private static List<StudentSessionIfno> studentSessionIfnos;
        private static List<UniversitySessionInfo> universitySessionInfos;

        public InteractionOfComomnInfoAndDatabseBase(string connectionString)
        {

            groupOfStudentDao = groupOfStudentFactory.CreateDao("TransatSqlDao", connectionString);
            studentDao = studentFactory.CreateDao("TransatSqlDao", connectionString);
            examDao = examFactory.CreateDao("TransatSqlDao", connectionString);
            examInfoDao = examInfoFactory.CreateDao("TransatSqlDao", connectionString);
            studentSessionIfnoDao = studentSessionIfnoFactory.CreateDao("TransatSqlDao", connectionString);
            universitySessionInfoDao = universitySessionInfoFactory.CreateDao("TransatSqlDao", connectionString);

        }
        static InteractionOfComomnInfoAndDatabseBase()
        {
            groupOfStudentFactory = new DaoFactory<GroupOfStudent>();
            studentFactory = new DaoFactory<Student>();
            examFactory = new DaoFactory<Exam>();
            examInfoFactory = new DaoFactory<ExamInfo>();
            studentSessionIfnoFactory = new DaoFactory<StudentSessionIfno>();
            universitySessionInfoFactory = new DaoFactory<UniversitySessionInfo>();
        }

        public List<CommonInfo> GetCommonInfoFromDatabase()
        {
            groupOfStudents = groupOfStudentDao.ReadAll();
            students = studentDao.ReadAll();
            exams = examDao.ReadAll();
            examInfos = examInfoDao.ReadAll();
            studentSessionIfnos = studentSessionIfnoDao.ReadAll();
            universitySessionInfos = universitySessionInfoDao.ReadAll();

            var extendedListWithGroupId = from stdInfo in studentSessionIfnos
                                          join student in students on stdInfo.StudentId equals student.Id
                                          select new { student.GroupId, student.FIO, student.Sex, student.BirthDay,student.Id, stdInfo.ExamInfoId, stdInfo.Mark };
            var extendedListWithExamNameAndSemestr = from data in extendedListWithGroupId
                                                     join examInfo in examInfos on data.ExamInfoId equals examInfo.Id
                                                     select new { data.GroupId, data.FIO, data.Sex, data.Id,data.BirthDay, examInfo.ExamId, examInfo.NumberSession, data.Mark };
            var result = extendedListWithExamNameAndSemestr.OrderBy(x => x.GroupId).ThenBy(x => x.NumberSession);


            List<CommonInfo> commons = new List<CommonInfo>();
            foreach (var info in result)
            {
                commons.Add(new CommonInfo(info.GroupId, info.Id,info.FIO, info.ExamId, info.NumberSession, info.Mark));
            }
            return commons;
        }    
    }
}
