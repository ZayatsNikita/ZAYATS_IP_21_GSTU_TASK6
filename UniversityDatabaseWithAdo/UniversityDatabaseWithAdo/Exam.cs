using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Exam
    {
        public int Id { get; set; }
        public string NameOfExam { get; set; }
        public string TypeOfExam { get; set; }

        public override string ToString()
        {
            return $"NameOfExam is {NameOfExam}, TypeOfExam = {TypeOfExam}";
        }
    }
}
