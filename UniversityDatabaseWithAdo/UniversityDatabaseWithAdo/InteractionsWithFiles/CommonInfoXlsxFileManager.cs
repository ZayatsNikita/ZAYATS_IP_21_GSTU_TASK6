using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using University;

namespace InteractionsWithFiles
{
    /// <summary>
    /// This class is intended for writing session data to xml files.
    /// </summary>
    public static class CommonInfoXlsxFileManager
    {
        /// <summary>
        /// A static constructor is used to set constants.
        /// </summary>
        static CommonInfoXlsxFileManager()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        /// <summary>
        /// The static method used to record the results of the sessions to a file in xlsx format.
        /// </summary>
        /// <param name="filePath">Specifies the path to save the file to.</param>
        /// <param name="data">Data on the results of the session.</param>
        /// <exception cref="ArgumentNullException">Thrown if filePath of data quuals to null.</exception>
        public static void SaveResaltOfSessionsToXlsxTable(string filePath, List<CommonInfo> data)
        {
            if(filePath == null || data == null)
            {
                throw new ArgumentNullException();
            }

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells[1, 1].Value = "Group Id";
                worksheet.Cells[1, 2].Value = "Students Fio";
                worksheet.Cells[1, 3].Value = "Exam Id";
                worksheet.Cells[1, 4].Value = "Number of session";
                worksheet.Cells[1, 5].Value = "Mark";

                int i = 2;
                foreach (var info in data)
                {
                    worksheet.Cells[i, 1].Value = info?.GroupId;
                    worksheet.Cells[i, 2].Value = info?.StudentFio;
                    worksheet.Cells[i, 3].Value = info?.ExamId;
                    worksheet.Cells[i, 4].Value = info?.NumberSession;
                    worksheet.Cells[i, 5].Value = info?.Mark;
                    i++;
                }
                FileInfo fi = new FileInfo(filePath);
                excelPackage.SaveAs(fi);
            }

        }

        /// <summary>
        /// A method that records the value of the average, maximum and minimum score for groups for different semesters.
        /// </summary>
        /// <param name="filePath">Specifies the path to save the file to.</param>
        /// <param name="data">Data on the results of the session.</param>
        /// <exception cref="ArgumentNullException">Thrown if filePath of data quuals to null or if there are null elements in data.</exception>
        public static void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(string filePath, List<CommonInfo> data)
        {
            if (filePath == null || data == null || data.Count(x=>x==null)!=0)
            {
                throw new ArgumentNullException();
            }
            var minMaxAvgGroup = data.Select(x => new {
                x.GroupId,
                x.NumberSession,
                x.ExamId,
                AvgMark = data.Where(y => y.NumberSession == x.NumberSession && y.GroupId == x.GroupId).Average(l => l.Mark),
                MaxMark = data.Where(z => z.NumberSession == x.NumberSession && z.GroupId == x.GroupId).Max(t => t.Mark),
                MinMark = data.Where(f => f.NumberSession == x.NumberSession && f.GroupId == x.GroupId).Min(q => q.Mark)
            });
            
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
                    worksheet.Cells[i, 1].Value = info?.GroupId;
                    worksheet.Cells[i, 2].Value = info?.NumberSession;
                    worksheet.Cells[i, 3].Value = info?.ExamId;
                    worksheet.Cells[i, 4].Value = info?.AvgMark;
                    worksheet.Cells[i, 5].Value = info?.MaxMark;
                    worksheet.Cells[i, 6].Value = info?.MinMark;
                    i++;
                }

                FileInfo fi = new FileInfo(filePath);
                excelPackage.SaveAs(fi);
            }

        }
    }
}
