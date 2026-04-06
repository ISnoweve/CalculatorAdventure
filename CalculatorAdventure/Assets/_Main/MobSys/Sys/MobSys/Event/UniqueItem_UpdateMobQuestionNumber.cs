using _Main.CalculatorSys.Enum;
using EventSys.Interface;

namespace _Main.MobBattleSys.Sys.MobSys.Event
{
    public readonly struct UniqueItem_UpdateMobQuestionNumber : IEventData
    {
        private readonly int _questionNumber;
        private readonly int _result;
        private readonly CalculatorOperator _firstOperator;
        public int QuestionNumber => _questionNumber;
        public int Result => _result;
        public CalculatorOperator FirstOperator => _firstOperator;
        

        public UniqueItem_UpdateMobQuestionNumber(int questionNumber, int result, CalculatorOperator firstOperator)
        {
            _questionNumber = questionNumber;
            _firstOperator = firstOperator;
            _result = result;
        }
    }
}