//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SqlClient;
//using University;

//namespace DAOLib.TransactSqlDaodd
//{
//    class GroupOfStudentTransactSqlDao : IDao<GroupOfStudent>
//    {
//        private static SqlConnection connection;
//        private static SqlCommand command;

//        private string deleteItemText = $"Delete GroupOfStudent where NameOfGroup = @name";
//        private string findItemText = "select count(*) from GroupOfStudent where NameOfGroup = @name";
//        private string createItemText = "insert into GroupOfStudent (NameOfGroup) values (@name)";
//        private string updateComantText = "update GroupOfStudent set NameOfGroup=@newName where  NameOfGroup = @name";
//        private string readAllComandText = "select NameOfGroup from GroupOfStudentTransactSqlDao";


//        private static SqlParameter nameParam;
//        private static SqlParameter newNameParam;

//        private static GroupOfStudentTransactSqlDao instance;

//        private GroupOfStudentTransactSqlDao()
//        {
//            newNameParam = new SqlParameter();

//            nameParam.ParameterName = "@name";
            
//            newNameParam.ParameterName = "@newName";

//            nameParam = new SqlParameter();

//            command.Connection = connection;
//        }


//        public bool CheckTheExistenceOfTheElementInTheDataSource(GroupOfStudent element)
//        {
//            command.CommandText = findItemText;

//            nameParam.Value = element.NameOfGroup;

//            int res = (int)command.ExecuteNonQuery();

//            return res > 0;
//        }

//        public void Create(GroupOfStudent element)
//        {
//            try
//            {
//                command.CommandText = createItemText;
//                nameParam.Value = element.NameOfGroup;
//                command.ExecuteNonQuery();
//            }
//            catch(SqlException ex)
//            {

//            }
//        }

//        public void Update(GroupOfStudent oldElement, GroupOfStudent newElement)
//        {
//            if(connection.State == System.Data.ConnectionState.Open)
//            {
//                command.CommandText = updateComantText;

//                newNameParam.Value = newElement.NameOfGroup;

//                nameParam.Value = oldElement.NameOfGroup;

//                command.ExecuteNonQuery();
//            }
//            else
//            {
//                throw new InvalidOperationException();
//            }
//        }

//        public void Delete(GroupOfStudent element)
//        {
//            if (connection.State == System.Data.ConnectionState.Open)
//            {
//                command.CommandText = deleteItemText;

//                nameParam.Value = element.NameOfGroup;

//                command.ExecuteNonQuery();
//            }
//            else
//            {
//                throw new InvalidOperationException();
//            }
//        }

//        public List<GroupOfStudent> ReadAll()
//        {
//            if (connection.State == System.Data.ConnectionState.Open)
//            {
//                List<GroupOfStudent> result = new List<GroupOfStudent>();
//                SqlDataReader reader=null;
//                try
//                {
//                    command.CommandText = readAllComandText;
//                    reader = command.ExecuteReader();

                    
//                    while (reader.Read())
//                    {
//                        string nameOfGroup = reader.GetString(0);
                      
//                        result.Add(new GroupOfStudent(nameOfGroup));
//                    }
//                }
//                catch(Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    if (reader != null)
//                    {
//                        reader.Close();
//                    }
//                }
//                return result;

//            }
//            else
//            {
//                throw new InvalidOperationException();
//            }
//        }


//        public static GroupOfStudentTransactSqlDao GetDao(SqlConnection connection)
//        {
//            if (instance == null)
//            {
//                try
//                {
//                    GroupOfStudentTransactSqlDao.connection = connection;
//                    GroupOfStudentTransactSqlDao.connection.Open();
//                    command = new SqlCommand();
//                    command.Connection = GroupOfStudentTransactSqlDao.connection;
//                    instance = new GroupOfStudentTransactSqlDao();
//                }
//                catch (SqlException ex)
//                {
//                    instance = null;
//                    throw ex;
//                }
//            }
//            return instance;
//        }


//        public void CloseConnect()
//        {
//            if (connection.State == System.Data.ConnectionState.Open)
//            {
//                connection.Close();
//            }
//        }

        
//    }
//}
