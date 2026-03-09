using EventSys.Interface;

namespace _Main.MobSys.Sys.MobSys.Event
{
    public readonly struct MobTurn_UpdateBehaviourNumber : IEventData
    {
        private readonly int mobAttackSkillCountDown;
        public int MobAttackSkillCountDown => mobAttackSkillCountDown;
        public MobTurn_UpdateBehaviourNumber(int mobAttackSkillCountDown)
        {
            this.mobAttackSkillCountDown = mobAttackSkillCountDown;
        }
    }
}