using _Main.CalculatorSys.Data.Enum;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Manager;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_AddOrSubtract", menuName = "SoSetting/Mob/Skills/Recover_AddOrSubtract", order = 4)]
    public class AtkS_Recover_AddOrSubtract : AttackSkillData
    {
        public int randomLimitMin;
        public int randomLimitMax;
        public override void Execute()
        {
            Random random = new Random();
            int randomValue = random.Next(randomLimitMin, randomLimitMax);
            
            
            
            // not work here. work in system.
            if (MobManager.CurrentsMob.CurrentQuestionNumber > 0)
            {
                MobManager.CurrentsMob.ModifyQuestionNumber(randomValue,CalculatorOperator.Add);
            }
            else
            {
                MobManager.CurrentsMob.ModifyQuestionNumber(-randomValue,CalculatorOperator.Subtract);
            }
        }
    }
}