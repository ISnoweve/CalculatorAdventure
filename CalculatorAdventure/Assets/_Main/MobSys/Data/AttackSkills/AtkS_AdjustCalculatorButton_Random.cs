using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.UtilityFeature;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "AdjustCalculatorButton_Random",
        menuName = "SoSetting/Mob/Skills/AdjustCalculatorButton_Random", order = 1)]
    public class AtkS_AdjustCalculatorButton_Random : AttackSkillData
    {
        [Title("AtkSkill Info")] public int randomValueLimitMin;

        public int randomValueLimitMax;
        public int adjustCount;

        public override void Execute()
        {
            var calculatorButtons = TakeCalculatorNumberButtons();

            var random = new Random();
            var randomValue = random.Next(randomValueLimitMin, randomValueLimitMax);

            ButtonSystem.ModifyNumberButtonValueByAttackSkill(calculatorButtons, randomValue);
           
        }

        private List<CalculatorButton> TakeCalculatorNumberButtons()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllNumberButton()
                .Where(x => x.CalculatorButtonType == CalculatorButtonType.NumberActivate).ToList();

            var takeButtons = new List<CalculatorButton>();

            calculatorButtonsNotClick.ShuffleList();
            for (var i = 0; i < adjustCount; i++) takeButtons.Add(calculatorButtonsNotClick[i]);

            return takeButtons;
        }
    }
}