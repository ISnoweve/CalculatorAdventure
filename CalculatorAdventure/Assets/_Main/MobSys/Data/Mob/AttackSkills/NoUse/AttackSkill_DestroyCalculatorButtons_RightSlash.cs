using _Main.MobSys.Data.Mob.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.Mob.AttackSkills.NoUse
{
    [CreateAssetMenu(fileName = "DestroyButtons_RightSlash",
        menuName = "SoSetting/Mob/Skills/DestroyButtons_RightSlash", order = 2)]
    public class AttackSkill_DestroyCalculatorButtons_RightSlash : AttackSkillData
    {
        public int[] slashIndex;

        public override void Execute()
        {
        }
    }
}