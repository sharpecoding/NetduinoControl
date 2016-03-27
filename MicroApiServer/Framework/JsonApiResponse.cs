using Json.NETMF;

namespace MicroApiServer.Framework
{
    public class JsonApiResponse : ApiResponse
    {
        private const string JsonContentType = "application/json";

        internal JsonApiResponse(object o) : base(GetJson(o), JsonContentType)
        {
            
        }

        private static string GetJson(object o)
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Serialize(o);
        }
    }
}
