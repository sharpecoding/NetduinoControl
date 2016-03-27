using MicroApiServer.Http;
using MicroMVC.Http;

namespace MicroApiServer.Framework
{
    public class ApiResponse : HttpResponse
    {
        private const string DefaultContentType = "text/html";

        internal ApiResponse(string content = null, string contentType = DefaultContentType)
        {
            this.Content = content;
            this.Headers.ContentType = contentType;
        }
    }
}
