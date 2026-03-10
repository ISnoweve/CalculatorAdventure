using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Data.AttackSkills.Event;
using _Main.MobSys.Manager;
using _Main.SnoweveToolKit.UtilityFeature;
using MessagePipe;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_TakeCalculatorButtonsAddOrSubtract",
        menuName = "SoSetting/Mob/Skills/Recover_TakeCalculatorButtonsAddOrSubtract", order = 4)]
    public class AtkS_Recover_TakeCalculatorButtonsAddOrSubtract : AttackSkillData
    {
        public int takeCount;

        public override void Execute()
        {
            var takeButtons = TakeCalculatorClickableButtons();

            ModifyMobQuestionWithTakeButtons(takeButtons);
            var currentQuestionNumber = MobManager.CurrentsMob.CurrentQuestionNumber;

            ButtonSystem.CloseButtonClickableByAttackSkill(takeButtons);

            var eventData =
                new Event_AtkS_Recover_TakeCalculatorButtonsAddOrSubtract(takeButtons, currentQuestionNumber);
            GlobalMessagePipe.GetPublisher<Event_AtkS_Recover_TakeCalculatorButtonsAddOrSubtract>().Publish(eventData);
        }

        private List<CalculatorButton> TakeCalculatorClickableButtons()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton()
                .Where(x => x.IsClick == false).ToList();

            var takeButtons = new List<CalculatorButton>();

            if (calculatorButtonsNotClick.Count() <= takeCount)
            {
                foreach (var button in calculatorButtonsNotClick) takeButtons.Add(button);
            }
            else
            {
                calculatorButtonsNotClick.ShuffleList();
                for (var i = 0; i < takeCount; i++) takeButtons.Add(calculatorButtonsNotClick[i]);
            }

            return takeButtons;
        }

        private static void ModifyMobQuestionWithTakeButtons(List<CalculatorButton> takeButtons)
        {
            if (MobManager.CurrentsMob.CurrentQuestionNumber >= 0)
                foreach (var calculatorButton in takeButtons)
                    MobManager.CurrentsMob.ModifyQuestionNumber(calculatorButton.CurrentValue, CalculatorOperator.Add);
            else
                foreach (var calculatorButton in takeButtons)
                    MobManager.CurrentsMob.ModifyQuestionNumber(calculatorButton.CurrentValue,
                        CalculatorOperator.Subtract);
        }
    }
}