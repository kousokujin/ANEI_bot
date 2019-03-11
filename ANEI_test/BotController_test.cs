using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ANEI_bot;
using System.IO;

namespace ANEI_test
{
    [TestClass]
    public class BotController_test
    {
        BotController bot;
        List<string> anei_words;
        public BotController_test()
        {
            AbstractClient Client = new TestClient();
            bot = new BotController(Client);

            anei_words = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                anei_words.Add(getAneiWords(i));
            }
        }

        [TestMethod]
        public void anei_word_test()
        {
            DateTime d = new DateTime(2019, 3, 22);

            for (int i = 0; i < 30; i++)
            {
                bot.postRecommendQuest(d);
                d += new TimeSpan(1, 0, 0, 0);
            }
            
        }

        string postQuest(DateTime time)
        {
            string quest_name = RecommendQuestCalculator.recommandQuest(time);
            (int day, DateTime nextday) = RecommendQuestCalculator.nextQuest("暗影渦巻く壊れた世界", time);
            string postStr = string.Format("今日のおすすめクエストは{0}です。次の暗影は{1}です。",
                quest_name,
                nextday.ToString("MM月dd日"));
            return postStr;
        }

        string getAneiWords(int index)
        {
            string post = "";
            switch (index)
            {
                case 0:
                    post += "うおおおお今日は暗影だ！暗影暗影暗影暗影！";
                    break;
                case 1:
                    post += "さぁ！ひれ伏したまえ。暗影の時間だ！";
                    break;
                case 2:
                    post += "応えよ暗影！　我が経験値に！";
                    break;
                case 3:
                    post += "今日は暗影の日だ！暗影から、レベリングから逃げるんじゃねえぞ・・・";
                    break;
                case 4:
                    post += "「今日は何の日だか知ってる？」「今日はね、暗影渦巻く壊れた世界の日だよ」";
                    break;
                case 5:
                    post += "今日は暗影♪L( ＾ω＾ )┘└( ＾ω＾ )」♪暗影レベリングが始まった♪L( ＾ω＾ )┘└( ＾ω＾ )」♪";
                    break;
                case 6:
                    post += "＿人人人人人人人人人人＿";
                    post += Environment.NewLine;
                    post += "＞　暗影が始まった！　＜";
                    post += Environment.NewLine;
                    post += "￣Y^Y^Y^Y^Y^Y^Y^Y^Y￣";
                    break;
                case 7:
                    post += "暗影渦巻く壊れた世界は経験値へと収束する!!";
                    break;
                case 8:
                    //これはひどいと僕もおもった。
                    post += "＿人人人人人人人＿";
                    post += Environment.NewLine;
                    post += "＞レベリングだ！＜";
                    post += Environment.NewLine;
                    post += "￣Y^Y^Y^Y^Y^Y￣";
                    post += Environment.NewLine;
                    post += Environment.NewLine;
                    post += "　 _n";
                    post += Environment.NewLine;
                    post += "　( ｜　 ハ_ハ";
                    post += Environment.NewLine;
                    post += "　 ＼＼ ( ‘-^ 　)";
                    post += Environment.NewLine;
                    post += "　　 ＼￣￣　 )";
                    post += Environment.NewLine;
                    post += "　　　 ７　　/";
                    post += Environment.NewLine;
                    post += "＿人人人人＿";
                    post += Environment.NewLine;
                    post += "＞暗影集合＜";
                    post += Environment.NewLine;
                    post += "￣Y^Y^Y^Y￣";
                    post += Environment.NewLine;
                    post += Environment.NewLine;
                    post += "　ハ_ハ";
                    post += Environment.NewLine;
                    post += "（ ‘-^ 　)　　n";
                    post += Environment.NewLine;
                    post += "￣　　 ＼　( E)";
                    post += Environment.NewLine;
                    post += "７　　/＼ヽ/ /";
                    break;
                default:
                    break;
            }

            return post;
        }
    }
}
