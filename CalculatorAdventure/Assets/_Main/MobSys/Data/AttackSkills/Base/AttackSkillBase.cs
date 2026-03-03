using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills.Base
{
    public abstract class AttackSkillBase : ScriptableObject
    {
        public int countDownRound;
        public abstract void Execute();
    }
}