using System;
using MicroApiServer.Framework;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using NetduinoControl.Api;
using NetduinoControl.Netduino.Drivers;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoControl.Netduino.Controllers
{
    class TempController : Controller
    {
        private readonly DS18B20 _sensor;
        public TempController() : base("Temp")
        {
            _sensor = new DS18B20(new OutputPort(Pins.GPIO_PIN_D7, false));
        }

        public ApiResponse Get()
        {
            double celcius = _sensor.ReadTemperature(TemperatureFormat.CELSIUS);
            double f = ConvertCelsiusToFahrenheit(celcius);

            return Json(new TempApiResult { Fahrenheit = f });
        }

        public static double ConvertCelsiusToFahrenheit(double c)
        {
            return ((9.0 / 5.0) * c) + 32;
        }
    }
}
