# NetduinoControl (with MicroApiServer)

## Overview
A complete solution for using a Windows Phone to "control" outlets via a relay.
Communication is done over HTTP, leveraging MicroApiServer.

I am in the process of updating this documentation

## MicroApiServer
"MVC" like implementation of a light-weight API server. Utilizes HTTP

Currently there isn't any flexibility in the routing tables. Urls are routed in the format of:
http://<ip>/{controller}/{action}/{arg0}/{arg1}/...

When http://<ip>/ is hit without {controller} or {action}, "Api Not Found" is returned. Unless HelpEnabled == true, then the HelpController will list all the currently registered controllers and methods.
When http://<ip>/{controller}/ is hit without {action}, requests will be sent to the action "Default"

Arguments are seperated by "/" in the url, and will be bound in the order they appear in the url. Booleans, Integers and Strings are supported. Example:
http://<ip>/Outlet/SetState/1/true will be routed to OutletController::SetState(1, true), where 1 and true are integers and booleans respectively.

## NetduinoControl.Api
Defines common classes to be used by NetduinoControl.Netduino and NetduinoControl.Phone.
Note, PCL isn't supported by .Net Micro and therefore these files are linked in NetduinoControl.Phone

## NetduinoControl.Netduino
Netduino application showcasing creating a controller and starting the MicroApiServer

## NetduinoControl.Phone
Windows Phone application showing how to consume the Api Server

## Examples

### Creating the server:
```C#
MvcServer server = new MvcServer(helpEnabled:true);
server.RegisterController(new SampleController());
server.Start();
```

### Creating a Controller
```C#
public class SampleController : Controller
{
    public SampleController() : base("Sample")
    {
	
    }

    public ApiResponse DoSomething(int count, bool flag, string name)
    {
        return Json("Welcome");
    }
}
```