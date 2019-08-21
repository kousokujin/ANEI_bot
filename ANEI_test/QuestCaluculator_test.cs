using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ANEI_bot;

namespace ANEI_test
{
    [TestClass]
    public class QuestCaluculator_test
    {
        [TestMethod]
        public void RecomQuest_test()
        {
            //List<string> quest_list = new List<string>() { "�Ée�Q������ꂽ���E", "���E���т��o�p�̋���", "���ד����ł̉��g", "���׎Y�ݏo���ł̉��g", "���׊��яo�����̙��K" };
            List<string> quest_list = new List<string>(){ "�Ée�Q������ꂽ���E" ,"�����������􂭍���", "���ד����ł̉��g", "��o����������̋���", "�Î�ɐ��܂ꂵ����"};
            DateTime d = new DateTime(2019, 8, 21);
            DateTime epc = new DateTime(2019, 8, 20);

            for (int i = 0; i < 30; i++)
            {
                string quest = RecommendQuestCalculator.recommandQuest(d);
                int expect_day = (d - epc).Days % 5;
                Assert.AreEqual(quest_list[expect_day], quest);

                d += new TimeSpan(1, 0, 0, 0);
            }
        }

        [TestMethod]
        public void NextDay_test()
        {
            string quest = "�Ée�Q������ꂽ���E";
            DateTime d = new DateTime(2019, 8, 21);
            //DateTime NextAnei = new DateTime(2019, 8, 25);
            DateTime epc = new DateTime(2019, 8, 20);

            for (int i = 0; i < 30; i++)
            {
                (int day, DateTime time) = RecommendQuestCalculator.nextQuest(quest, d);

                int expect_day = (d - epc).Days % 5;
                if(expect_day != 0)
                {
                    expect_day = 5 - expect_day;
                }

                DateTime expect_time = d + new TimeSpan(expect_day, 0, 0, 0);
                Assert.AreEqual(expect_day, day);
                Assert.AreEqual(expect_time, time);


                d += new TimeSpan(1, 0, 0, 0);
            }
        }

        [TestMethod]
        public void NextNextDay_test()
        {
            string quest = "�Ée�Q������ꂽ���E";
            DateTime d = new DateTime(2019, 8, 21);
            DateTime NextAnei = new DateTime(2019, 8, 25);
            DateTime epc = new DateTime(2019, 8, 20);

            for (int i = 0; i < 30; i++)
            {
                (int day, DateTime time) = RecommendQuestCalculator.nextnextQuest(quest, d);
                int expect_day = 5 - (d - epc).Days % 5;
                DateTime expect_time = d + new TimeSpan(expect_day, 0, 0, 0);

                Assert.AreEqual(expect_day, day);
                Assert.AreEqual(expect_time, time);

                d += new TimeSpan(1, 0, 0, 0);
            }
        }
    }
}
