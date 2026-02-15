using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonUpdateFail : IEventData
    {
        private readonly CalculatorButton _button;
        public CalculatorButton Button => _button;

        public ButtonUpdateFail(CalculatorButton button)
        {
            _button = button;
        }
    }
}