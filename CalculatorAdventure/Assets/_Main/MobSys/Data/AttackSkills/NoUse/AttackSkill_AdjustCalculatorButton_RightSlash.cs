using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.AttackSkills.NoUse
{
    [CreateAssetMenu(fileName = "AdjustCalculatorButton_RightSlash",
        menuName = "SoSetting/Mob/Skills/AdjustCalculatorButton_RightSlash", order = 1)]
    public class AttackSkill_AdjustCalculatorButton_RightSlash : AttackSkillData
    {
        public int[] slashIndex = { 25, 20, 15, 10, 5, 4, 3, 2, 1 };

        public override void Execute()
        {
            var random = new Random();
            var randomValue = random.Next(0, slashIndex.Length);
        }
    }
}