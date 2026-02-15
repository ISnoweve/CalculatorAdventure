using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonUpdateSuccess : IEventData
    {
        private readonly CalculatorButton _button;
        public CalculatorButton Button => _button;

        public ButtonUpdateSuccess(CalculatorButton button)
        {
            _button = button;
        }
    }
}