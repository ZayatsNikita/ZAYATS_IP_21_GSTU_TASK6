using System;

namespace University
{
    /// <summary>
    /// A class which describe exam.
    /// </summary>
    public class Exam
    {
        private string _nameOfExam;
        public int Id { get; set; }
        /// <summary>
        /// Property that descrybe exam name.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if null is set as NameOfExam.</exception>
        /// <exception cref="ArgumentException">Thrown if string with zero length is set as NameOfExam.</exception>
        public string NameOfExam 
        {
            get=> _nameOfExam;
            set {
            if(value == null)
                {
                    throw new ArgumentNullException();
                }
            if(value.Length==0)
                {
                    throw new ArgumentException();
                }
                _nameOfExam = value;
            }
        }
        /// <summary>
        /// Property that descrybe type of exam.
        /// </summary>
        public string TypeOfExam { get; set; }
     
        public override string ToString()
        {
            return $"NameOfExam is {NameOfExam}, TypeOfExam = {TypeOfExam}";
        }
    }
}
