using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAOLib.SqlDao;
using System.Data.SqlClient;

namespace DAOLib.SqlDao
{
    class TransactSqlDaoFactory<T> : IDaoFactory<T> where T: new()
    {
        public IDao<T> CreateDao(string paramsOfCreating)
        {
            SqlConnection sqlConnection = new SqlConnection(paramsOfCreating);
            TransactSqlDao<T> transactSqlDao = TransactSqlDao<T>.GetDao(sqlConnection);
            return transactSqlDao;
        }
    }
}
