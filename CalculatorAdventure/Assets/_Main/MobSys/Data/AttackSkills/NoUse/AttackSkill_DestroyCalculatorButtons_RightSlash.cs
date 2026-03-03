using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills.NoUse
{
    [CreateAssetMenu(fileName = "DestroyButtons_RightSlash", menuName = "SoSetting/Mob/Skills/DestroyButtons_RightSlash", order = 2)]
    public class AttackSkill_DestroyCalculatorButtons_RightSlash : AttackSkillBase
    {
        public int[] slashIndex;
        public override void Execute()
        {
        }
    }
}