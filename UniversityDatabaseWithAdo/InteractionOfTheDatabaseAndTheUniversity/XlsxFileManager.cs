using DAOLib;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using University;

namespace InteractionOfTheDatabaseAndTheUniversity
{
    public class XlsxFileManager
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

        public XlsxFileManager(string connectionString)
        {

            groupOfStudentDao = groupOfStudentFactory.CreateDao("TransatSqlDao", connectionString);
            studentDao = studentFactory.CreateDao("TransatSqlDao", connectionString);
            examDao = examFactory.CreateDao("TransatSqlDao", connectionString);
            examInfoDao = examInfoFactory.CreateDao("TransatSqlDao", connectionString);
            studentSessionIfnoDao = studentSessionIfnoFactory.CreateDao("TransatSqlDao", connectionString);
            universitySessionInfoDao = universitySessionInfoFactory.CreateDao("TransatSqlDao", connectionString);

        }
        static XlsxFileManager()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            groupOfStudentFactory = new DaoFactory<GroupOfStudent>();
            studentFactory = new DaoFactory<Student>(); 
            examFactory = new DaoFactory<Exam>(); 
            examInfoFactory= new DaoFactory<ExamInfo>();
            studentSessionIfnoFactory= new DaoFactory<StudentSessionIfno>();
            universitySessionInfoFactory= new DaoFactory<UniversitySessionInfo>();
        }


        
        public void SaveTheResultsOfEachSessionByGroupToTable(string filePath)
        {
            groupOfStudents = groupOfStudentDao.ReadAll();
            students = studentDao.ReadAll();
            exams = examDao.ReadAll();
            examInfos = examInfoDao.ReadAll();
            studentSessionIfnos = studentSessionIfnoDao.ReadAll();
            universitySessionInfos = universitySessionInfoDao.ReadAll();

            if(groupOfStudents.Count==0 || students.Count==0 || exams.Count==0 || examInfos.Count ==0 || studentSessionIfnos.Count == 0 || universitySessionInfos.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var extendedListWithGroupId = from stdInfo in studentSessionIfnos
                                           join student in students on stdInfo.StudentId equals student.Id
                                           select new { student.GroupId, student.FIO, stdInfo.ExamInfoId, stdInfo.Mark };
            var extendedListWithExamNameAndSemestr = from data in extendedListWithGroupId
                                                     join examInfo in examInfos on data.ExamInfoId equals examInfo.Id
                                                     select new { data.GroupId, data.FIO, examInfo.ExamId, examInfo.NumberSession, data.Mark };
            var result = extendedListWithExamNameAndSemestr.OrderBy(x => x.GroupId).ThenBy(x=>x.NumberSession);


            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells[1, 1].Value = "Group Id";
                worksheet.Cells[1, 2].Value = "Students Fio";
                worksheet.Cells[1, 3].Value = "Exam Id";
                worksheet.Cells[1, 4].Value = "Number of session";
                worksheet.Cells[1, 5].Value = "Mark";

                int i = 2;
                foreach (var info in result)
                {
                    worksheet.Cells[i, 1].Value = info.GroupId;
                    worksheet.Cells[i, 2].Value = info.FIO;
                    worksheet.Cells[i, 3].Value = info.ExamId;
                    worksheet.Cells[i, 4].Value = info.NumberSession;
                    worksheet.Cells[i, 5].Value = info.Mark;
                    i++;
                }

                FileInfo fi = new FileInfo(filePath);
                excelPackage.SaveAs(fi);
            }

        }
        public void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(string filePath)
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

            var extendedListWithGroupId = from stdInfo in studentSessionIfnos
                                          join student in students on stdInfo.StudentId equals student.Id
                                          select new { student.GroupId, student.FIO, stdInfo.ExamInfoId, stdInfo.Mark };
            var extendedListWithExamNameAndSemestr = from data in extendedListWithGroupId
                                                     join examInfo in examInfos on data.ExamInfoId equals examInfo.Id
                                                     select new { data.GroupId, data.FIO, examInfo.ExamId, examInfo.NumberSession, data.Mark };
            extendedListWithExamNameAndSemestr.OrderBy(x => x.GroupId);
            var result = extendedListWithExamNameAndSemestr.OrderBy(x => x.GroupId).ThenBy(x => x.NumberSession);
            
            var minMaxAvgGroup = result.Select( x=> new { x.GroupId, x.NumberSession,x.ExamId, AvgMark = result.Where(y=>y.NumberSession==x.NumberSession && y.GroupId==x.GroupId).Average(l=>l.Mark), MaxMark = result.Where(z => z.NumberSession == x.NumberSession && z.GroupId == x.GroupId).Max(t => t.Mark), MinMark= result.Where(f => f.NumberSession == x.NumberSession && f.GroupId == x.GroupId).Min(q=> q.Mark)});
            var toSave = minMaxAvgGroup.OrderBy(x => x.NumberSession).Distinct();

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells[1, 1].Value = "Group Id";
                worksheet.Cells[1, 2].Value = "Number of session";
                worksheet.Cells[1, 3].Value = "Exam Id";
                worksheet.Cells[1, 4].Value = "AvgMark";
                worksheet.Cells[1, 5].Value = "MaxMark";
                worksheet.Cells[1, 6].Value = "MinMark";

                int i = 2;
                foreach (var info in toSave)
                {
                    worksheet.Cells[i, 1].Value = info.GroupId;
                    worksheet.Cells[i, 2].Value = info.NumberSession;
                    worksheet.Cells[i, 3].Value = info.ExamId;
                    worksheet.Cells[i, 4].Value = info.AvgMark;
                    worksheet.Cells[i, 5].Value = info.MaxMark;
                    worksheet.Cells[i, 6].Value = info.MinMark;
                    i++;
                }

                FileInfo fi = new FileInfo(filePath);
                excelPackage.SaveAs(fi);
            }

        }
    }
}
