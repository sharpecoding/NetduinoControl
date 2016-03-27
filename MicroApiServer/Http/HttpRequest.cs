using MicroMVC.Http;

namespace MicroApiServer.Http
{
    //http://www.w3.org/Protocols/rfc2616/rfc2616-sec5.html
    public class HttpRequest : HttpBase
    {
        public string Method { get; set; }
        public string Url { get; set; }

        public HttpRequest()
        {
            Version = HttpVersion;
        }

        public override string GetHttpFirstLine()
        {
            return Method + " " + Url + " " + Version;
        }
    }
}
