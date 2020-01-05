using System;

namespace Svg2EmfServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Svg2EmfServer: start...");
            var server = new Server();
            server.start();
        }
    }
}
