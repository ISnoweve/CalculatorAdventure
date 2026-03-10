using EventSys.Interface;

namespace _Main.MobBattleSys.Sys.MobSys.Event
{
    public readonly struct MobTurn_UpdateBehaviourNumber : IEventData
    {
        public int MobAttackSkillCountDown { get; }

        public MobTurn_UpdateBehaviourNumber(int mobAttackSkillCountDown)
        {
            this.MobAttackSkillCountDown = mobAttackSkillCountDown;
        }
    }
}