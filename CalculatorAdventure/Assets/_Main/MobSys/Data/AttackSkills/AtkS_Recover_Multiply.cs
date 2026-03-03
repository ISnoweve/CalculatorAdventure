using _Main.CalculatorSys.Data.Enum;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Manager;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_Multiply", menuName = "SoSetting/Mob/Skills/Recover_Multiply", order = 4)]
    public class AtkS_Recover_Multiply : AttackSkillBase
    {
        public int randomLimitMin;
        public int randomLimitMax;
        public override void Execute()
        {
            Random random = new Random();
            int randomValue = random.Next(randomLimitMin, randomLimitMax);
            
            
            // not work here. work in system.
            MobManager.CurrentsMob.ModifyQuestionNumber(randomValue,CalculatorOperator.Multiply);
        }
    }
}