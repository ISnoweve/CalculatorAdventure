using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.HealthSys.Sys;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using _Main.UtilityFeature;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data.Mob.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_Random", menuName = "SoSetting/Mob/Skills/DestroyButtons_Random",
        order = 2)]
    public class AtkS_DestroyCalculatorButtons_Random : AttackSkillData
    {
        [Title("AtkSkill Info")] public int destroyCount;

        public override void Execute()
        {
            var takeButtons = TakeCalculatorClickableButtons();

            ButtonSystem.CloseNumberButtonClickableByAttackSkill(takeButtons);
            HealthSystem.Instance.ModifyPlayerHealthByMobAttack(takeButtons);
        }

        private List<CalculatorButton> TakeCalculatorClickableButtons()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton()
                .Where(x => x.IsClick == false).ToList();

            var takeButtons = new List<CalculatorButton>();

            if (calculatorButtonsNotClick.Count() <= destroyCount)
            {
                foreach (var button in calculatorButtonsNotClick) takeButtons.Add(button);
            }
            else
            {
                calculatorButtonsNotClick.ShuffleList();
                for (var i = 0; i < destroyCount; i++) takeButtons.Add(calculatorButtonsNotClick[i]);
            }

            return takeButtons;
        }
    }
}