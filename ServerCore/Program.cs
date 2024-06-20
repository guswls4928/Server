using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerCore
{
    class Program
    {
        static Listner _listner = new Listner();

        static void OnAcceptHandler(Socket clientSocket)
        {
            try
            {
                Session session = new Session();
                session.Start(clientSocket);

                byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server!");
                session.Send(sendBuff);

                Thread.Sleep(1000);

                session.DisConnect();
                session.DisConnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _listner.Init(endPoint, OnAcceptHandler);
            Console.WriteLine("Listening...");

            while (true)
            {
                
            }
        }
    }
}