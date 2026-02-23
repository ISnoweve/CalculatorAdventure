using _Main.CalculatorSys.Data.Enum;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    public readonly struct CalculatorResultNotify : IEventData
    {
        public int Result { get; }

        public CalculatorOperator FirstOperator { get; }

        public CalculatorResultNotify(in int result, in CalculatorOperator firstOperator)
        {
            FirstOperator = firstOperator;
            Result = result;
        }
    }
}