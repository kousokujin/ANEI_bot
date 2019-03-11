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
            List<string> quest_list = new List<string>() { "ˆÃ‰e‰QŠª‚­‰ó‚ê‚½¢ŠE", "‹«ŠE‚ğŠÑ‚­‘oŠp‚Ì‹¥’¹", "¬“×“±‚­ˆÅ‚Ì‰»g", "¬“×Y‚İo‚·ˆÅ‚Ì‰»g", "¬“×Š«‚Ño‚·—´‚Ì™ôšK" };
            DateTime d = new DateTime(2019, 3, 22);
            DateTime epc = new DateTime(2019, 3, 28);

            for (int i = 0; i < 30; i++)
            {
                string quest = RecommendQuestCalculator.recommandQuest(d);
                if(d >= epc)
                {
                    string expect = "ˆÃ‰e‰QŠª‚­‰ó‚ê‚½¢ŠE";
                    Assert.AreEqual(expect, quest);
                }
                else
                {
                    string expect = quest_list[i % 5];
                    Assert.AreEqual(expect, quest);
                }

                d += new TimeSpan(1, 0, 0, 0);
            }
        }

        [TestMethod]
        public void NextDay_test()
        {
            string quest = "ˆÃ‰e‰QŠª‚­‰ó‚ê‚½¢ŠE";
            DateTime d = new DateTime(2019, 3, 22);
            DateTime NextAnei = new DateTime(2019, 3, 22);
            DateTime epc = new DateTime(2019, 3, 28);

            for (int i = 0; i < 30; i++)
            {
                (int day, DateTime time) = RecommendQuestCalculator.nextQuest(quest, d);
                if (d >= epc)
                {

                    int expect_day = 0;
                    Assert.AreEqual(expect_day, day);
                    Assert.AreEqual(d, time);

                }
                else
                {
                    int expect_day = (i % 5);
                    if(expect_day != 0)
                    {
                        expect_day = 5 - expect_day;
                    }
                    Assert.AreEqual(expect_day, day);
                    Assert.AreEqual(NextAnei, time);

                    if (i % 5 == 0)
                    {
                        NextAnei += new TimeSpan(5, 0, 0, 0);
                    }
                }

                d += new TimeSpan(1, 0, 0, 0);
            }
        }

        [TestMethod]
        public void NextNextDay_test()
        {
            string quest = "ˆÃ‰e‰QŠª‚­‰ó‚ê‚½¢ŠE";
            DateTime d = new DateTime(2019, 3, 22);
            DateTime NextAnei = new DateTime(2019, 3, 22);
            DateTime epc = new DateTime(2019, 3, 28);

            for (int i = 0; i < 30; i++)
            {
                (int day, DateTime time) = RecommendQuestCalculator.nextnextQuest(quest, d);
                if (d >= epc)
                {

                    int expect_day = 1;
                    DateTime expect_time = d + new TimeSpan(1, 0, 0, 0);
                    Assert.AreEqual(expect_day, day);
                    Assert.AreEqual(expect_time,time);

                }
                else
                {
                    int expect_day = 5 - (i % 5);
                    if(i % 5 == 0)
                    {
                        DateTime tmp = NextAnei + new TimeSpan(5, 0, 0, 0);
                        if (tmp < epc)
                        {
                            NextAnei += new TimeSpan(5, 0, 0, 0);
                        }
                        else
                        {
                            NextAnei = epc;
                        }
                    }
                    Assert.AreEqual(expect_day, day);
                    Assert.AreEqual(NextAnei, time);
                }

                d += new TimeSpan(1, 0, 0, 0);
            }
        }
    }
}
