using System;
using System.Collections;
using System.Reflection;
using System.Text;
using MicroApiServer.Framework;
using Microsoft.SPOT;

namespace MicroApiServer.Mvc
{
    internal class HelpController : Controller
    {
        public new const string Name = "Help";
        
        private readonly ArrayList _controllers;
        private readonly ArrayList _actions;
        
        public HelpController(ArrayList controllers, ArrayList actions) : base("Help")
        {
            _controllers = controllers;
            _actions = actions;
        }

        public ApiResponse Default()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<h1>Help</h1>");
            html.AppendLine("<ul>");
            foreach (Controller controller in _controllers)
            {
                html.AppendLine("<li>" + controller.Name);
                html.AppendLine("<ul>");
                Type type = controller.GetType();

                foreach (MethodInfo item in _actions)
                {
                    if (item.DeclaringType == type)
                        html.AppendLine("<li>" + item.Name + "</li>");
                }
                html.AppendLine("</ul>");
                html.AppendLine("</li>");
            }

            html.AppendLine("</ul>");
            return Ok(html.ToString());
        }
    }
}
