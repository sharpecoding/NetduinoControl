using System;
using System.Collections;
using System.Reflection;
using MicroApiServer.Framework;
using MicroApiServer.Http;
using Microsoft.SPOT;

namespace MicroApiServer.Mvc
{
    public class MvcServer
    {
        //private const string DefaultController = "System";
        private const string DefaultAction = "Default";

        private readonly ArrayList _controllers;
        private readonly ArrayList _actions;

        private readonly HttpServer _server;

        private static readonly ApiResponse NotFoundResponse = new ApiResponse("<h1>Api Not Found</h1>") { Status = HttpStatusCode.NotFound };
        private static readonly ApiResponse ExceptionResponse = new ApiResponse("<h1>Exception occured processing request</h1>") { Status = HttpStatusCode.OK };

        private readonly bool _helpEnabled;
        public bool IsListening
        {
            get { return _server.IsListening; }
        }

        public MvcServer(ushort port = 80, bool helpEnabled = false)
        {
            _controllers = new ArrayList();
            _actions = new ArrayList();

            _server = new HttpServer(port);
            _server.OnHttpRequest += (server, request) =>
            {
                ApiResponse result = Dispatch(request.Url);
                return result;
            };

            _helpEnabled = helpEnabled;
            if (_helpEnabled)
                RegisterController(new HelpController(_controllers, _actions));
        }

        public void Start()
        {
            _server.Start();
        }

        public void RegisterController(Controller controller)
        {
            _controllers.Add(controller);
            
            Type type = controller.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in methods)
            {
                if (method.ReturnType == typeof(ApiResponse))
                    _actions.Add(method);
            }
        }

        public ApiResponse Dispatch(string url)
        {
            MvcRequest request = ParseRequest(url);

            if ((request.Controller == null) || (request.Action == null))
                return NotFoundResponse;

            try
            {
                if (request.Arguments.Count == 0)
                    return (ApiResponse)request.Action.Invoke(request.Controller, null);
                else
                    return (ApiResponse)request.Action.Invoke(request.Controller, request.Arguments.ToArray());
            }
            catch (Exception)
            {
                return ExceptionResponse;
            }
        }

        //{controller}/{action}/{argument}
        private MvcRequest ParseRequest(string url)
        {
            MvcRequest request = new MvcRequest();

            url = url.Substring(1);//Remove starting slash

            string controller = null;
            string action = null;

            if (url == "")
            {
                if (_helpEnabled)
                    controller = HelpController.Name;
            }
            else
            {
                string[] parts = url.Split('/');
                if (parts.Length > 0)
                    controller = parts[0];
                if (parts.Length > 1)
                    action = parts[1];

                for (int i = 2; i < parts.Length; i++)
                {
                    request.Arguments.Add(ParseArgument(parts[i]));
                }
            }

            if (controller != null)
            {
                request.Controller = GetController(controller);
                request.Action = GetAction(request.Controller, action ?? DefaultAction);
            }
            
            return request;
        }

        //Super basic parsing function, does bools, ints, and strings.
        private object ParseArgument(string arg)
        {
            if (arg.ToLower() == bool.TrueString.ToLower())
                return true;
            else if (arg.ToLower() == bool.FalseString.ToLower())
                return false;

            try
            {
                return int.Parse(arg);
            }
            catch (Exception)
            {
                return arg;
            }
        }

        private Controller GetController(string controller)
        {
            foreach (Controller item in _controllers)
            {
                if (item.Name == controller)
                    return item;
            }

            return null;
        }

        public MethodInfo GetAction(Controller controller, string action)
        {
            foreach (MethodInfo item in _actions)
            {
                if ((item.Name == action) && (item.DeclaringType == controller.GetType()))
                    return item;
            }

            return null;
        }
    }
}
