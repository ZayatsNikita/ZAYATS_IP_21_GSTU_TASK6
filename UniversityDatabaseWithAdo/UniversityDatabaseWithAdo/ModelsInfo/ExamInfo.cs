using System;

namespace University
{
    public class ExamInfo
    {
        private int _numberSession;
        public int Id { get; set; }
        public int ExamId { get; set; }

        public DateTime DatOfTheExam { get; set; }
        public int NumberSession 
        { get=> _numberSession;
            set
            {
                if(value<1)
                {
                    throw new ArgumentException();
                }
                _numberSession = value;
            }
        }
    }
}
