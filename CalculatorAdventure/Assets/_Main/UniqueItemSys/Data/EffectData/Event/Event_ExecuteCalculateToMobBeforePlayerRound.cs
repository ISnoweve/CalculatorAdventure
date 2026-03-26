using _Main.CalculatorSys.Enum;
using EventSys.Interface;

namespace _Main.UniqueItemSys.Data.EffectData.Event
{
    public readonly struct Event_ExecuteCalculateToMobBeforePlayerRound : IEventData
    {
        private readonly int _modifyNumber;
        private readonly CalculatorOperator _calculatorOperator;
        public int ModifyNumber => _modifyNumber;
        public CalculatorOperator CalculatorOperator => _calculatorOperator;
        public Event_ExecuteCalculateToMobBeforePlayerRound(int modifyNumber, CalculatorOperator calculatorOperator)
        {
            _modifyNumber = modifyNumber;
            _calculatorOperator = calculatorOperator;
        }
    }
}