using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    [Serializable]
    public readonly struct AllButtonClickRecover : IEventData
    {
        public List<CalculatorButton> Buttons { get; }

        public AllButtonClickRecover(List<CalculatorButton> buttons)
        {
            Buttons = buttons;
        }
    }
}