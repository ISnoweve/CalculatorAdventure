using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills.NoUse
{
    [CreateAssetMenu(fileName = "LockCalculatorButton", menuName = "SoSetting/Mob/Skills/LockCalculatorButton", order = 5)]
    public class AttackSkill_LockCalculatorButton : AttackSkillData
    {
        public int lockCount;
        public int lockRound;
        public override void Execute()
        {
        }
    }
}