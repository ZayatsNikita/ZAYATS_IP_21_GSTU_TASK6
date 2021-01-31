using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using University;

namespace InteractionsWithFiles
{
    /// <summary>
    /// This class is intended for writing session data to xml files.
    /// </summary>
    public static class ReportsXlsxFileManager
    {
        /// <summary>
        /// A static constructor is used to set constants.
        /// </summary>
        static ReportsXlsxFileManager()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        /// <summary>
        /// The static method used to record the results of the sessions to a file in xlsx format.
        /// </summary>
        /// <param name="filePath">Specifies the path to save the file to.</param>
        /// <param name="data">Data on the results of the session.</param>
        /// <exception cref="ArgumentNullException">Thrown if filePath of data quuals to null.</exception>
        public static void SaveResaltOfSessionsToXlsxTable(string filePath, List<ReportingReport> data)
        {
            if(filePath == null || data == null)
            {
                throw new ArgumentNullException();
            }
            DAOLib.XlsxDao dao = new DAOLib.XlsxDao(new string[] { "Group Id", "Students Fio", "Exam Id", "Number of session", "Mark" });
            List<Object> list = new List<object>();
            foreach (ReportingReport item in data)
            {
                list.Add(item?.GroupId);
                list.Add(item?.StudentFio);
                list.Add(item?.ExamId);
                list.Add(item?.NumberSession);
                list.Add(item?.Mark);
            }
            dao.SaveDataToFile(filePath,list);
        }

        /// <summary>
        /// A method that records the value of the average, maximum and minimum score for groups for different semesters.
        /// </summary>
        /// <param name="filePath">Specifies the path to save the file to.</param>
        /// <param name="data">Data on the results of the session.</param>
        /// <exception cref="ArgumentNullException">Thrown if filePath of data quuals to null or if there are null elements in data.</exception>
        public static void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(string filePath, List<ReportingReport> data)
        {
            if (filePath == null || data == null || data.Count(x=>x==null)!=0)
            {
                throw new ArgumentNullException();
            }
            var minMaxAvgGroup = data.Select(x => new {
                x.GroupId,
                x.NumberSession,
                AvgMark = data.Where(y => y.NumberSession == x.NumberSession && y.GroupId == x.GroupId).Average(l => l.Mark),
                MaxMark = data.Where(z => z.NumberSession == x.NumberSession && z.GroupId == x.GroupId).Max(t => t.Mark),
                MinMark = data.Where(f => f.NumberSession == x.NumberSession && f.GroupId == x.GroupId).Min(q => q.Mark)
            });

            var toSave = minMaxAvgGroup.Distinct();

            DAOLib.XlsxDao dao = new DAOLib.XlsxDao(new string[] { "Group Id", "Number of session", "AvgMark", "MaxMark", "MinMark" });
            List<Object> list = new List<object>();
            foreach (var item in toSave)
            {
                list.Add(item?.GroupId);
                list.Add(item?.NumberSession);
                list.Add(item?.AvgMark);
                list.Add(item?.MaxMark);
                list.Add(item?.MinMark);
            }
            dao.SaveDataToFile(filePath, list);
        }
    }
}
