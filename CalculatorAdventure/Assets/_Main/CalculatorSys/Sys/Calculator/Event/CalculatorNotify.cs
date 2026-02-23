using System;
using _Main.CalculatorSys.Data.Enum;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    [Serializable]
    public readonly struct CalculatorNotify : IEventData
    {
        public CalculatorOperator[] CurrentOperators { get; }

        public int[] NumbersInBox { get; }

        public int IndexCount { get; }

        public CalculatorNotify(in CalculatorOperator[] currentOperators, in int[] numbersInBox)
        {
            CurrentOperators = currentOperators;
            NumbersInBox = numbersInBox;
            IndexCount = currentOperators.Length;
        }
    }
}