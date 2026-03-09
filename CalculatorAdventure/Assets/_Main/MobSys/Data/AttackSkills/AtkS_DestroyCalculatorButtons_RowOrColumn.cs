using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Data.AttackSkills.Enum;
using _Main.MobSys.Enum;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_RowOrColumn", menuName = "SoSetting/Mob/Skills/DestroyButtons_RowOrColumn", order = 2)]
    public class AtkS_DestroyCalculatorButtons_RowOrColumn : AttackSkillData
    {
        public RowOrColumn rowOrColumn;
        public int rowOrColumnCount;
        
        public override void Execute()
        {
        }
    }
}