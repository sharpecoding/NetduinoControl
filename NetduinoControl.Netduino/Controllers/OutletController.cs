using System;
using MicroApiServer.Framework;
using Microsoft.SPOT;
using NetduinoControl.Api;

namespace NetduinoControl.Netduino.Controllers
{
    public class OutletController : Controller
    {
        private readonly ApiResult OutOfRangeResult = new ApiResult {Error = "Index out of range"};

        private const int OutletCount = 4;
        private readonly bool[] _states;

        public OutletController() : base("Outlets")
        {
            _states = new bool[OutletCount];
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
            
            //TODO: Send to relay

            return Json(new OutletApiResult { State = _states[index], Success = true });
        }

        public ApiResponse GetStates()
        {
            return Json(new OutletApiResult { States = _states, Success = true });
        }
    }
}
