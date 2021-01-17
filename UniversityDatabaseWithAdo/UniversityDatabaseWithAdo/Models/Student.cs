using System;

namespace University
{
    public class Student
    {
        private string _fio;
        private string _sex;
        public int Id { get; set; }
        public string FIO
        {
            get => _fio;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                if (value.Length <6)
                {
                    throw new ArgumentException();
                }
                _fio = value;
            }
        }
        public string Sex
        {
            get => _sex;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                if (value == "M" || value == "F")
                {
                    _sex = value;
                }
                throw new ArgumentException();
            }
        }
        public DateTime BirthDay { get; set; }
        public int GroupId {get;set;}
    }
}
