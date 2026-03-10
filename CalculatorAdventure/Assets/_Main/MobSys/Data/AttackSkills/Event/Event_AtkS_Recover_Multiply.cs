using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_Multiply : IEventData
    {
        public int MultiplyValue { get; }

        public int MobNewQuestionNumber { get; }

        public Event_AtkS_Recover_Multiply(int mobNewQuestionNumber, int multiplyValue)
        {
            MobNewQuestionNumber = mobNewQuestionNumber;
            MultiplyValue = multiplyValue;
        }
    }
}