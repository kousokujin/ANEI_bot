using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CoreTweet;
using static CoreTweet.OAuth;

namespace ANEI_bot
{
    class TwitterClient : AbstractClient
    {
        string consumerKey;
        string consumerSecret;
        Tokens tkn;

        string filename = @"config/token.xml";

        public TwitterClient()
        {
            loginParams logpara = loadToken();
            if (logpara == null || !(logpara is TwitterLoginParams))
            {
                Authentication();
            }
            else
            {
                login(logpara as TwitterLoginParams);
            }
        }

        /// <summary>
        /// PinによるTwitterの認証
        /// </summary>
        public void Authentication()
        {
            //TwitterLoginParams para = new TwitterLoginParams();

            Console.Write("Consumer Key:");
            consumerKey = Console.ReadLine();
            Console.Write("Consumer Secret:");
            consumerSecret = Console.ReadLine();

            var taskSession = AuthorizeAsync(consumerKey, consumerSecret);
            var session = taskSession.Result;
            Console.WriteLine("Access this url.");
            Console.WriteLine(session.AuthorizeUri.AbsoluteUri);
            Console.Write("Input Pin Code:");
            string pin = Console.ReadLine();

            var tknTsk = OAuth.GetTokensAsync(session, pin);
            tkn = tknTsk.Result;

            saveToken();
        }

        public override bool login(loginParams twitterparams)
        {
            if (twitterparams is TwitterLoginParams)
            {
                var para = (TwitterLoginParams)twitterparams;
                try
                {
                    tkn = Tokens.Create(para.consumerKey, para.consumerSecret, para.accessToken, para.accessSecret);
                }
                catch
                {
                    logOutput.writeLog("Login Failed");
                }
            }

            return true;
        }

        public override bool sendMessage(string message)
        {
            if (tkn != null)
            {
                try
                {
                    logOutput.writeLog("SendTweet「{0}」", message);
                    var tsk = tkn.Statuses.UpdateAsync(new { status = message });
                    tsk.Wait();
                    //Console.WriteLine(message);
                    return true;
                }
                catch
                {
                    logOutput.writeLog("Tweet Faild");
                    return false;
                }
            }

            return false;
        }


        //---ファイルに鍵を保存・読み込み---

        public override bool saveToken()
        {
            saveParams save = new saveParams();
            save.accessToken = tkn.AccessToken;
            save.accessSecret = tkn.AccessTokenSecret;
            save.consumerKey = consumerKey;
            save.consumerSecret = consumerSecret;

            return XmlFileIO.xmlSave(save.GetType(), filename, save);

        }

        public override loginParams loadToken()
        {
            saveParams load = new saveParams();

            if (File.Exists(filename))
            {
                object obj = new object();
                bool isLoad = XmlFileIO.xmlLoad(load.GetType(), filename, out obj);

                if(isLoad == true && obj is saveParams) {
                    load = obj as saveParams;
                    TwitterLoginParams output = new TwitterLoginParams();
                    output.accessSecret = load.accessSecret;
                    output.accessToken = load.accessToken;
                    output.consumerKey = load.consumerKey;
                    output.consumerSecret = load.consumerSecret;

                    return output;
                }
            }

            return null;
        }

    }

    class TwitterLoginParams : loginParams
    {
        public string accessToken = "";
        public string accessSecret = "";
        public string consumerKey = "";
        public string consumerSecret = "";

        public TwitterLoginParams()
        {
            LoginClientName = "Twitter";
        }
    }

    public class saveParams
    {
        public string accessToken;
        public string accessSecret;
        public string consumerKey;
        public string consumerSecret;
    }
}
