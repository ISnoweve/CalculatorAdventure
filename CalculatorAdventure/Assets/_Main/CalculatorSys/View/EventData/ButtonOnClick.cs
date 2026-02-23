using System;
using EventSys.Interface;

namespace _Main.CalculatorSys.View.EventData
{
    [Serializable]
    public readonly struct ButtonOnClick : IEventData
    {
        public byte Index { get; }

        public ButtonOnClick(byte index)
        {
            this.Index = index;
        }
    }
}