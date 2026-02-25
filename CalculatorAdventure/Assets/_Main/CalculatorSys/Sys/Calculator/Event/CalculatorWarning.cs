using _Main.CalculatorSys.Sys.Calculator.Enum;
using EventSys.Interface;
using UnityEngine;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    public readonly struct CalculatorWarning : IEventData
    {
        private readonly CalculatorWarningEnum _warning;
        public CalculatorWarningEnum Warning => _warning;

        public CalculatorWarning(CalculatorWarningEnum warning)
        {
            _warning = warning;
        }
    }
}