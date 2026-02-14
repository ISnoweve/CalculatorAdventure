using _Main.CalculatorSys.Data.Enum;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Event
{
    public readonly struct CalculatorResultNotify : IEventData
    {
        private readonly int _result;
        private readonly CalculatorOperator _firstOperator;
        public int Result => _result;
        public CalculatorOperator FirstOperator => _firstOperator;
        
        public CalculatorResultNotify(in int result, in CalculatorOperator firstOperator)
        {
            _firstOperator = firstOperator;
            _result = result;
        }
    }
}