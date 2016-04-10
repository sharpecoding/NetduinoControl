# NetduinoControl (with MicroApiServer)

## Overview
A complete solution for using a Windows Phone to "control" outlets via a relay.
Communication is done over HTTP, leveraging MicroApiServer.

I am in the process of updating this documentation

## MicroApiServer
"MVC" like implementation of a light-weight API server. Utilizes HTTP

### Routing Table
Currently there isn't any flexibility in the routing tables:

| Url | Invoked Action |
|---|---|
|<ip>/|Api Not Found (unless HelpEnabled is true, see below)|
|<ip>/{controller}| Controller::Default|
|<ip>/{controller}/{action}| Controller::Action|
|<ip>/{controller}/{action}/{arg0}| Controller::Action(Arg0)|
|<ip>/{controller}/{action}/{arg0}/{arg1}/..| Controller::Action(Arg0, Arg1, ...)|

### Arguments
Arguments are seperated by "/" in the url, and will be bound in the order they appear in the url. Booleans, Integers and Strings are supported. Example:
http://<ip>/Outlet/SetState/1/true will be routed to OutletController::SetState(1, true), where 1 and true are integers and booleans respectively

Named parameters (/{controller}/{action}?{name}={value}) is left to do.

### HelpEnabled
When HelpEnabled is true, urls routed to <ip>/ do not return Api Not Found, but instead show a list of all the controllers/actions registered with the server.

## NetduinoControl.Api
Defines common classes to be used by NetduinoControl.Netduino and NetduinoControl.Phone.
Note, PCL isn't supported by .Net Micro and therefore these files are linked in NetduinoControl.Phone

## NetduinoControl.Netduino
Netduino application showcasing creating a controller and starting the MicroApiServer

## NetduinoControl.Phone
Windows Phone application showing how to consume the Api Server

## Example

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

    public ApiResponse Default()
    {
        return Ok("Welcome");
    }

    public ApiResponse DoSomething(int count, bool flag, string name)
    {
        return Json(new SampleApiResult { Status = true, Message = "Executed successfully"});
    }
}
```

### Over HTTP
GET /Sample/ - "Welcome"

GET /Sample/DoSomething/1/true/foo - {Status: true, Message: "Executed successfully"}
