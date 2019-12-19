using System;
using System.Net;
using System.Net.Sockets;

namespace EchoServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Socket listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEp = new IPEndPoint(ipAddr, 8888);
            listenfd.Bind(ipEp);
            listenfd.Listen(0);
            Console.WriteLine("Server Run!!!");

            while (true)
            {
                Socket connfd = listenfd.Accept();
                Console.WriteLine("Server accept");

                byte[] readBuff = new byte[1024];

                int count = connfd.Receive(readBuff);
                string readStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);

                Console.WriteLine("Receive: " + readStr);

                byte[] sendBytes = System.Text.Encoding.Default.GetBytes(readStr);

                connfd.Send(sendBytes);
            }
        }
    }
}
