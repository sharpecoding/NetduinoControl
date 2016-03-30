using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace NetduinoControl.Netduino.Drivers
{
    public class InvertedOutputPort : OutputPort
    {
        public InvertedOutputPort(Cpu.Pin portId, bool initialState) : base(portId, initialState)
        {

        }

        public new void Write(bool b)
        {
            base.Write(!b);
        }
    }
}
