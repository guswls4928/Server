using ServerCore;
using System.Net;
using System.Text;

namespace Server
{
    class ClientSession : PacketSession
    {
        public int SessionId { get; set; }
        public GameRoom Room { get; set; }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected: {endPoint}");
            
            Program.Room.Enter(this);
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }

        public override void OnDisConnected(EndPoint endPoint)
        {
            SessionManager.Instance.Remove(this);
            if (Room != null)
                Room.Leave(this);

            Console.WriteLine($"OnDisConnected: {endPoint}");
        }
    }
}
