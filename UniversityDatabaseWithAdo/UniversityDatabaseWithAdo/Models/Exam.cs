using System;

namespace University
{
    public class Exam
    {
        private string _nameOfExam;
        private string _typeOfExam;
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
        public string TypeOfExam {
            get=> _typeOfExam;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                if (value == "Test" || value == "Exam")
                {
                    _typeOfExam = value;
                }
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            return $"NameOfExam is {NameOfExam}, TypeOfExam = {TypeOfExam}";
        }
    }
}
