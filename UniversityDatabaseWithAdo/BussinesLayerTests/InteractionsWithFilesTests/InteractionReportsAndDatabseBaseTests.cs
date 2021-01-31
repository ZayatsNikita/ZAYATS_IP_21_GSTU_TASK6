using DaoLibTests;
using InteractionOfTheDatabaseAndTheUniversity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using University;

namespace BussinesLayerTests
{
    [TestClass]
    public class InteractionReportsAndDatabseBaseTests
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////                                           !!!ATTENTION -- 1!!!                                               //////
        ///////To work correctly, you need to have a database created and populated with scripts from the same repository    //////
        ///////                     The scripts are located in "the scripts for database deployment" folder                  //////
        ///////                                           !!!ATTENTION -- 2!!!                                               //////                       
        ///////If necessary change the database "connectionString" ConnectionString.connectionString                         //////
        ///////ConnectionString.connectionString used in classes: TransatSqlDaoTest, TransatSqlDaoFactoryTests,              //////
        ///////                                          InteractionOfComomnInfoAndDatabseBaseTests.                         //////
        ///////                                                                                                              //////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string connectionString = ConnectionString.connectionString;
        [TestMethod]
        public void GetCommonInfoFromDatabase_DataInDataBase_ListWithCommonInfo()
        {
            InteractionReportsAndDatabseBase.OpenConnections("TransatSqlDao", connectionString);
            List<ReportingReport> list = InteractionReportsAndDatabseBase.GetCommonInfoFromDatabase();
            bool actual = list.Count(x=>x.Mark == 10 && x.StudentFio == "Zayats Nikita Sergeevich" && x.ExamId == 1 && x.StudentId == 1 && x.NumberSession == 1)==1 
                && list.Count(x=>x.Mark == 9 && x.StudentFio == "Pishuck Alex Igoreevna" && x.ExamId == 1 && x.StudentId == 2 && x.NumberSession == 1)==1
                && list.Count(y=>y.Mark == 9 && y.StudentFio == "Zayats Nikita Sergeevich" && y.ExamId == 1 && y.StudentId == 1 && y.NumberSession == 2)==1
                && list.Count(y=>y.Mark == 8 && y.StudentFio == "Pishuck Alex Igoreevna" && y.ExamId == 1 && y.StudentId == 2 && y.NumberSession == 2)==1
                && list.Count(z=>z.Mark == 8 && z.StudentFio == "Zayats Nikita Sergeevich" && z.ExamId == 1 && z.StudentId == 1 && z.NumberSession == 3)==1
                && list.Count(z=>z.Mark == 8 && z.StudentFio == "Pishuck Alex Igoreevna" && z.ExamId == 1 && z.StudentId == 2 && z.NumberSession == 3)==1
                && list.Count(i=>i.Mark == 7 && i.StudentFio == "Tsmyg Dmitry Alexandrovich" && i.ExamId == 2 && i.StudentId == 3 && i.NumberSession == 4)==1
                && list.Count(i=>i.Mark == 3 && i.StudentFio == "Tsmyg Dmitry Alexandrovich" && i.ExamId == 2 && i.StudentId == 3 && i.NumberSession == 5)==1;
            Assert.IsTrue(actual);
        }
    }
}
