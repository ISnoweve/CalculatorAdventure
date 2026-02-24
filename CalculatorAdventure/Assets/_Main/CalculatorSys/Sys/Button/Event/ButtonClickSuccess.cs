using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonClickSuccess : IEventData
    {
        private readonly byte _buttonIndex;
        public byte ButtonIndex => _buttonIndex;

        public ButtonClickSuccess(byte index)
        {
            _buttonIndex = index;
        }
    }
}