using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills.NoUse
{
    [CreateAssetMenu(fileName = "DestroyButtons_LeftSlash", menuName = "SoSetting/Mob/Skills/DestroyButtons_LeftSlash", order = 2)]
    public class AttackSkill_DestroyCalculatorButtons_LeftSlash : AttackSkillData
    {
        public int[] slashIndex;
        public override void Execute()
        {
        }
    }
}