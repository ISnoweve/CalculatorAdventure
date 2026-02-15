using System;
using _Main.CalculatorSys.Data.Enum;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    [Serializable]
    public readonly struct CalculatorNotify : IEventData
    {
        private readonly CalculatorOperator[] _currentOperators;
        private readonly int[] _numbersInBox;
        private readonly int _indexCount;
        
        public CalculatorOperator[] CurrentOperators => _currentOperators;
        public int[] NumbersInBox => _numbersInBox;
        public int IndexCount => _indexCount;
        
        public CalculatorNotify(in CalculatorOperator[] currentOperators, in int[] numbersInBox)
        {
            _currentOperators = currentOperators;
            _numbersInBox = numbersInBox;
            _indexCount = currentOperators.Length;
        }
    }
}