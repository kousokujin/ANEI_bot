using System;
using System.Collections.Generic;
using System.Text;

namespace ANEI_bot
{
    class Shell
    {
        BotController controller;
        bool loop = true;

        public Shell()
        {
            outputTitle();
            controller = new BotController();
            loop = true;
            //Console.WriteLine("debug:{0}", RecommendQuestCalculator.recommandQuest(DateTime.Now));
            inputCMD();
        }

        private void inputCMD()
        {
            do
            {
                outputPrompt();
                string cmd = Console.ReadLine();
                processCommand(cmd);
            } while (loop);
        }

        private void processCommand(string command)
        {
            string[] separate = command.Split(' ');

            if(separate[0] == null)
            {
                return;
            }

            switch (separate[0])
            {
                case "post":
                    if(separate.Length > 1)
                    {
                        controller.ServiceClient.sendMessage(separate[1]);
                    }
                    else
                    {
                        Console.WriteLine("内容がありません。");
                    }
                    break;
                case "repost":
                    controller.postRecommendQuest(DateTime.Now);
                    break;

                case "stop":
                    loop = false;
                    break;
                case "exit":
                    loop = false;
                    break;
                case "show":
                    showRecomList();
                    break;
                case "":
                    break;
                default:
                    Console.WriteLine("コマンドが見つかりません。");
                    break;
            }
        }

        private void showRecomList()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);

            for (int i = 0; i < 20; i++)
            {
                string quest = RecommendQuestCalculator.recommandQuest(dt);
                Console.WriteLine("{0}:{1}", dt.ToString("yyyy/MM/dd"), quest);
                dt += ts;
            }
        }

        private void outputPrompt()
        {
            while(logOutput.logQue != null && logOutput.logQue.Count != 0)
            {

            }

            Console.Write("ANEI_BOT > ");
        }

        private void outputTitle()
        {
            Console.WriteLine("-----------------------------");
            outputVersion();
            Console.WriteLine("-----------------------------");

        }

        private void outputVersion()
        {
            Console.WriteLine("ANEI_BOT");
            Console.WriteLine("Version {0}", getAssemblyVersion());
            Console.WriteLine("Copyright (c) 2018 Kousokujin.");
            Console.WriteLine("Released under the MIT license.");
        }

        static public string getAssemblyVersion()
        {
            System.Diagnostics.FileVersionInfo ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            string version = ver.ProductVersion;

            return version;
        }
    }
}
