using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.CalculatorSys.View.UI_CalculatorButton.Control;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Data.AttackSkills.Event;
using _Main.MobSys.Manager;
using _Main.SnoweveToolKit.UtilityFeature;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_TakeCalculatorButtonsMultiply", menuName = "SoSetting/Mob/Skills/Recover_TakeCalculatorButtonsMultiply", order = 4)]
    public class AtkS_Recover_TakeCalculatorButtonsMultiply : AttackSkillData
    {
        [InfoBox("Suggest not more than 2")]
        public int takeCount;
        public override void Execute()
        {
            var takeButtons = TakeCalculatorClickableButtons();

            ModifyMobQuestionWithTakeButtons(takeButtons);
            int currentQuestionNumber = MobManager.CurrentsMob.CurrentQuestionNumber;

            ButtonSystem.CloseButtonClickableByAttackSkill(takeButtons);
            
            Event_AtkS_Recover_TakeCalculatorButtonsMultiply eventData = 
                new Event_AtkS_Recover_TakeCalculatorButtonsMultiply(takeButtons,currentQuestionNumber);
            GlobalMessagePipe.GetPublisher<Event_AtkS_Recover_TakeCalculatorButtonsMultiply>().Publish(eventData);
        }

        private List<CalculatorButton> TakeCalculatorClickableButtons()
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

            return takeButtons;
        }
        
        private static void ModifyMobQuestionWithTakeButtons(List<CalculatorButton> takeButtons)
        {
            foreach (var calculatorButton in takeButtons)
            {
                MobManager.CurrentsMob.ModifyQuestionNumber(calculatorButton.CurrentValue,CalculatorOperator.Multiply);
            }
        }
    }
}