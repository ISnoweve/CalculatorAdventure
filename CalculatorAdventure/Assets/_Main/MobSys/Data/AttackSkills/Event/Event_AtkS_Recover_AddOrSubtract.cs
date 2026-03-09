using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_AddOrSubtract : IEventData
    {
        private readonly int _addOrSubtractValue;
        public int AddOrSubtractValue => _addOrSubtractValue;
        private readonly int _mobNewQuestionNumber;
        public int MobNewQuestionNumber => _mobNewQuestionNumber;

        public Event_AtkS_Recover_AddOrSubtract(int mobNewQuestionNumber, int addOrSubtractValue)
        {
            _mobNewQuestionNumber = mobNewQuestionNumber;
            _addOrSubtractValue = addOrSubtractValue;
        }
    }
}