using System;

namespace University
{
    public class GroupOfStudent
    {
        private string _nameOfGroup;
        public int Id { get; set; }
        public string NameOfGroup
        {
            get => _nameOfGroup;
            set
            {

                if (value == null)
                {
                    throw new NullReferenceException();
                }
                if (value.Length == 0)
                {
                    throw new ArgumentException();
                }
                _nameOfGroup = value;
            }
        }
    }
}
