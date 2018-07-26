using System;

namespace ANEI_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterClient client = new TwitterClient();
            //client.Authentication();
            client.sendMessage("ああああああ");
        }
    }
}
