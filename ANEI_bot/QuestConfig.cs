using System;
using System.Collections.Generic;
using System.Text;

namespace ANEI_bot
{
    public class QuestConfig
    {
        /// <summary>
        /// エポックとなる日
        /// </summary>
        public DateTime epoch_day;

        /// <summary>
        ///始まりの日。
        /// </summary>
        public DateTime start_time;

        /// <summary>
        /// クエスト一覧。順番に注意。
        /// </summary>
        public List<string> quest_names;
    }

    public class QuestConfiguraor
    {
        public List<QuestConfig> quests;

        public QuestConfiguraor()
        {
            quests = new List<QuestConfig>();

            QuestConfig q1 = new QuestConfig();
            q1.epoch_day = new DateTime(2018, 5, 31);
            q1.start_time = new DateTime(2018, 5, 31);
            q1.quest_names = new List<string>() {"暗影渦巻く壊れた世界", "境界を貫く双角の凶鳥", "混沌導く闇の化身", "混沌産み出す闇の化身", "混沌喚び出す龍の咆哮"};
            quests.Add(q1);

            QuestConfig q2 = new QuestConfig();
            q2.epoch_day = new DateTime(2019, 3, 28);
            q2.start_time = new DateTime(2019, 3, 28);
            q2.quest_names = new List<string>() { "暗影渦巻く壊れた世界" };
            quests.Add(q2);

            QuestConfig q3 = new QuestConfig();
            q3.epoch_day = new DateTime(2018, 5, 31);
            q3.start_time = new DateTime(2019, 4, 4);
            q3.quest_names = new List<string>() { "暗影渦巻く壊れた世界", "境界を貫く双角の凶鳥", "混沌導く闇の化身", "混沌産み出す闇の化身", "混沌喚び出す龍の咆哮" };
            quests.Add(q3);

            QuestConfig q4 = new QuestConfig();
            q4.epoch_day = new DateTime(2019, 8, 20);
            q4.start_time = new DateTime(2019, 8, 21);
            q4.quest_names = new List<string>() { "暗影渦巻く壊れた世界", "平穏を引き裂く混沌", "混沌導く闇の化身", "戦塵を招く魔城の脅威", "静寂に生まれし混沌" };
            quests.Add(q4);
        }
        
    }
}
