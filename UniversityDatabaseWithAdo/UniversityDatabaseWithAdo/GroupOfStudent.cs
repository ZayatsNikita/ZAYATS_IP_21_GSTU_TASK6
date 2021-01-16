using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class GroupOfStudent
    {

        private int id;
        public void SetId(int idValue)
        {
            id = idValue;
        }
        public int GetId() => id;
        public GroupOfStudent(string nameOfGroup)
        {
            NameOfGroup = nameOfGroup;
        }
        public string NameOfGroup { get; set; }
    }
}
