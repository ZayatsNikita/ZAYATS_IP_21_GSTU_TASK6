using System;
namespace University
{
    /// <summary>
    /// A class that describes report about sessions result
    /// </summary>
    public class ReportingReport : IComparable<ReportingReport>
    {
        /// <summary>
        /// Constructor of a class that creates an object without objects initializing.
        /// </summary>
        public ReportingReport()
        { }
        /// <summary>
        /// Constructor of a class that creates an object with initializing of object.
        /// </summary>
        /// <param name="groupId">id of the group in which the student is studying.</param>
        /// <param name="studentId">Student's id.</param>
        /// <param name="studentFio">Student's fio.</param>
        /// <param name="studentSex">Student's sex.</param>
        /// <param name="studentBirthday">Student's date of birth.</param>
        /// <param name="examID">Exam id.</param>
        /// <param name="numberOfSession">Number of session.</param>
        /// <param name="mark">The mark that the student received on the exam.</param>
        public ReportingReport(int groupId, int studentId,string studentFio,string studentSex, DateTime studentBirthday, int examID, int numberOfSession, int mark)
        {
            StudentSex = studentSex;
            StudentBirthday = studentBirthday;
            StudentFio = studentFio;
            GroupId = groupId;
            StudentId = studentId;
            ExamId = examID;
            NumberSession = numberOfSession;
            Mark = mark;
        }
        /// <summary>
        /// A property that describes student's fio.
        /// </summary>
        public string StudentFio { get; set; }
        /// <summary>
        /// A property that describes student's date of birth.
        /// </summary>
        public DateTime StudentBirthday { get; set; }
        /// <summary>
        /// A property that describes student's sex.
        /// </summary>
        public string StudentSex { get; set; }

        /// <summary>
        /// A property that describes student's id.
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// A property that describes student's group id.
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// A property that describes student's exam id.
        /// </summary>
        public int ExamId { get; set; }
        /// <summary>
        /// A property that describes student's number of session.
        /// </summary>
        public int NumberSession { get; set; }
        /// <summary>
        /// A property that describes student's mark in exam.
        /// </summary>
        public int Mark { get; set; }

        private static Func<ReportingReport, ReportingReport, int> _comparator = (x, y) => 0;

        /// <summary>
        /// A property that describes The operation of comparisons used when sorting the report.
        /// </summary>
        public static Func<ReportingReport, ReportingReport, int> Comparator {
            get=>_comparator;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (value.GetInvocationList().Length!=1)
                {
                    throw new ArgumentException();
                }
                _comparator = value;
            }
        }

        /// <summary>
        /// Method for comparing two instances of the CommonInfo class.
        /// </summary>
        /// <param name="other">The object to compare the caller to.</param>
        /// <returns>An integer describing the result of the comparison. The value of the number depends on the Comparator property.</returns>
        public int CompareTo(ReportingReport other)
        {
            return Comparator(this,other);
        }
    }
}
