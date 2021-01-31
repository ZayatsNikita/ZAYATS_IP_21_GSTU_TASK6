using System;
using System.Collections.Generic;
using System.Linq;

namespace University
{
    /// <summary>
    /// A statiic class that A static class that is used to handle students.
    /// </summary>
    public static class StudentProcessing
    {
        /// <summary>
        /// A method that extracts the students to be expelled from the session results report and combines them into groups.
        /// </summary>
        /// <param name="commonInfo">List of result of the session</param>
        /// <returns>A dictionary in which the group code corresponds to a list of students from this group who should be expelled.</returns>
        /// <exception cref="ArgumentException">Throw if commonInfo param has no alements.</exception>
        /// <exception cref="ArgumentNullException">Throw if commonInfo is null or there are elements with null value.</exception>
        public static Dictionary<int, List<Student>> ExtractStudentsForExpulsion(List<ReportingReport> commonInfo)
        {
            if (commonInfo == null)
            {
                throw new ArgumentNullException();
            }
            if (commonInfo.Count==0)
            {
                throw new ArgumentException();
            }
            List<Student> students = new List<Student>();

            IEnumerable<Student> badStudent = commonInfo.Where(y => y.Mark < 4)
                .Select(z=> new { z.StudentSex, z.StudentFio, z.StudentBirthday, z.StudentId,z.GroupId}).Distinct()
                .Select(z=>new Student() {Sex = z.StudentSex, FIO = z.StudentFio, Id = z.StudentId, GroupId =z.GroupId,BirthDay =z.StudentBirthday });
            
            if(badStudent.Count()==0)
            {
                throw new ArgumentException();
            }
            foreach(var student in badStudent)
            {
                students.Add(student);
            }
            var result = students.GroupBy(x=>x.GroupId).Select(g => new { GroupId = g.Key, StudentsForDeleting = g.ToList() });

            Dictionary<int, List<Student>> report= new Dictionary<int, List<Student>>();
            foreach(var data in result)
            {
                report.Add(data.GroupId,data.StudentsForDeleting);
            }
        
            return report;
        }
        
    }

   


}
