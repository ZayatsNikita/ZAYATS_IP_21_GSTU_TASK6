using System;
using System.Collections.Generic;
using System.Linq;

namespace University
{
    public static class StudentProcessing
    {
        public static Dictionary<int, List<Student>> ExtractStudentsForExpulsion(List<CommonInfo> commonInfo)
        {
            if(commonInfo.Count==0)
            {
                throw new ArgumentException();
            }
            if(commonInfo==null)
            {
                throw new NullReferenceException();
            }
            List<Student> students = new List<Student>();
            var badStudent = commonInfo.Where(y => y.Mark < 4).Select(z=> new {z.GroupId, z.StudentFio, z.StudentId});
            if(badStudent.Count()==0)
            {
                throw new ArgumentException();
            }
            foreach(var info in badStudent)
            {
                students.Add(new Student() {Id =info.StudentId, GroupId = info.GroupId, FIO = info.StudentFio});
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
