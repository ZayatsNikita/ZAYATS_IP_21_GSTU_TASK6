using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Student
    {

        private int id;

        public void SetId(int idValue)
        {
            id = idValue;
        }
        public int GetId() => id;
        public char Sex { get; set; }
        public string Fio { get; set; }
        public DateTime BirthDay { get; set; }

        public int GroupId {get;set;}
    }
}
