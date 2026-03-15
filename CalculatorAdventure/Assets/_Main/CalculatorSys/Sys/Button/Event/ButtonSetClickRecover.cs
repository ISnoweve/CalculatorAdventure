using System;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    [Serializable]
    public readonly struct ButtonSetClickRecover : IEventData
    {
        public byte ButtonIndex { get; }

        public ButtonSetClickRecover(byte index)
        {
            ButtonIndex = index;
        }
    }
}