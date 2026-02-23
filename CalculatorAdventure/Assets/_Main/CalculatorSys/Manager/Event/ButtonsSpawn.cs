using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Manager.Event
{
    [Serializable]
    public readonly struct ButtonsSpawn : IEventData
    {
        public List<CalculatorButton> Buttons { get; }

        public ButtonsSpawn(List<CalculatorButton> buttons)
        {
            Buttons = buttons;
        }
    }
}