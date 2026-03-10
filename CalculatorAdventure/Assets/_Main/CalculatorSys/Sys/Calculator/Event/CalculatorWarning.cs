using _Main.CalculatorSys.Sys.Calculator.Enum;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    public readonly struct CalculatorWarning : IEventData
    {
        public CalculatorWarningEnum Warning { get; }

        public CalculatorWarning(CalculatorWarningEnum warning)
        {
            Warning = warning;
        }
    }
}