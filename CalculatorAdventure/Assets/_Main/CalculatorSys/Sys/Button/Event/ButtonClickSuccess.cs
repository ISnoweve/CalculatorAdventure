using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonClickSuccess : IEventData
    {
        public byte ButtonIndex { get; }

        public ButtonClickSuccess(byte index)
        {
            ButtonIndex = index;
        }
    }
}