using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct SetOperatorButton : IEventData
    {
        public byte ButtonIndex { get; }

        public SetOperatorButton(byte index)
        {
            ButtonIndex = index;
        }
    }
}