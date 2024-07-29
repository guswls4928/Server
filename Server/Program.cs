using System.Net;
using ServerCore;

namespace Server
{
    class Program
    {
        static Listner _listner = new Listner();
        public static GameRoom Room = new GameRoom();

        static void Main(string[] args)
        {
            PacketManager.Instance.Register();

            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _listner.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            Console.WriteLine("Listening...");

            while (true)
            {

            }
        }
    }
}