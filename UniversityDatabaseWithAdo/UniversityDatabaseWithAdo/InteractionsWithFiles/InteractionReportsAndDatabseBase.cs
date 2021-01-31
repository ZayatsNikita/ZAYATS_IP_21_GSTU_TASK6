using DAOLib;
using System.Collections.Generic;
using System.Linq;
using University;


namespace InteractionOfTheDatabaseAndTheUniversity
{
    public static class InteractionReportsAndDatabseBase
    {
        private static DaoFactory<Student> studentFactory;
        private static DaoFactory<ExamInfo> examInfoFactory;
        private static DaoFactory<StudentSessionIfno> studentSessionIfnoFactory;

        private static IDao<Student> studentDao;
        private static IDao<ExamInfo> examInfoDao;
        private static IDao<StudentSessionIfno> studentSessionIfnoDao;

     

        /// <summary>
        /// Used for initializing factories.
        /// </summary>
        static InteractionReportsAndDatabseBase()
        {
            studentFactory = new DaoFactory<Student>();
            examInfoFactory = new DaoFactory<ExamInfo>();
            studentSessionIfnoFactory = new DaoFactory<StudentSessionIfno>();
        }

        /// <summary>
        /// A method which is used to create connections to database
        /// </summary>
        /// <param name="dataBaseType">Type of necessary database</param>
        /// <param name="connectionString">String with params of connections</param>
        /// <exception cref="System.Data.SqlClient.SqlException">Thrown if there is't necessaty dataabse</exception>
        /// <exception cref="ArgumentNullException">Thrown if dataBaseType or connectionString is equals to null</exception>
        /// <exception cref="ArgumentException">Thrown if there are no database with specified name.</exception>
        public static void OpenConnections(string dataBaseType, string connectionString)
        {
            studentDao = studentFactory.CreateDao(dataBaseType, connectionString);
            examInfoDao = examInfoFactory.CreateDao(dataBaseType, connectionString);
            studentSessionIfnoDao = studentSessionIfnoFactory.CreateDao(dataBaseType, connectionString);
        }

        /// <summary>
        /// A method that extracts data from the database and generates a report based on it.
        /// </summary>
        /// <returns>List of CommonInfo</returns>
        /// <exception cref="System.Data.SqlClient.SqlException">Thrown if there are problems in databse</exception>
        public static List<ReportingReport> GetCommonInfoFromDatabase()
        {
            
            List<Student> students = studentDao.ReadAll();
            List<ExamInfo> examInfos = examInfoDao.ReadAll();
            List<StudentSessionIfno> studentSessionIfnos = studentSessionIfnoDao.ReadAll();

            var extendedListWithGroupId = from stdInfo in studentSessionIfnos
                                          join student in students on stdInfo.StudentId equals student.Id
                                          select new { student.GroupId, student.FIO, student.Sex, student.BirthDay,student.Id, stdInfo.ExamInfoId, stdInfo.Mark };
            var extendedListWithExamNameAndSemestr = from data in extendedListWithGroupId
                                                     join examInfo in examInfos on data.ExamInfoId equals examInfo.Id
                                                     select new { data.GroupId, data.FIO, data.Sex, data.Id, data.BirthDay, examInfo.ExamId, examInfo.NumberSession, data.Mark };
            var result = extendedListWithExamNameAndSemestr.OrderBy(x => x.GroupId).ThenBy(x => x.NumberSession);

            List<ReportingReport> commons = new List<ReportingReport>();

            foreach (var info in result)
            {
                commons.Add(new ReportingReport(info.GroupId, info.Id,info.FIO,info.Sex,info.BirthDay, info.ExamId, info.NumberSession, info.Mark));
            }
            return commons;
        }

        
        
    }
}
