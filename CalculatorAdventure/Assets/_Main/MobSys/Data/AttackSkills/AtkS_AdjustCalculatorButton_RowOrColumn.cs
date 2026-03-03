using _Main.CalculatorSys.Manager;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Enum;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "AdjustCalculatorButton_RowOrColumn", menuName = "SoSetting/Mob/Skills/AdjustCalculatorButton_RowOrColumn", order = 1)]
    public class AtkS_AdjustCalculatorButton_RowOrColumn : AttackSkillBase
    {
        public RowOrColumn rowOrColumn;
        public int rowOrColumnCount;
        public int adjustValue;
        
        public override void Execute()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllButton())
            {
                
            }
        }
    }
}