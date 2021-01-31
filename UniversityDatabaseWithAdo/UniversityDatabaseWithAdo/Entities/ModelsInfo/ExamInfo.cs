using System;

namespace University
{
    /// <summary>
    /// A class that describes information.
    /// </summary>
    public class ExamInfo
    {
        private int _numberSession;
        /// <summary>
        /// A property that describes exam info id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// A property that describes exam id.
        /// </summary>
        public int ExamId { get; set; }
        /// <summary>
        /// A property that describes date of the exam.
        /// </summary>
        public DateTime DatOfTheExam { get; set; }
        /// <summary>
        /// A property that describes NumberSession
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if set the value to less than 1</exception>
        public int NumberSession 
        { 
            get=> _numberSession;
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
