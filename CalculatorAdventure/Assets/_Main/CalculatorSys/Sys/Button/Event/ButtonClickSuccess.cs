using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonClickSuccess : IEventData
    {
        public CalculatorButton Button { get; }

        public ButtonClickSuccess(CalculatorButton button)
        {
            Button = button;
        }
    }
}