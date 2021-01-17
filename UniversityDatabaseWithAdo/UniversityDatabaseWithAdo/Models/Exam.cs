using System;

namespace University
{
    public class Exam
    {
        private string _nameOfExam;
        public int Id { get; set; }
        public string NameOfExam 
        {
            get=> _nameOfExam;
            set {
            if(value == null)
                {
                    throw new NullReferenceException();
                }
            if(value.Length==0)
                {
                    throw new ArgumentException();
                }
                _nameOfExam = value;
            }
        }
        public string TypeOfExam { get; set; }
        

        public override string ToString()
        {
            return $"NameOfExam is {NameOfExam}, TypeOfExam = {TypeOfExam}";
        }
    }
}
