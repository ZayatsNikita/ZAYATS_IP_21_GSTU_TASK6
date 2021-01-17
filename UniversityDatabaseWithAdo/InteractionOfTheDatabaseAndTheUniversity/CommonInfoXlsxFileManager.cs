using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using University;

namespace UserLayer
{
    public static class CommonInfoXlsxFileManager
    {
        
        static CommonInfoXlsxFileManager()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static void SaveComonInfoToXLSX(string filePath, List<CommonInfo> result)
        {
            if(filePath == null || result == null)
            {
                throw new NullReferenceException();
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
                foreach (var info in result)
                {
                    worksheet.Cells[i, 1].Value = info.GroupId;
                    worksheet.Cells[i, 2].Value = info.StudentFio;
                    worksheet.Cells[i, 3].Value = info.ExamId;
                    worksheet.Cells[i, 4].Value = info.NumberSession;
                    worksheet.Cells[i, 5].Value = info.Mark;
                    i++;
                }

                FileInfo fi = new FileInfo(filePath);
                excelPackage.SaveAs(fi);
            }

        }
        
        public static void SaveGroupIdMaxMinAvgMarkBySessionToXlsxTable(string filePath, List<CommonInfo> result)
        {

            if (filePath == null || result == null)
            {
                throw new NullReferenceException();
            }
            var minMaxAvgGroup = result.Select(x => new { x.GroupId, x.NumberSession, x.ExamId, AvgMark = result.Where(y => y.NumberSession == x.NumberSession && y.GroupId == x.GroupId).Average(l => l.Mark), MaxMark = result.Where(z => z.NumberSession == x.NumberSession && z.GroupId == x.GroupId).Max(t => t.Mark), MinMark = result.Where(f => f.NumberSession == x.NumberSession && f.GroupId == x.GroupId).Min(q => q.Mark) });
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
