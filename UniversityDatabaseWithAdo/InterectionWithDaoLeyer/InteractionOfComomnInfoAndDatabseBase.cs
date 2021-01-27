using DAOLib;
using System.Collections.Generic;
using System.Linq;
using University;

namespace InteractionOfTheDatabaseAndTheUniversity
{
    public class InteractionOfComomnInfoAndDatabseBase
    {
        private static DaoFactory<Student> studentFactory;
        private static DaoFactory<ExamInfo> examInfoFactory;
        private static DaoFactory<StudentSessionIfno> studentSessionIfnoFactory;

        private IDao<Student> studentDao;
        private IDao<ExamInfo> examInfoDao;
        private IDao<StudentSessionIfno> studentSessionIfnoDao;

        private static List<Student> students;
        private static List<ExamInfo> examInfos;
        private static List<StudentSessionIfno> studentSessionIfnos;

        public InteractionOfComomnInfoAndDatabseBase(string connectionString)
        {
            studentDao = studentFactory.CreateDao("TransatSqlDao", connectionString);
            examInfoDao = examInfoFactory.CreateDao("TransatSqlDao", connectionString);
            studentSessionIfnoDao = studentSessionIfnoFactory.CreateDao("TransatSqlDao", connectionString);
        }
        static InteractionOfComomnInfoAndDatabseBase()
        {
            studentFactory = new DaoFactory<Student>();
            examInfoFactory = new DaoFactory<ExamInfo>();
            studentSessionIfnoFactory = new DaoFactory<StudentSessionIfno>();
        }

        public List<CommonInfo> GetCommonInfoFromDatabase()
        {
            students = studentDao.ReadAll();
            examInfos = examInfoDao.ReadAll();
            studentSessionIfnos = studentSessionIfnoDao.ReadAll();

            var extendedListWithGroupId = from stdInfo in studentSessionIfnos
                                          join student in students on stdInfo.StudentId equals student.Id
                                          select new { student.GroupId, student.FIO, student.Sex, student.BirthDay,student.Id, stdInfo.ExamInfoId, stdInfo.Mark };
            var extendedListWithExamNameAndSemestr = from data in extendedListWithGroupId
                                                     join examInfo in examInfos on data.ExamInfoId equals examInfo.Id
                                                     select new { data.GroupId, data.FIO, data.Sex, data.Id, data.BirthDay, examInfo.ExamId, examInfo.NumberSession, data.Mark };
            var result = extendedListWithExamNameAndSemestr.OrderBy(x => x.GroupId).ThenBy(x => x.NumberSession);

            List<CommonInfo> commons = new List<CommonInfo>();

            foreach (var info in result)
            {
                commons.Add(new CommonInfo(info.GroupId, info.Id,info.FIO,info.Sex,info.BirthDay, info.ExamId, info.NumberSession, info.Mark));
            }
            return commons;
        }    
    }
}
