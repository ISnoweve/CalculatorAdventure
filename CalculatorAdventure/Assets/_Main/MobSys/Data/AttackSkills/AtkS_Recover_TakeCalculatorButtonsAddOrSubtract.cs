using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.SnoweveToolKit.UtilityFeature;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_TakeCalculatorButtons", menuName = "SoSetting/Mob/Skills/Recover_TakeCalculatorButtonsAddOrSubtract", order = 4)]
    public class AtkS_Recover_TakeCalculatorButtonsAddOrSubtract : AttackSkillBase
    {
        public int takeCount;
        public override void Execute()
        {
            List<CalculatorButton> calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton().
                Where(x => x.IsClick==false).ToList();

            List<CalculatorButton> takeButtons = new List<CalculatorButton>();
            
            if (calculatorButtonsNotClick.Count() <= takeCount)
            {
                foreach (var button in calculatorButtonsNotClick)
                {
                    takeButtons.Add(button);
                }
            }
            else
            {
                calculatorButtonsNotClick.ShuffleList();
                for (int i = 0; i < takeCount; i++)
                {
                    takeButtons.Add(calculatorButtonsNotClick[i]);
                }
            }
        }
    }
}