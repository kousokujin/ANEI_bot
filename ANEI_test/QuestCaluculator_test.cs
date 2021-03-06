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
            //List<string> quest_list = new List<string>() { "暗影渦巻く壊れた世界", "境界を貫く双角の凶鳥", "混沌導く闇の化身", "混沌産み出す闇の化身", "混沌喚び出す龍の咆哮" };
            List<string> quest_list = new List<string>(){ "暗影渦巻く壊れた世界" ,"平穏を引き裂く混沌", "混沌導く闇の化身", "戦塵を招く魔城の脅威", "静寂に生まれし混沌"};
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
            string quest = "暗影渦巻く壊れた世界";
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
            string quest = "暗影渦巻く壊れた世界";
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
