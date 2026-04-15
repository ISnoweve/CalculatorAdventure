using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using _Main.MobSys.Data.Mob.AttackSkills.Event;
using _Main.MobSys.Manager;
using _Main.UtilityFeature;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data.Mob.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_TakeCalculatorButtonsMultiply",
        menuName = "SoSetting/Mob/Skills/Recover_TakeCalculatorButtonsMultiply", order = 4)]
    public class AtkS_Recover_TakeCalculatorButtonsMultiply : AttackSkillData
    {
        [Title("AtkSkill Info")] [InfoBox("Suggest not more than 2")]
        public int takeCount;

        public override void Execute()
        {
            var takeButtons = TakeCalculatorClickableButtons();

            ModifyMobQuestionWithTakeButtons(takeButtons);
            var currentQuestionNumber = MobManager.CurrentsMob.CurrentQuestionNumber;

            // 快速調整
            ButtonSystem.CloseNumberButtonClickableByAttackSkill(takeButtons);

            var eventData =
                new Event_AtkS_Recover_TakeCalculatorButtonsMultiply(takeButtons, currentQuestionNumber);
            GlobalMessagePipe.GetPublisher<Event_AtkS_Recover_TakeCalculatorButtonsMultiply>().Publish(eventData);
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
            foreach (var calculatorButton in takeButtons)
                MobManager.CurrentsMob.ModifyQuestionNumber(calculatorButton.CurrentValue, CalculatorOperator.Multiply);
        }
    }
}