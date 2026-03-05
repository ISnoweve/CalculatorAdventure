using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_AddOrSubtract : IEventData
    {
        private readonly int _addOrSubtractValue;
        public int AddOrSubtractValue => _addOrSubtractValue;

        public Event_AtkS_Recover_AddOrSubtract(int addOrSubtractValue)
        {
            _addOrSubtractValue = addOrSubtractValue;
        }
    }
}