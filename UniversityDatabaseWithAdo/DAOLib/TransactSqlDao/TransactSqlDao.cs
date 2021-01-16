using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace DAOLib.SqlDao
{
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


        private TransactSqlDao(SqlConnection connection)
        {

            command = new SqlCommand();
            command.Connection = connection;
           
            propertys = typeof(T).GetProperties();
            
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

        public object Create(T element)
        {
            command.Parameters.Clear();
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = propertys[i].GetValue(element,null);
                
            }
            command.CommandText = createItemComandText;
            command.Parameters.AddRange(parameters);
            object obj =  command.ExecuteScalar();
            return obj;
        }

        public void Delete(T element)
        {
            command.Parameters.Clear();
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = propertys[i].GetValue(element, null);
            }
            command.CommandText = deleteItemComandText;
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
        }

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

        public void Update(T oldElement, T newElement)
        {
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
                catch (SqlException ex)
                {
                    TransactSqlDao<T>.instance = null;
                    throw ex;
                }
            }
            return TransactSqlDao<T>.instance;
        }

        public void CloseConnect()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
