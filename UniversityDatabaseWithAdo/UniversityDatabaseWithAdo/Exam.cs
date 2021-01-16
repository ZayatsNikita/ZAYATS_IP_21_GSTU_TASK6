using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Exam
    {
        private int id;

        public void SetId(int idValue)
        {
            id = idValue;
        }
        public int GetId()=>id;
        public Exam(string nameOfExam, string typeOfExam)
        {
            NameOfExam = nameOfExam;
            TypeOfExam = typeOfExam;
        }
        public string NameOfExam { get; set; }
        public string TypeOfExam { get; set; }

        public override string ToString()
        {
            return $"NameOfExam is {NameOfExam}, TypeOfExam = {TypeOfExam}";
        }
    }
}
