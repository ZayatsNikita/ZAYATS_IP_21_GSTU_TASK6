using System;
using System.Collections.Generic;
using University;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniverisyModelsTests
{
    [TestClass]
    public class CommonInfoTests
    {
        private static List<CommonInfo> list = new List<CommonInfo>() {

            new CommonInfo(1,1,"Karp Alex DD","F",DateTime.Now.AddMonths(-10),1,1,3),
            new CommonInfo(2,2,"NIkita Valer Gnusov","M",DateTime.Now.AddMonths(-9),3,1,2),
            new CommonInfo(1,3,"MOyva JJu DD","M",DateTime.Now.AddMonths(-8),1,1,10),
            new CommonInfo(2,4,"Prerk Tuk Tuc","F",DateTime.Now.AddMonths(-7),2,1,8),
            new CommonInfo(3,5,"Munsd sdhf dfkj","F",DateTime.Now.AddMonths(-6),2,3,8),
            new CommonInfo(3,6,"Larisa JJJ Ddsf","M",DateTime.Now.AddMonths(-5),2,1,2),
            };

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void CommonInfoSortTests_CorrectSortParams_SortedList(int testMode)
        {
            bool actual = true;
            switch(testMode)
            {
                case 1:
                    CommonInfo.Comparator = (first, second) => { return first.Mark.CompareTo(second.Mark); };
                    break;
                case 2:
                    CommonInfo.Comparator = (first, second) => { return first.StudentFio.CompareTo(second.StudentFio); };
                    break;
                case 3:
                    CommonInfo.Comparator = (first, second) => { return first.StudentBirthday.CompareTo(second.StudentBirthday); };
                    break;
            }

            list.Sort();

            int length = list.Count;

            switch (testMode)
            {
                case 1:
                    for (int i = 1; i < length; i++)
                    {
                        if(list[i].Mark<list[i-1].Mark)
                        {
                            actual = false;
                        }
                    }
                    break;
                case 2:
                    for (int i = 1; i < length; i++)
                    {
                        if (list[i].StudentFio.CompareTo(list[i-1].StudentFio)<1)
                        {
                            actual = false;
                        }
                    }
                    break;
                case 3:
                    for (int i = 1; i < length; i++)
                    {
                        if (list[i].StudentBirthday.CompareTo(list[i - 1].StudentBirthday) <1)
                        {
                            actual = false;
                        }
                    }
                    break;

            }
            Assert.IsTrue(actual);    
        }

        [TestMethod]
        public void CommonInfoSortTests_NullIsSetAsEventHandler_ArgumentNullExceptionThrown()
        {
            bool actual = false;
            try
            {
                CommonInfo.Comparator = null;
            }
            catch (ArgumentNullException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CommonInfoSortTests_MultipleMethodsAreSetForProcessing_ArgumentExceptionThrown()
        {
            bool actual = false;

            CommonInfo.Comparator = (first, second) => { return first.Mark.CompareTo(second.Mark); };
            try
            {
                CommonInfo.Comparator+= (first, second) => { return 0; };
            }
            catch (ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }




    }
}
