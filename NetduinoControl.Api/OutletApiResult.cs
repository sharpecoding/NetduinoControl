using System;

namespace NetduinoControl.Api
{
    public class OutletApiResult : ApiResult
    {
        public bool State { get; set; }
        public bool[] States { get; set; }
    }
}
