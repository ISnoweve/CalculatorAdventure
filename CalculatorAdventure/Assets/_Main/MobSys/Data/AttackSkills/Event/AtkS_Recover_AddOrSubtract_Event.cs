using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct AtkS_Recover_AddOrSubtract_Event : IEventData
    {
        private readonly int _addOrSubtractValue;
        public int AddOrSubtractValue => _addOrSubtractValue;

        public AtkS_Recover_AddOrSubtract_Event(int addOrSubtractValue)
        {
            _addOrSubtractValue = addOrSubtractValue;
        }
    }
}