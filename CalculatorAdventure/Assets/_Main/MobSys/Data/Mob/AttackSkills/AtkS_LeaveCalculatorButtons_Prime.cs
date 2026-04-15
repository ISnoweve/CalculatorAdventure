using System.Collections.Generic;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.Mob.AttackSkills
{
    [CreateAssetMenu(fileName = "LeaveCalculatorButtons_Prime",
        menuName = "SoSetting/Mob/Skills/LeaveCalculatorButtons_Prime", order = 3)]
    public class AtkS_LeaveCalculatorButtons_Prime : AttackSkillData
    {
        public override void Execute()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton();

            var calculatorButtonsIsPrime = GetPrimes(calculatorButtonsNotClick, out var otherCalculatorButtons);

            ButtonSystem.CloseNumberButtonClickableByAttackSkill(otherCalculatorButtons);
        }

        private List<CalculatorButton> GetPrimes(List<CalculatorButton> inputList,
            out List<CalculatorButton> otherButtons)
        {
            otherButtons = new List<CalculatorButton>();
            var primeButtons = new List<CalculatorButton>();

            foreach (var calculatorButton in inputList)
                if (IsPrime(calculatorButton.CurrentValue))
                    primeButtons.Add(calculatorButton);
                else
                    otherButtons.Add(calculatorButton);

            return primeButtons;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = Mathf.FloorToInt(Mathf.Sqrt(number));
            for (var i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}