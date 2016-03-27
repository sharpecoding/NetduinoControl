# NetduinoControl (with MicroApiServer)

## Overview
A complete solution for using a Windows Phone to "control" outlets via a relay.
Communication is done over HTTP, leveraging MicroApiServer.

## MicroApiServer
"MVC" like implementation of a light-weight API server. Utilizes HTTP

## NetduinoControl.Api
Defines common classes to be used by NetduinoControl.Netduino and NetduinoControl.Phone.
Note, PCL isn't supported by .Net Micro and therefore these files are linked in NetduinoControl.Phone

## NetduinoControl.Netduino
Netduino application showcasing creating a controller and starting the MicroApiServer

## NetduinoControl.Phone
Windows Phone application showing how to consume the Api Server