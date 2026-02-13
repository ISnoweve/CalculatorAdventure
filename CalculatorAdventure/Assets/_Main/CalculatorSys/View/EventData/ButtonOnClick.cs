using System;
using EventSys.Interface;

namespace _Main.CalculatorSys.View.EventData
{
    [Serializable]
    public readonly struct ButtonOnClick : IEventData
    {
        private readonly byte index;
        public byte Index => index;
        
        public ButtonOnClick(byte index)
        {
            this.index = index;
        }
    }
}