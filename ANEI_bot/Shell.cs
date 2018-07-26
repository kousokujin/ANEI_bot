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
            controller = new BotController();
            loop = true;
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
                    if(separate[1] != null)
                    {
                        controller.ServiceClient.sendMessage(separate[1]);
                    }
                    else
                    {
                        Console.WriteLine("内容がありません。");
                    }
                    break;

                case "stop":
                    loop = false;
                    break;
                case "exit":
                    loop = false;
                    break;

                case "":
                    break;
                default:
                    Console.Write("コマンドが見つかりません。");
                    break;
            }
        }

        private void outputPrompt()
        {
            while(logOutput.logQue != null && logOutput.logQue.Count != 0)
            {

            }

            Console.Write("ANEI_BOT > ");
        }
    }
}
