using System;
using Microsoft.SPOT;

namespace MicroApiServer.Framework
{
    public class RawApiResponse : ApiResponse
    {
        private const string PlainContentType = "text/plain";

        internal RawApiResponse(object o) : base(GetBase64String(o), PlainContentType)
        {
            this.Headers.Set("Content-Transfer-Encoding", "base64");
        }

        private static string GetBase64String(object o)
        {
            byte[] buffer = Reflection.Serialize(o, o.GetType());
            return Convert.ToBase64String(buffer);
        }
    }
}
