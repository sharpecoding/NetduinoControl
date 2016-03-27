using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;

namespace MicroApiServer.Tcp
{

    public delegate void SocketRequestHandler(object sender, Socket client);
    public class TcpServer : IDisposable
    {
        public event SocketRequestHandler OnRequest;

        public ushort Port { get; private set; }
        public bool IsListening { get; private set; }

        private readonly Socket _socket;
        private readonly Thread _thread;
        public TcpServer(ushort port)
        {
            Port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Any, port));
            _thread = new Thread(ListenThreadStart);
        }

        public void Start()
        {
            _thread.Start();
            IsListening = true;
        }

        public void Stop()
        {
            _thread.Abort();
            IsListening = false;
        }

        private void ListenThreadStart()
        {
            _socket.Listen(10);
            
            while (true)
            {
                Socket clientSocket = _socket.Accept();
                if (OnRequest != null)
                {
                    Thread thread = new Thread(() =>
                    {
                        using (clientSocket)
                        {
                            Debug.Print("Socked connected");
                            OnRequest(this, clientSocket);
                        }
                    });
                    thread.Start();
                }
            }
        }

        #region IDisposable Members
        ~TcpServer()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_socket != null)
                _socket.Close();
        }
        #endregion
    }
}
