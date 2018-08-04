using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ANEI_bot
{
    class BotController
    {
        public AbstractClient ServiceClient;
        bool loop = false;
        DateTime NextPost;  //おすすめクエスト通知時間
        DateTime notifyANEI;    //暗影の日1時間前
        Task looptask;
        Random rnd;


        public BotController()
        {
            ServiceClient = new TwitterClient();
            setNextPostTime();
            setANEInotify();
            rnd = new Random();

            looptask = StartLoop();
        }

        private void setNextPostTime()
        {
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            DateTime nextTime = DateTime.Now + ts;

            NextPost = new DateTime(nextTime.Year, nextTime.Month, nextTime.Day, 0, 0, 0);

        }

        private void setANEInotify()
        {
            TimeSpan ts60 = new TimeSpan(1, 0, 0);
            //DateTime nowDT = DateTime.Now;
            (int day, DateTime time) = RecommendQuestCalculator.nextnextQuest(0, DateTime.Now);
            
            if(day == 1 && (DateTime.Now > new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,23,0,0)))
            {
                DateTime fixDT = DateTime.Now + new TimeSpan(1, 0, 0, 0);
                (day, time) = RecommendQuestCalculator.nextnextQuest(0, fixDT);
            }

            time = time - ts60;

            notifyANEI = time;
            
        }


        /// <summary>
        /// 引数の時刻のおすすめクエストを投稿
        /// </summary>
        /// <param name="time"></param>
        public void postRecommendQuest(DateTime time)
        {
            int index = RecommendQuestCalculator.recommandQuest(time);

            if (index == 0)
            {
                string postStr = ANEI_POST(time);
                ServiceClient.sendMessage(postStr);
            }
            else
            {
                (int day, DateTime nextday) = RecommendQuestCalculator.nextQuest(0, time);
                string postStr = string.Format("今日のおすすめクエストは{0}です。次の暗影は{1}です。",
                    RecommendQuestCalculator.getRecommendQuestName(index),
                    nextday.ToString("MM月dd日"));
                ServiceClient.sendMessage(postStr);
            }
        }

        /// <summary>
        /// 暗影の専用のPOST
        /// </summary>
        /// <param name="time"></param>
        private string ANEI_POST(DateTime time)
        {
            string post = "";
            int RndIndex = rnd.Next() % 7;

            switch (RndIndex)
            {
                case 0:
                    post += "うおおおお今日は暗影だ！暗影暗影暗影暗影！";
                    break;
                case 1:
                    post += "さぁ！ひれ伏したまえ。暗影の時間だ！";
                    break;
                case 2:
                    post += "本日は暗影の日です。レベリングの日です。";
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
                default:
                    break;
            }

            return post;
        }

        public void ANEInotify()
        {
            ServiceClient.sendMessage("もうすぐ暗影の日だ！うおおおおおお！！！！！！");
        }

        private void eventloop()
        {
            while (loop)
            {
                if (DateTime.Now > NextPost)
                {
                    postRecommendQuest(DateTime.Now);
                    setNextPostTime();
                }

                if(DateTime.Now > notifyANEI)
                {
                    ANEInotify();
                    setANEInotify();
                }


                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// イベントルームの開始
        /// </summary>
        /// <returns></returns>
        private async Task StartLoop()
        {
            loop = true;
            await Task.Run(() => eventloop());
        }
        
    }
}
