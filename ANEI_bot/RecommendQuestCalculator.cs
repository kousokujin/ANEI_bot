using System;
using System.Collections.Generic;
using System.Text;

namespace ANEI_bot
{
    public static class RecommendQuestCalculator
    {
        //基準となる日(暗影の日)
        //static DateTime epoc = new DateTime(2018, 5, 31);

        static QuestConfiguraor questConfig = new QuestConfiguraor();

        /// <summary>
        /// DateTimeの時分秒をすべて0にする
        /// </summary>
        /// <param name="time"></param>
        /// <returns>fixed datatime</returns>
        static private DateTime fixDateTime(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
        }


        /// <summary>
        /// 引数の日のおすすめクエストを返す。
        /// </summary>
        /// <param name="time"></param>
        /// <returns>おすすめクエスト</returns>
        public static string recommandQuest(DateTime time)
        {
            QuestConfig qst = getQuestConfig(time);
            DateTime epoc = qst.epoch_day;

            TimeSpan ts = time - epoc;
            return qst.quest_names[ts.Days % qst.quest_names.Count];
        }

        /// <summary>
        /// 引数の日がどのQuestConifigになるかを返す
        /// </summary>
        /// <param name="time">時間</param>
        /// <returns>その日のQuestConfig</returns>
        public static QuestConfig getQuestConfig(DateTime time)
        {
            DateTime fixedTime = fixDateTime(time);
            int count = 0;

            foreach (QuestConfig q in questConfig.quests)
            {
                if ((count + 1) != questConfig.quests.Count)
                {
                    DateTime nextQuest = questConfig.quests[count + 1].start_time;
                    //nextQuest -= new TimeSpan(1, 0, 0, 0);
                    if ((q.start_time <= time) && (nextQuest > time))
                    {
                        return q;
                    }
                }
                count++;
            }

            return questConfig.quests[questConfig.quests.Count - 1];
        }

        /// <summary>
        /// 引数で指定したクエストが発生する一番近い日を返す。
        /// そこまでの日数も返す
        /// </summary>
        /// <param name="quest"></param>
        /// <param name="nowDay"></param>
        /// <returns>その日までの日数、クエストの日</returns>
        public static (int days, DateTime day) nextQuest(string quest, DateTime nowDay)
        {
            DateTime start = fixDateTime(nowDay);
            int day = 0;

            while (quest != recommandQuest(start))
            {
                start += new TimeSpan(1, 0, 0, 0);
                day++;
            }

            return (day, start);
        }

        /// <summary>
        /// 引数で指定したクエストが発生する一番近い日を探す。
        /// その日にクエストが発生場合、次のクエスがが発生する日を返す。
        /// </summary>
        /// <param name="quest"></param>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public static (int days, DateTime day) nextnextQuest(string quest, DateTime nowDay)
        {
            (int d, DateTime time) = nextQuest(quest, nowDay);
            QuestConfig questconfig = getQuestConfig(nowDay);
            if (d == 0)
            {
                DateTime addOneday = (fixDateTime(nowDay) + new TimeSpan(1, 0, 0, 0));
                (int day2, DateTime time2) = nextQuest(quest, addOneday);
                return (questconfig.quest_names.Count,time2);
            }
            else
            {
                return (d, time);
            }
        }

        /// <summary>
        /// 引数で指定した番号をクエスト名にする
        /// </summary>
        /// <param name="index"></param>
        /// <returns>クエスト名</returns>
        /*
        public static string getRecommendQuestName(int index)
        {
            switch (index)
            {
                case 0:
                    return "暗影渦巻く壊れた世界";
                case 1:
                    return "境界を貫く双角の凶鳥";
                case 2:
                    return "混沌導く闇の化身";
                case 3:
                    return "混沌産み出す闇の化身";
                case 4:
                    return "混沌喚び出す龍の咆哮";
                default:
                    return "";

            }
        }
        */
    }
}
