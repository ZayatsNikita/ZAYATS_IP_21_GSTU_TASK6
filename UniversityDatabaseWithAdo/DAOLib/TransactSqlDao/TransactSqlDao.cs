using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace DAOLib.SqlDao
{
    /// <summary>
    /// Generic class for working with TransactSQL and the T class.
    /// </summary>
    public class TransactSqlDao<T> : IDao<T> where T: new()
    {
        private static TransactSqlDao<T> instance;
        private static SqlCommand command;
        private static SqlConnection connection;
        private static SqlParameter[] parameters;
        private static SqlParameter[] newParameters;
        private static PropertyInfo[] propertys;

        
        private string createItemComandText;
        private string deleteItemComandText;
        private string readAllComandText;
        private string updateComandText;

        /// <summary>
        /// Private constructor which is used for initialization fields of class.
        /// </summary>
        /// <param name="connection">Connection to database.</param>
        /// <exception cref="ArgumentException">Thrown if T type have no propetyes.</exception>
        private TransactSqlDao(SqlConnection connection)
        {

            if(connection == null)
            {
                throw new ArgumentNullException();
            }
            command = new SqlCommand();
            command.Connection = connection;
           
            propertys = typeof(T).GetProperties();
            
            if(propertys.Length==0)
            {
                throw new ArgumentException("This class does not contain public properties");
            }


            parameters = new SqlParameter[propertys.Length];
            newParameters = new SqlParameter[propertys.Length];
            
            for (int i = 0; i < propertys.Length; i++)
            {
                parameters[i] = new SqlParameter();
                newParameters[i] = new SqlParameter();

                parameters[i].ParameterName = "@" + propertys[i].Name;
                newParameters[i].ParameterName = parameters[i].ParameterName + "New";
            }

            createItemComandText = GetComandForTransactSql.GetCreateItemComand(typeof(T).Name,propertys);
            deleteItemComandText = GetComandForTransactSql.GetDeleteItemComand(typeof(T).Name,propertys);
            updateComandText = GetComandForTransactSql.GetUpdateItemComand(typeof(T).Name,propertys);
            readAllComandText = GetComandForTransactSql.GetReadAllComand(typeof(T).Name,propertys);

            command.Parameters.AddRange(parameters);
            command.Parameters.AddRange(newParameters);
        }
        /// <summary>
        /// The Method that is used to record the value of the public properties of this object.
        /// </summary>
        /// <param name="element">Object whose property values will be recorded in the database.</param>
        /// <returns>Id of the database entry.</returns>
        /// <exception cref="ArgumentNullException">Thrown if element is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if connection to database is closed.</exception>
        /// <exception cref="SqlException">Thrown if an error occurred on the db server.</exception>
        public object Create(T element)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                if (element == null)
                {
                    throw new ArgumentNullException();
                }
                command.Parameters.Clear();
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameters[i].Value = propertys[i].GetValue(element, null);

                }
                command.CommandText = createItemComandText;
                command.Parameters.AddRange(parameters);
                object obj = command.ExecuteScalar();
                return obj;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// A method that deletes a record with values equal to the properties of this object.
        /// </summary>
        /// <param name="element">The object of the data that is to be removed from the database.</param>
        /// <exception cref="ArgumentNullException">Thrown if element is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if connection to database is closed.</exception>
        /// <exception cref="SqlException">Thrown if an error occurred on the db server.</exception>
        public void Delete(T element)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                if (element == null)
                {
                    throw new ArgumentNullException();
                }
                command.Parameters.Clear();
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameters[i].Value = propertys[i].GetValue(element, null);
                }
                command.CommandText = deleteItemComandText;
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// A method that gets data from a table with the same name as the class and create a list of elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if element is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if connection to database is closed.</exception>
        /// <exception cref="SqlException">Thrown if an error occurred on the db server.</exception>
        public List<T> ReadAll()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {

                command.Parameters.Clear();
               
                List<T> result = new List<T>();
                SqlDataReader reader = null;
                try
                {
                    command.CommandText = readAllComandText;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        T element = new T();
                        for (int i = 0; i < propertys.Length; i++)
                        {
                            propertys[i].SetValue(element,reader[i],null);
                        }
                        result.Add(element);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader?.Close();
                }
                return result;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        /// <summary>
        /// A method that replaces old data in the database with new data.
        /// </summary>
        /// <param name="oldElement">An object whose properties contain old information. </param>
        /// <param name="newElement">An object whose properties contain new information.</param>
        /// <exception cref="ArgumentNullException">Thrown if oldElement or newElement is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if connection to database is closed.</exception>
        /// <exception cref="SqlException">Thrown if an error occurred on the db server.</exception>
        public void Update(T oldElement, T newElement)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                if(oldElement== null || newElement==null)
                {
                    throw new ArgumentNullException();
                }
                command.Parameters.Clear();
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameters[i].Value = propertys[i].GetValue(oldElement, null);
                    newParameters[i].Value = propertys[i].GetValue(newElement, null);
                }
                command.Parameters.AddRange(parameters);
                command.Parameters.AddRange(newParameters);
                command.CommandText = updateComandText;
                command.ExecuteNonQuery();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static TransactSqlDao<T> GetDao(SqlConnection connection)
        {
            if (TransactSqlDao<T>.instance == null)
            {
                try
                {
                    TransactSqlDao<T>.connection = connection;
                    TransactSqlDao<T>.connection.Open();
                    TransactSqlDao<T>.instance = new TransactSqlDao<T>(connection);         
                }
                catch (Exception ex)
                {
                    TransactSqlDao<T>.instance = null;
                    throw ex;
                }
            }
            return TransactSqlDao<T>.instance;
        }

        /// <summary>
        /// Method wich is used for closing connection to database.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if connection is already closed.</exception>
        public void CloseConnect()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
