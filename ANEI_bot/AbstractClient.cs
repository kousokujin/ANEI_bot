using System;
using System.Collections.Generic;
using System.Text;

namespace ANEI_bot
{
    abstract class AbstractClient
    {

        public abstract bool sendMessage(string message);
        public abstract bool login(loginParams para);
        public abstract bool saveToken();
        public abstract loginParams loadToken();
    }

    class loginParams
    {
        public string LoginClientName = "";
    }
}
