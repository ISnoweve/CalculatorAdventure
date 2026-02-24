using System;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    [Serializable]
    public readonly struct ButtonClickRecover : IEventData
    {
        private readonly byte _buttonIndex;
        public byte ButtonIndex => _buttonIndex;

        public ButtonClickRecover(byte index)
        {
            _buttonIndex = index;
        }
    }
}