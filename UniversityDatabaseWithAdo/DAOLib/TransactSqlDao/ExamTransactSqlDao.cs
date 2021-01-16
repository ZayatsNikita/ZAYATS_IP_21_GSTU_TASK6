//using System.Collections.Generic;
//using System.Data.SqlClient;
//using University;

////Добавить проверку на наличее открытого соеденения
//namespace DAOLib.TransactSqlDaods
//{
//    public class ExamTransactSqlDao : IDao<Exam>
//    {
//        private static SqlConnection connection;
//        private static SqlCommand command;
                                         
//        private string deleteItemText = $"Delete Exam where NameOfExam = @name and TypeOfExam = @type";
//        private string findItemText = "select count(*) from Exam where NameOfExam = @gname and TypeOfExam = @gtype";
//        private string createItemText = "insert into Exam (NameOfExam, TypeOfExam) values (@gname,@gtype)";
//        private string updateComantText = "update Exam set NameOfExam=@newName, TypeOfExam = @newType where  NameOfExam = @gname and TypeOfExam = @gtype";
//        private string readAllComandText = "select NameOfExam, TypeOfExam from Exam";


//        private SqlParameter typeParam;
//        private SqlParameter nameParam;


//        private static ExamTransactSqlDao instance;
//        private ExamTransactSqlDao()
//        {;}
//        public void Create(Exam element)
//        {
//            try
//            {
                
//                command.Parameters.Clear();
//                command.CommandText = createItemText;

//                nameParam = new SqlParameter("@gname", element.NameOfExam);
//                typeParam = new SqlParameter("@gtype", element.TypeOfExam);

//                command.Parameters.Add(nameParam);
//                command.Parameters.Add(typeParam);

//                command.ExecuteNonQuery();
//            }
//            catch(SqlException ex)
//            {

//            }
//        }

//        public void Delete(Exam element)
//        {
//            command.Parameters.Clear();

//            command.CommandText = deleteItemText;
//            nameParam = new SqlParameter("@name", $"{element.NameOfExam}");
//            typeParam = new SqlParameter("@type", $"{element.TypeOfExam}");

//            command.Parameters.Add(nameParam);
//            command.Parameters.Add(typeParam);

//            int res = command.ExecuteNonQuery();
            
//        }

//        public void Update(Exam oldElement, Exam newElement)
//        {
//            command.Parameters.Clear();
//            command.CommandText = updateComantText;

//            nameParam = new SqlParameter("@gname", oldElement.NameOfExam);
//            typeParam = new SqlParameter("@gtype", oldElement.TypeOfExam);

//            command.Parameters.Add(nameParam);
//            command.Parameters.Add(typeParam);

//            command.Parameters.Add(new SqlParameter("@newName",newElement.NameOfExam));
//            command.Parameters.Add(new SqlParameter("@newType",newElement.TypeOfExam));

//            int res = command.ExecuteNonQuery();
            
//        }

//        public bool CheckTheExistenceOfTheElementInTheDataSource(Exam element)
//        {
//            command.Parameters.Clear();
//            command.CommandText = findItemText;

//            nameParam = new SqlParameter("@gname", element.NameOfExam);
//            typeParam = new SqlParameter("@gtype", element.TypeOfExam);

//            command.Parameters.Add(nameParam);
//            command.Parameters.Add(typeParam);

//            int res = (int)command.ExecuteScalar();

//            return res > 0;
//        }

//        public List<Exam> ReadAll()
//        {
//            command.Parameters.Clear();
//            command.CommandText = readAllComandText;
//            SqlDataReader reader =  command.ExecuteReader();
//            List<Exam> result = new List<Exam>();
//            while(reader.Read())
//            {
//                string nameOfExam = reader.GetString(0);
//                string typeOfExam = reader.GetString(1);
//                result.Add(new Exam(nameOfExam,typeOfExam));
//            }
//            reader.Close();

//            return result;
//        }

//        public static ExamTransactSqlDao GetExamTransactSqlDao(SqlConnection connection)
//        {
//            if(instance == null)
//            {
//                try
//                {
//                    instance = new ExamTransactSqlDao();
//                    ExamTransactSqlDao.connection = connection;
//                    ExamTransactSqlDao.connection.Open();
//                    command = new SqlCommand();
//                    command.Connection = ExamTransactSqlDao.connection;
//                }
//                catch(SqlException ex)
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
