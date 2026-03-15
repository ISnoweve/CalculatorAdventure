using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonValueModify : IEventData
    {
        public List<CalculatorButton> Buttons { get; }

        public ButtonValueModify(List<CalculatorButton> buttons)
        {
            Buttons = buttons;
        }
    }
}