using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Sys.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.EventData
{
    [Serializable]
    public readonly struct ButtonsSpawn : IEventData
    {
        private readonly List<CalculatorButton> _buttons;
        public List<CalculatorButton> Buttons => _buttons;
        public ButtonsSpawn(List<CalculatorButton> buttons)
        {
            _buttons = buttons;
        }
    }
}