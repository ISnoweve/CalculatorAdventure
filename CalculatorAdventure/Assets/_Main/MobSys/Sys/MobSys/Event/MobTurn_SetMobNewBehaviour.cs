using _Main.MobSys.Data.Mob.AttackSkills.Base;
using EventSys.Interface;

namespace _Main.MobSys.Sys.MobSys.Event
{
    public readonly struct MobTurn_SetMobNewBehaviour : IEventData
    {
        public AttackSkillData AtkSData { get; }

        public int MobNewBehaviourNumber { get; }

        public MobTurn_SetMobNewBehaviour(AttackSkillData atkSData, int mobNewBehaviourNumber)
        {
            AtkSData = atkSData;
            MobNewBehaviourNumber = mobNewBehaviourNumber;
        }
    }
}