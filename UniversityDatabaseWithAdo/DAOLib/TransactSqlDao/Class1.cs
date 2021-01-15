using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University;

namespace DAOLib.TransactSqlDao
{
    class ExamTransactSqlDao : IDao<Exam>
    {
        private static ExamTransactSqlDao instance;
        protected ExamTransactSqlDao()
        {;}
        public void Create(Exam element)
        {
            throw new NotImplementedException();
        }

        public void Delete(Exam element)
        {
           
            throw new NotImplementedException();
        }

        public Exam Read(Exam element)
        {
            throw new NotImplementedException();
        }

        public List<Exam> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Exam element)
        {
            throw new NotImplementedException();
        }

        public static ExamTransactSqlDao GetExamTransactSqlDao()
        {
            if(instance == null)
            {
                instance = new ExamTransactSqlDao();
            }
            return instance;
        }
    }
}
