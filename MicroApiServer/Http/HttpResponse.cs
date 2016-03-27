using MicroMVC.Http;

namespace MicroApiServer.Http
{
    //http://www.w3.org/Protocols/rfc2616/rfc2616-sec6.html
    public class HttpResponse : HttpBase
    {
        public HttpStatusCode Status { get; set; }
        
        public HttpResponse() : base()
        {
            
        }

        public override string GetHttpFirstLine()
        {
            return Version + " " + (int)Status;
        }
    }
}
