using _Main.CalculatorSys.Manager;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.Mob.AttackSkills.NoUse
{
    [CreateAssetMenu(fileName = "AdjustCalculatorButton_LeftSlash",
        menuName = "SoSetting/Mob/Skills/AdjustCalculatorButton_LeftSlash", order = 1)]
    public class AttackSkill_AdjustCalculatorButton_LeftSlash : AttackSkillData
    {
        public int[] slashIndex = { 21, 16, 11, 6, 1, 2, 3, 4, 5 };

        public override void Execute()
        {
            var random = new Random();
            var randomValue = random.Next(0, slashIndex.Length);

            switch (slashIndex[randomValue])
            {
                case 21:
                    CalculatorButtonManager.GetButtonByIndex(21);
                    break;
                case 16:
                    break;
                case 11:
                    break;
                case 6:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }
}