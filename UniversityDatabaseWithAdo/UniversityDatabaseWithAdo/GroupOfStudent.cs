using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class GroupOfStudent
    {
        GroupOfStudent(string nameOfGroup)
        {
            NameOfGroup = nameOfGroup;
        }
        public string NameOfGroup { get; set; }
    }
}
