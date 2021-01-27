using System;
using System.Data.SqlClient;

namespace DAOLib.SqlDao
{
    /// <summary>
    /// A generyc class that is used to create a TransactSQL<T> object in an abstract factory.
    /// </summary>
    internal class TransactSqlDaoFactory<T> : IDaoFactory<T> where T: new()
    {
        /// <summary>
        /// Method that returns a TransactSqlDao object for type T.
        /// </summary>
        /// <param name="connectionDatabaseString">A string with the parameters for connecting to the database.
        /// <example><para></para>Example of connections string:<para></para>
        /// @"Data Source=.\SQLEXPRESS; Initial Catalog=UniversityDatabase; Integrated Security=True";
        /// </example>
        /// </param>
        /// <returns>TransactSqlDao for T type.</returns>
        /// <exception cref="ArgumentNullException">Thrwon if connectionDatabaseString equals to null.</exception>
        /// <exception cref="ArgumentException">Thrwon if connectionDatabaseString length equals to zero or if T type have not any public propertyes.</exception>
        public IDao<T> CreateDao(string connectionDatabaseString)
        {
            if(connectionDatabaseString==null)
            {
                throw new ArgumentNullException();
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
