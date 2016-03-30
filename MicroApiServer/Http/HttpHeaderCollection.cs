using System;
using MicroMVC;

namespace MicroApiServer.Http
{
    public class HttpHeaderCollection : VariableCollection
    {
        private const string UserAgentString = "User-Agent";
        private const string ContentTypeString = "Content-Type";
        private const string ContentLengthString = "Content-Length";
        private const string HostString = "Host";
        private const string KeepAliveString = "Keep-Alive";
        private const string ConnectionString = "Connection";
        private const string DateString = "Date";
        private const string ServerString = "Server";
        private const string CacheControlString = "Cache-Control";
        
        public string UserAgent
        {
            get { return this.GetString(UserAgentString); }
            set { this.Set(UserAgentString, value); }
        }

        public string ContentType
        {
            get { return this.GetString(ContentTypeString); }
            set { this.Set(ContentTypeString, value); }
        }

        public int ContentLength
        {
            get { return this.GetInt(ContentLengthString); }
            set { this.Set(ContentLengthString, value); }
        }

        public string Host
        {
            get { return this.GetString(HostString); }
            set { this.Set(HostString, value); }
        }

        public string KeepAlive
        {
            get { return this.GetString(KeepAliveString); }
            set { this.Set(KeepAliveString, value); }
        }

        public string Connection
        {
            get { return this.GetString(ConnectionString); }
            set { this.Set(ConnectionString, value); }
        }

        public DateTime Date
        {
            get { return (DateTime) this.Get(DateString); }
            set { this.Set(DateString, value); }
        }

        public string Server
        {
            get { return this.GetString(ServerString); }
            set { this.Set(ServerString, value); }
        }

        public string CacheControl
        {
            get { return this.GetString(CacheControlString); }
            set { this.Set(CacheControlString, value); }
        }

        public HttpHeaderCollection() : base()
        {
            
        }
    }
}
