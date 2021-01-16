using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DAOLib.SqlDao
{
    class TransactSqlDao<T> : IDao<T> where T: new()
    {
        private static TransactSqlDao<T> instance;
        private static SqlCommand command;
        private static SqlConnection connection;
        private static SqlParameter[] parameters;
        private static SqlParameter[] newParameters;
        private static PropertyInfo[] propertys;


        private string[] columnsName;
        
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

            columnsName = new string[propertys.Length];
          
            StringBuilder builderForName = new StringBuilder();
            StringBuilder builderForParams = new StringBuilder();

            for (int i = 0; i < propertys.Length; i++)
            {
                columnsName[i] = propertys[i].Name;
                
                parameters[i] = new SqlParameter();
                newParameters[i] = new SqlParameter();

                parameters[i].ParameterName = "@" + columnsName[i];
                newParameters[i].ParameterName = parameters[i].ParameterName + "New";
            }

            builderForName.Append(columnsName[0]);
            builderForParams.Append("@" + columnsName[0]);
            for (int i = 1; i < columnsName.Length; i++)
            {
                builderForName.Append(", " + columnsName[i]);
                builderForParams.Append(", @" + columnsName[0]);
            }

            readAllComandText = $"select {builderForName} from {typeof(T).Name}";
            createItemComandText = $"insert into {typeof(T).Name} ({builderForName}) values ({builderForParams}) select SCOPE_IDENTITY()";
            
            builderForName.Clear();
            builderForParams.Clear();

            builderForName.Append($"{columnsName[0]} = {parameters[0].ParameterName}");
            builderForParams.Append($"{columnsName[0]} = {parameters[0].ParameterName+"New"}");
            for (int i = 1; i < columnsName.Length; i++)
            {
                builderForName.Append($" and {columnsName[i]} = {parameters[i].ParameterName}");
                builderForParams.Append($"{columnsName[i]} = {newParameters[i].ParameterName}");

            }

            deleteItemComandText = $"Delete {typeof(T).Name} where {builderForName}";
            updateComandText = $"update {typeof(T).Name} set {builderForParams} where {builderForName}";

            command.Parameters.AddRange(parameters);
            command.Parameters.AddRange(newParameters);
        }

        public object Create(T element)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = propertys[i].GetValue(element,null);
                
            }
            command.CommandText = createItemComandText;
            return command.ExecuteScalar();
        }

        public void Delete(T element)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = propertys[i].GetValue(element, null);
            }
            command.CommandText = deleteItemComandText;
            command.ExecuteNonQuery();
        }

        public List<T> ReadAll()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
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
                    if (reader != null)
                    {
                        reader.Close();
                    }
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
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = propertys[i].GetValue(oldElement, null);
                newParameters[i].Value = propertys[i].GetValue(newElement, null);
            }
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
