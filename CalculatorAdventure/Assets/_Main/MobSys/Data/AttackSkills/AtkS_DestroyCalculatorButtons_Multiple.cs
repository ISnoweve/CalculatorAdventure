using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_Multiple", menuName = "SoSetting/Mob/Skills/DestroyButtons_Multiple", order = 2)]
    public class AtkS_DestroyCalculatorButtons_Multiple : AttackSkillBase
    {
        public int[] multiple;
        public override void Execute()
        {
        }
    }
}