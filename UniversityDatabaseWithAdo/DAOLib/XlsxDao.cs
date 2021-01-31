using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DAOLib
{
    /// <summary>
    /// A class designed to write data to a file in Xlsx format.
    /// </summary>
    public class XlsxDao
    {
        /// <summary>
        /// A static constructor is used to initialize constants.
        /// </summary>
        static XlsxDao()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        private string[] titles;
        /// <summary>
        /// A constructor that sets the names of columns in a table.
        /// </summary>
        /// <param name="titles">List of titles of columns</param>
        /// <exception cref="ArgumentNullException">Thrown if titles is null, or if there is elements that equals to null in titles</exception>
        /// <exception cref="ArgumentException">Thrown if there is't title in titles.</exception>
        public XlsxDao(string[] titles)
        {
            if (titles == null || titles.Count(x => x == null) != 0)
            {
                throw new ArgumentNullException();
            }
            if (titles.Length < 1)
            {
                throw new ArgumentException("Minimal count of colomns is 1");
            }
            this.titles = titles;
        }
        /// <summary>
        /// The method which performs the preservation of objects in the table cell.
        /// </summary>
        /// <param name="filePath">The path to the file containing the xlsx table.</param>
        /// <param name="list">List of objects to save.</param>
        ///  <exception cref="ArgumentNullException">Thrown if filePath or list is null, or if there is elements that equals to null in list</exception>
        public void SaveDataToFile(string filePath, IEnumerable<object> list)
        {
            if (filePath == null || list == null || list.Count(x => x == null) != 0)
            {
                throw new ArgumentNullException();
            }
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                int j = 1;
                foreach (string item in titles)
                {
                    worksheet.Cells[1, j].Value = titles[j - 1];
                    j++;
                }



                int i = 2; j = 1;
                foreach (object obj in list)
                {
                    worksheet.Cells[i, j].Value = obj;
                    if (j == titles.Length)
                    {
                        i++;
                        j = 1;
                    }
                    else
                    {
                        j++;
                    }
                }

                FileInfo fi = new FileInfo(filePath);
                excelPackage.SaveAs(fi);
            }
        }
    }
}
