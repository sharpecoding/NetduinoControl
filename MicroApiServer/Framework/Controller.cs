using MicroApiServer.Http;

namespace MicroApiServer.Framework
{
    public abstract class Controller
    {
        public string Name { get; private set; }

        protected Controller(string name)
        {
            Name = name;
        }

        public ApiResponse Ok(string text)
        {
            return new ApiResponse(text) { Status = HttpStatusCode.OK };
        }

        public ApiResponse NotFound()
        {
            return new ApiResponse { Status = HttpStatusCode.NotFound };
        }

        public ApiResponse StatusCode(HttpStatusCode code)
        {
            return new ApiResponse { Status = code };
        }

        public JsonApiResponse Json(object o)
        {
            return new JsonApiResponse(o) { Status = HttpStatusCode.OK };
        }

        public RawApiResponse Raw(object o)
        {
            return new RawApiResponse(o) { Status = HttpStatusCode.OK };
        }
    }
}
