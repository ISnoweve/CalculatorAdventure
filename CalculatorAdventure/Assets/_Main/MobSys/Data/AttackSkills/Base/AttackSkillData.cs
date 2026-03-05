using _Main.MobSys.Data.AttackSkills.Enum;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills.Base
{
    public abstract class AttackSkillData : ScriptableObject
    {
        public AttackSkillType attackSkillType;
        public int countDownRound;
        public abstract void Execute();
    }
}