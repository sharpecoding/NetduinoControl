using System;
using System.Text;
using MicroApiServer.Http;

namespace MicroMVC.Http
{
    public abstract class HttpBase
    {
        public const string HttpVersion = "HTTP/1.1";

        public string Version { get; set; }
        
        public HttpHeaderCollection Headers { get; private set; }
        public string Content { get; set; }

        protected HttpBase()
        {
            Headers = new HttpHeaderCollection();

            Version = HttpVersion;
        }

        public string Compose()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetHttpFirstLine());

            //Headers
            foreach (string key in Headers.AllKeys)
            {
                builder.AppendLine(key + ": " + Headers[key]);
            }

            builder.AppendLine();
            if (Content != null)
            {
                builder.AppendLine(Content);
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public abstract string GetHttpFirstLine();
    }
}
