using System.Net.Sockets;
using MicroApiServer.Tcp;
using Microsoft.SPOT;

namespace MicroApiServer.Http
{
    public delegate HttpResponse HttpRequestHandler(HttpServer server, HttpRequest request);
    public class HttpServer
    {
        private const string DefaultServerName = "Netduino MicroMVC";
        
        public event HttpRequestHandler OnHttpRequest;

        public ushort Port
        {
            get { return _server.Port; }
        }

        public bool IsListening
        {
            get { return _server.IsListening; }
        }

        private readonly TcpServer _server;
        private readonly string _serverName;

        public HttpServer(ushort port = 80, string serverName = DefaultServerName)
        {
            _server = new TcpServer(port);
            _server.OnRequest += ServerOnRequest;
            _serverName = serverName;
        }

        private void ServerOnRequest(object sender, Socket clientSocket)
        {
            HttpClient client = new HttpClient(clientSocket);

            if (OnHttpRequest == null)
                return;

            HttpRequest request = client.ReadRequest();
            if (request == null)
                return;

            Debug.Print("Http Request: " + request.Url);
            HttpResponse response = OnHttpRequest(this, request);
            PostProcess(response);
            client.WriteResponse(response);
        }

        private void PostProcess(HttpResponse response)
        {
            response.Headers.Server = _serverName;
            response.Headers.Connection = "close";
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
