using System;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    [Serializable]
    public readonly struct ButtonClickRecover : IEventData
    {
        private readonly CalculatorButton _button;
        public CalculatorButton Button => _button;

        public ButtonClickRecover(CalculatorButton button)
        {
            _button = button;
        }

    }
}