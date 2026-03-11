using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonValueModify : IEventData
    {
        private readonly List<CalculatorButton> _buttons;
        public List<CalculatorButton> Buttons => _buttons;
        public ButtonValueModify(List<CalculatorButton> buttons)
        {
            _buttons = buttons;
        }
    }
}