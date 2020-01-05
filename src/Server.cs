using System;

namespace Svg2EmfServer
{
    public class Server
    {
        private static BeetleX.FastHttpApi.HttpApiServer mApiServer;

        public Server()
        {
            mApiServer = new BeetleX.FastHttpApi.HttpApiServer();
            //mApiServer.Options.LogLevel = BeetleX.EventArgs.LogType.All;
            mApiServer.Options.LogLevel = BeetleX.EventArgs.LogType.Warring;
            mApiServer.Options.LogToConsole = true;
            //mApiServer.Options.SetDebug(); //set view path with vs project folder
            mApiServer.Register(typeof(Server).Assembly);
            mApiServer.Options.Port = 8889; //set listen port to 80
            mApiServer.Open(); //default listen port 9090
            Console.Write(mApiServer.BaseServer);
            Console.Read();
        }

        public void start()
        {
            while (true)
            {
                Console.Read();
            }
        }
    }
}
