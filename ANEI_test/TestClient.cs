using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using ANEI_bot;

namespace ANEI_test
{
    /// <summary>
    /// テスト用ダミークライアント
    /// </summary>
    class TestClient : AbstractClient
    {
        override public bool sendMessage(string message)
        {
            Console.WriteLine(message);
            Debug.Write(message);
            Debug.Write('\n');
            return true;
        }

        override public bool login(loginParams param)
        {
            Console.WriteLine("LoginClient!!");
            Debug.WriteLine("LoginClient!!\n");
            return true;
        }

        override public bool saveToken()
        {
            Console.WriteLine("LoginClient!!");
            Debug.WriteLine("LoginClient!!\n");
            return true;
        }

        override public loginParams loadToken()
        {
            loginParams login = new loginParams();
            login.LoginClientName = "TestClient";
            return login;
        }

    }
}
