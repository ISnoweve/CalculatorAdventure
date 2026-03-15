using System.Collections.Generic;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.AttackSkills.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_Multiple", menuName = "SoSetting/Mob/Skills/DestroyButtons_Multiple",
        order = 2)]
    public class AtkS_DestroyCalculatorButtons_Multiple : AttackSkillData
    {
        [Title("AtkSkill Info")] public int[] multipleNumbers;

        public override void Execute()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton();

            var randomMultipleIndex = GetRandomMultipleIndex();
            var multipleButtons = GetMultipleButtons(calculatorButtonsNotClick);

            ButtonSystem.CloseNumberButtonClickableByAttackSkill(multipleButtons);
        }

        private int GetRandomMultipleIndex()
        {
            var randomIndex = Random.Range(0, multipleNumbers.Length);
            return multipleNumbers[randomIndex];
        }

        private List<CalculatorButton> GetMultipleButtons(List<CalculatorButton> buttons)
        {
            var multipleButtons = new List<CalculatorButton>();
            foreach (var button in buttons)
            foreach (var multipleIndex in multipleNumbers)
                if (DetectMultiple(button.CurrentValue, multipleIndex))
                {
                    multipleButtons.Add(button);
                    break;
                }

            return multipleButtons;
        }

        private bool DetectMultiple(int value, int multipleIndex)
        {
            if (value % multipleIndex == 0) return true;
            return false;
        }
    }
}