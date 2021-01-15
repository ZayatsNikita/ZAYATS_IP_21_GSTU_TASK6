using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Exam
    {
        public Exam(string nameOfExam, string typeOfExam)
        {
            NameOfExam = nameOfExam;
            TypeOfExam = typeOfExam;
        }
        public string NameOfExam { get; set; }
        public string TypeOfExam { get; set; }
    }
}
