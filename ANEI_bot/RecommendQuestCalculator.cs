using System;
using System.Collections.Generic;
using System.Text;

namespace ANEI_bot
{
    static class RecommendQuestCalculator
    {
        //基準となる日(暗影の日)
        static DateTime epoc = new DateTime(2018,5,31);

        /// <summary>
        /// DateTimeの時分秒をすべて0にする
        /// </summary>
        /// <param name="time"></param>
        /// <returns>fixed datatime</returns>
        static private DateTime fixDateTime(DateTime time)
        {
            return new DateTime(time.Month, time.Month, time.Day, 0, 0, 0);
        }


        /// <summary>
        /// 引数の日のおすすめクエストを返す。
        /// 0:暗影
        /// </summary>
        /// <param name="time"></param>
        /// <returns>おすすめクエスト</returns>
        public static int recommandQuest(DateTime time)
        {
            TimeSpan ts = time - epoc;
            return ts.Days % 5;
        }

        /// <summary>
        /// 引数で指定したクエストが発生する一番近い日を返す。
        /// そこまでの日数も返す
        /// </summary>
        /// <param name="quest"></param>
        /// <param name="nowDay"></param>
        /// <returns>その日までの日数、クエストの日</returns>
        public static (int days,DateTime day) nextQuest(int quest,DateTime nowDay)
        {
            DateTime start = new DateTime(nowDay.Year, nowDay.Month, nowDay.Day, 0, 0, 0);
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
        public static (int days, DateTime day) nextnextQuest(int quest, DateTime nowDay)
        {
            (int d, DateTime time) = nextQuest(quest, nowDay);
            if(d == 0)
            {
                return (5,time + new TimeSpan(5, 0, 0, 0));
            }
            else
            {
                return (d,time);
            }
        }

        /// <summary>
        /// 引数で指定した番号をクエスト名にする
        /// </summary>
        /// <param name="index"></param>
        /// <returns>クエスト名</returns>
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
    }
}
