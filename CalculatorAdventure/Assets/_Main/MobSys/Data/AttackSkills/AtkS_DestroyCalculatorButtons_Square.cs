using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_Square", menuName = "SoSetting/Mob/Skills/DestroyButtons_Square",
        order = 2)]
    public class AtkS_DestroyCalculatorButtons_Square : AttackSkillData
    {
        public int centerIndex;

        public override void Execute()
        {
        }
    }
}