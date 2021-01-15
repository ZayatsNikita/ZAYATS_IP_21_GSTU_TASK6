using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Student
    {

        public char Sex { get; set; }
        public string Fio { get; set; }
        public DateTime BirthDay { get; set; }

        public GroupOfStudent Group {get;set;}
    }
}
