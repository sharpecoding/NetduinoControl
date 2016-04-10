using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MicroApiServer.Mvc;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Net.NetworkInformation;
using NetduinoControl.Netduino.Controllers;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoControl.Netduino
{
    public class Program
    {
        static readonly AutoResetEvent NetworkAvailableEvent = new AutoResetEvent(false);
        static readonly AutoResetEvent NetworkAddressChangedEvent = new AutoResetEvent(false);
        
        public static void Main()
        {
            // wire up events to wait for network link to connect and address to be acquired
            NetworkChange.NetworkAvailabilityChanged += (sender, args) =>
            {
                if (args.IsAvailable)
                {
                    NetworkAvailableEvent.Set();
                }
            };
            NetworkChange.NetworkAddressChanged += (sender, args) => NetworkAddressChangedEvent.Set();

            // if the network is already up or dhcp address is already set, pre-set those flags.
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                NetworkAvailableEvent.Set();

            if ((NetworkInterface.GetAllNetworkInterfaces()[0].IsDhcpEnabled) && (NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress != "0.0.0.0"))
                NetworkAddressChangedEvent.Set();

            MvcServer server = new MvcServer(helpEnabled:true);
            server.RegisterController(new OutletController());
            server.RegisterController(new TempController());

            //Wait for networks
            NetworkAvailableEvent.WaitOne();
            NetworkAddressChangedEvent.WaitOne();

            NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.Print("IP Address: " + networkInterface.IPAddress);

            server.Start();

            while (server.IsListening)
                Thread.Sleep(1000);
        }
    }
}
