using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    internal class Session
    {
        Socket _socket;

        int _disconnected = 0;

        public void Start(Socket socket)
        {
            _socket = socket;
            SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);
            recvArgs.SetBuffer(new byte[1024], 0, 1024);

            RegisterRecv(recvArgs);
        }

        public void Send(byte[] sendBuff)
        {
            _socket.Send(sendBuff);
        }
        
        public void DisConnect()
        {
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
                return;

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        #region Network
        void RegisterRecv(SocketAsyncEventArgs args)
        {
            bool pending = _socket.ReceiveAsync(args);
            if (!pending)
            {
                OnRecvCompleted(null, args);
            }
        }

        void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                try
                {
                    string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
                    Console.WriteLine($"[From Client] {recvData}");

                    RegisterRecv(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"OnRecvCompleted Failed {e}");
                }   
            }
            else
            {
                Console.WriteLine($"Client disconnected. {args.SocketError}");
            }
        }
        #endregion
    }
}
