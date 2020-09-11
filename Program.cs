using System;
using System.Net.Sockets;
using System.Text;

namespace SocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Client myClient = new Client();
            myClient.createSocketAndConnect();
            myClient.sendMessage();
            Console.ReadKey();
        }
    }
}
