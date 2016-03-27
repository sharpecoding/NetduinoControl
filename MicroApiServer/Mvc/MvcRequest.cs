using System.Collections;
using System.Reflection;
using MicroApiServer.Framework;

namespace MicroApiServer.Mvc
{
    internal class MvcRequest
    {
        public Controller Controller { get; set; }
        public MethodInfo Action { get; set; }
        public ArrayList Arguments { get; private set; }

        public MvcRequest()
        {
            Arguments = new ArrayList();
        }
    }
}
