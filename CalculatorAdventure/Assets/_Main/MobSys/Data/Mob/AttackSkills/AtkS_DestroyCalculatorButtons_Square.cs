using System.Collections.Generic;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data.Mob.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_Square", menuName = "SoSetting/Mob/Skills/DestroyButtons_Square",
        order = 2)]
    public class AtkS_DestroyCalculatorButtons_Square : AttackSkillData
    {
        [Title("AtkSkill Info")] public int[] squareSpotIndices = { 6, 7, 8, 11, 12, 13, 16, 17, 18 };

        public int squareSize = 3;

        public override void Execute()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton();

            var randomIndex = Random.Range(0, squareSpotIndices.Length);
            var randomCenterIndex = squareSpotIndices[randomIndex];
            var squareIndexes = GetSquareIndices(randomCenterIndex, squareSize);

            DestroyButtonsInSquare(calculatorButtonsNotClick, squareIndexes);
        }

        private void DestroyButtonsInSquare(List<CalculatorButton> buttons, List<int> squareIndexes)
        {
            var buttonsInSquare = new List<CalculatorButton>();

            foreach (var button in buttons)
                if (squareIndexes.Contains(button.Index))
                    buttonsInSquare.Add(button);

            ButtonSystem.CloseNumberButtonClickableByAttackSkill(buttonsInSquare);
        }

        private static List<int> GetSquareIndices(int centerIndex, int squareSize, int width = 5, int height = 5)
        {
            var result = new List<int>();

            if (width <= 0 || height <= 0 || squareSize <= 0) return result;
            if (centerIndex < 0 || centerIndex >= width * height) return result;

            var centerRow = centerIndex / width;
            var centerCol = centerIndex % width;
            var half = squareSize / 2;

            var minRow = Mathf.Max(0, centerRow - half);
            var maxRow = Mathf.Min(height - 1, centerRow + half);
            var minCol = Mathf.Max(0, centerCol - half);
            var maxCol = Mathf.Min(width - 1, centerCol + half);

            for (var r = minRow; r <= maxRow; r++)
            for (var c = minCol; c <= maxCol; c++)
                result.Add(r * width + c);

            return result;
        }
    }
}