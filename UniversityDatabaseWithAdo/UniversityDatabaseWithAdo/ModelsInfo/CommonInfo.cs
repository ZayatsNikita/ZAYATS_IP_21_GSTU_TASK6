using System;
namespace University
{
    public class CommonInfo : IComparable<CommonInfo>
    {
        public CommonInfo()
        { }
        public CommonInfo(int groupId, int studentId,string studentFio, int examID, int numberOfSession, int mark)
        {
            GroupId = groupId;
            StudentFio = studentFio;
            StudentId = studentId;
            ExamId = examID;
            NumberSession = numberOfSession;
            Mark = mark;
        }
        public int GroupId { get; set; }
        public string StudentFio { get; set; }
        public int ExamId { get; set; }
        public int NumberSession { get; set; }
        public int Mark { get; set; }
        
        public int StudentId { get; set; }

        private static Func<CommonInfo, CommonInfo, int> _comparator = (x, y) => x.Mark.CompareTo(y.Mark);
        public static Func<CommonInfo, CommonInfo, int> Comparator { 
            get=>_comparator;
            set
            {
                if(value!= null)
                {
                    _comparator = value;
                }
                throw new ArgumentException();
            }
        } 
        public int CompareTo(CommonInfo other)
        {
            return Comparator(this,other);
        }
    }
}
