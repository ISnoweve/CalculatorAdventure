using _Main.CalculatorSys.Enum;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    public readonly struct CalculateResultNotify : IEventData
    {
        public int Result { get; }

        public CalculatorOperator FirstOperator { get; }

        public CalculateResultNotify(in int result, in CalculatorOperator firstOperator)
        {
            FirstOperator = firstOperator;
            Result = result;
        }
    }
}