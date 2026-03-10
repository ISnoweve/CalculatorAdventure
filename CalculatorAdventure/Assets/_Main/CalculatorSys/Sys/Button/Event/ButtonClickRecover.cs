using System;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    [Serializable]
    public readonly struct ButtonClickRecover : IEventData
    {
        public byte ButtonIndex { get; }

        public ButtonClickRecover(byte index)
        {
            ButtonIndex = index;
        }
    }
}