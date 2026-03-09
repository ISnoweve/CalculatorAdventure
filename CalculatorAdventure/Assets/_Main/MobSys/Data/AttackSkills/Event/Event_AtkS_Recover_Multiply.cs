using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_Multiply : IEventData
    {
        private readonly int _multiplyValue;
        public int MultiplyValue => _multiplyValue;
        private readonly int _mobNewQuestionNumber;
        public int MobNewQuestionNumber => _mobNewQuestionNumber;
        public Event_AtkS_Recover_Multiply(int mobNewQuestionNumber, int multiplyValue)
        {
            _mobNewQuestionNumber = mobNewQuestionNumber;
            _multiplyValue = multiplyValue;
        }
    }
}