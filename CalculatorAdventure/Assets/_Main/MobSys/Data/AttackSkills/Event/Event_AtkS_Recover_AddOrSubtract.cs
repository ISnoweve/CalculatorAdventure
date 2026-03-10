using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_AddOrSubtract : IEventData
    {
        public int AddOrSubtractValue { get; }

        public int MobNewQuestionNumber { get; }

        public Event_AtkS_Recover_AddOrSubtract(int mobNewQuestionNumber, int addOrSubtractValue)
        {
            MobNewQuestionNumber = mobNewQuestionNumber;
            AddOrSubtractValue = addOrSubtractValue;
        }
    }
}