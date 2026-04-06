using _Main.CalculatorSys.Enum;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    public readonly struct CalculateResultNotify : IEventData
    {
        private readonly int _result;
        private readonly CalculatorOperator _firstOperator;
        public int Result => _result;

        public CalculatorOperator FirstOperator => _firstOperator;

        public CalculateResultNotify(in int result, in CalculatorOperator firstOperator)
        {
            _firstOperator = firstOperator;
            _result = result;
        }
    }
}