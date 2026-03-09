using _Main.MobSys.Data.AttackSkills.Base;
using EventSys.Interface;

namespace _Main.MobSys.Sys.MobSys.Event
{
    public readonly struct MobTurn_SetMobNewBehaviour : IEventData
    {
        private readonly AttackSkillData _atkSData;
        private readonly int _mobNewBehaviourNumber;
        public AttackSkillData AtkSData => _atkSData;
        public int MobNewBehaviourNumber => _mobNewBehaviourNumber;

        public MobTurn_SetMobNewBehaviour(AttackSkillData atkSData, int mobNewBehaviourNumber)
        {
            _atkSData = atkSData;
            _mobNewBehaviourNumber = mobNewBehaviourNumber;
        }
    }
}