using System;
using System.Data.SqlClient;

namespace DAOLib.SqlDao
{
    internal class TransactSqlDaoFactory<T> : IDaoFactory<T> where T: new()
    {
        public IDao<T> CreateDao(string connectionDatabaseString)
        {
            if(connectionDatabaseString==null)
            {
                throw new NullReferenceException();
            }
            if (connectionDatabaseString.Length==0)
            {
                throw new ArgumentException();
            }
            SqlConnection sqlConnection = new SqlConnection(connectionDatabaseString);
            TransactSqlDao<T> transactSqlDao = TransactSqlDao<T>.GetDao(sqlConnection);
            return transactSqlDao;
        }
    }
}
