using System;
using MicroApiServer.Framework;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using NetduinoControl.Api;
using NetduinoControl.Netduino.Drivers;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoControl.Netduino.Controllers
{
    public class OutletController : Controller
    {
        private readonly ApiResult OutOfRangeResult = new ApiResult {Error = "Index out of range"};

        private const int OutletCount = 4;

        private readonly bool[] _states;
        private readonly InvertedOutputPort[] _outputs;

        private readonly Cpu.Pin[] _pins = { Pins.GPIO_PIN_D0, Pins.GPIO_PIN_D1, Pins.GPIO_PIN_D2, Pins.GPIO_PIN_D3 };

        public OutletController() : base("Outlets")
        {
            _states = new bool[OutletCount];
            _outputs = new InvertedOutputPort[OutletCount];
            for (int i = 0; i < OutletCount; i++)
            {
                _outputs[i] = new InvertedOutputPort(_pins[i], true);
            }
        }

        public ApiResponse GetState(int index)
        {
            if ((index < 0) || (index > OutletCount))
                return Json(OutOfRangeResult);

            return Json(new OutletApiResult { State = _states[index], Success = true});
        }

        public ApiResponse SetState(int index, bool value)
        {
            if ((index < 0) || (index > OutletCount))
                return Json(OutOfRangeResult);

            _states[index] = value;
            _outputs[index].Write(_states[index]);

            return Json(new OutletApiResult { State = _states[index], Success = true });
        }

        public ApiResponse GetStates()
        {
            return Json(new OutletApiResult { States = _states, Success = true });
        }
    }
}
