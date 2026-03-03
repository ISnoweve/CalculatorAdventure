using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct AtkS_Recover_Multiply_Event : IEventData
    {
        private readonly int _multiplyValue;
        public int MultiplyValue => _multiplyValue;
        public AtkS_Recover_Multiply_Event(int multiplyValue)
        {
            _multiplyValue = multiplyValue;
        }
    }
}