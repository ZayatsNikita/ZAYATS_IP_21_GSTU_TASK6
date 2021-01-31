using System;

namespace University
{
    /// <summary>
    /// A class that describes group of student
    /// </summary>
    public class GroupOfStudent
    {
        private string _nameOfGroup;
        /// <summary>
        /// A property that describes id of group.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// A property that describes name of group.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if null is set as NameOfGroup.</exception>
        /// <exception cref="ArgumentException">Thrown if string with zero length is set as NameOfGroup.</exception>
        public string NameOfGroup
        {
            get => _nameOfGroup;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
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
