using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MicroMVC.Http;
using Microsoft.SPOT;

namespace MicroApiServer.Http
{
    internal class HttpClient
    {
        private readonly NetworkStream _stream;
        
        public HttpClient(Socket socket)
        {
            _stream = new NetworkStream(socket);
        }

        public HttpRequest ReadRequest()
        {
            byte[] bytes = ReadAllBytes();
            if (bytes == null)
            {
                Debug.Print("Unable to read Socket");
                return null;
            }

            char[] chars = Encoding.UTF8.GetChars(bytes);
            string http = new string(chars);
            return HttpClient.ParseRequest(http);
        }

        public void WriteResponse(HttpResponse response)
        {
            string http = response.Compose();
            byte[] buffer = Encoding.UTF8.GetBytes(http);
            _stream.Write(buffer, 0, buffer.Length);
        }

        private static HttpRequest ParseRequest(string http)
        {
            HttpRequest request = new HttpRequest();
            
            string[] lines = http.Split('\n');

            string[] requestLineParts = lines[0].Split(' ');
            request.Method = requestLineParts[0];
            request.Url = requestLineParts[1];

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].IndexOf(':') == -1)
                    break;
                
                string[] parts = lines[i].Split(':');
                request.Headers.Set(parts[0], parts[1].Trim());
            }

            return request;
        }

        private byte[] ReadAllBytes()
        {
            if (!_stream.CanRead)
                return null;

            byte[] data = new byte[(int)_stream.Length];
            if (data.Length == 0)
                return null;

            _stream.Read(data, 0, data.Length);
            return data;
        }
    }
}
