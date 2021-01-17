using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DAOLib.SqlDao;
using DAOLib;
using University;
using InteractionOfTheDatabaseAndTheUniversity;

namespace BussinesLayerTests
{
    [TestClass]
    public class InteractionOfComomnInfoAndDatabseBaseTests
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=InteractionOfComomnInfoAndDatabseBaseTest; Integrated Security=True";
        [TestMethod]
        public void GetCommonInfoFromDatabase_DataInDataBase_ListWithCommonInfo()
        {

            InteractionOfComomnInfoAndDatabseBase interaction = new InteractionOfComomnInfoAndDatabseBase(connectionString);
            List<CommonInfo> list = interaction.GetCommonInfoFromDatabase();
            bool actual = list.Count == 8 && list[0].Mark == 10 && list[0].StudentFio == "Zayats Nikita Sergeevich" && list[0].ExamId == 1 && list[0].StudentId == 1 && list[0].NumberSession == 1 
                && list[1].Mark == 9 && list[1].StudentFio == "Pishuck Alex Igoreevna" && list[1].ExamId == 1 && list[1].StudentId == 2 && list[1].NumberSession == 1
                && list[2].Mark == 9 && list[2].StudentFio == "Zayats Nikita Sergeevich" && list[2].ExamId == 1 && list[2].StudentId == 1 && list[2].NumberSession == 2
                && list[3].Mark == 8 && list[3].StudentFio == "Pishuck Alex Igoreevna" && list[3].ExamId == 1 && list[3].StudentId == 2 && list[3].NumberSession == 2
                && list[4].Mark == 8 && list[4].StudentFio == "Zayats Nikita Sergeevich" && list[4].ExamId == 1 && list[4].StudentId == 1 && list[4].NumberSession == 3
                && list[5].Mark == 8 && list[5].StudentFio == "Pishuck Alex Igoreevna" && list[5].ExamId == 1 && list[5].StudentId == 2 && list[5].NumberSession == 3
                && list[6].Mark == 7 && list[6].StudentFio == "Tsmyg Dmitry Alexandrovich" && list[6].ExamId == 2 && list[6].StudentId == 3 && list[6].NumberSession == 4
                && list[7].Mark == 3 && list[7].StudentFio == "Tsmyg Dmitry Alexandrovich" && list[7].ExamId == 2 && list[7].StudentId == 3 && list[7].NumberSession == 5;
            Assert.IsTrue(actual);

        }
    }
}
