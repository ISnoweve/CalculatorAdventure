using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_Random", menuName = "SoSetting/Mob/Skills/DestroyButtons_Random",
        order = 2)]
    public class AtkS_DestroyCalculatorButtons_Random : AttackSkillData
    {
        public int destroyCount;

        public override void Execute()
        {
        }
    }
}