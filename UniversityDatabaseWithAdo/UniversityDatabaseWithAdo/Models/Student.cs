using System;

namespace University
{
    /// <summary>
    /// A class that describes student.
    /// </summary>
    public class Student
    {
        private string _fio;
        private string _sex;
        /// <summary>
        /// A property that describes student's id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// A property that describes student's fio.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if null is set as NameOfExam.</exception>
        /// <exception cref="ArgumentNullException">Thrown if string length is set as NameOfExam.</exception>
        public string FIO
        {
            get => _fio;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (value.Length <6)
                {
                    throw new ArgumentException();
                }
                _fio = value;
            }
        }
        /// <summary>
        /// A property that describes student's sex.
        /// </summary>
        public string Sex
        {
            get => _sex;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                _sex = value;               
            }
        }
        /// <summary>
        /// A property that describes student's birth date.
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// A property that describes student's group id.
        /// </summary>
        public int GroupId {get;set;}
    }
}
