using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_Multiply : IEventData
    {
        private readonly int _multiplyValue;
        public int MultiplyValue => _multiplyValue;
        public Event_AtkS_Recover_Multiply(int multiplyValue)
        {
            _multiplyValue = multiplyValue;
        }
    }
}