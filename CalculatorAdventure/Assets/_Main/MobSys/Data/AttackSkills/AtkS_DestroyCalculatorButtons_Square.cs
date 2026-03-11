using System.Collections.Generic;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_Square", menuName = "SoSetting/Mob/Skills/DestroyButtons_Square",
        order = 2)]
    public class AtkS_DestroyCalculatorButtons_Square : AttackSkillData
    {
        public int[] squareSpotIndices = new[] { 6, 7, 8, 11, 12, 13, 16, 17, 18 };
        public int squareSize = 3;

        public override void Execute()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton();
            
            int randomIndex = Random.Range(0, squareSpotIndices.Length);
            int randomCenterIndex = squareSpotIndices[randomIndex];
            List<int> squareIndexes = GetSquareIndices(randomCenterIndex, squareSize);
            
            DestroyButtonsInSquare(calculatorButtonsNotClick, squareIndexes);
        }
        
        private void DestroyButtonsInSquare(List<CalculatorButton> buttons, List<int> squareIndexes)
        {
            List<CalculatorButton> buttonsInSquare = new List<CalculatorButton>();
            
            foreach (var button in buttons)
            {
                if (squareIndexes.Contains(button.Index))
                {
                    buttonsInSquare.Add(button);
                }
            }

            ButtonSystem.CloseButtonClickableByAttackSkill(buttonsInSquare);
        }
        
        public static List<int> GetSquareIndices(int centerIndex, int squareSize, int width =5, int height =5)
        {
            var result = new List<int>();

            if (width <=0 || height <=0 || squareSize <=0) return result;
            if (centerIndex <0 || centerIndex >= width * height) return result;

            int centerRow = centerIndex / width;
            int centerCol = centerIndex % width;
            int half = squareSize /2;

            int minRow = Mathf.Max(0, centerRow - half);
            int maxRow = Mathf.Min(height -1, centerRow + half);
            int minCol = Mathf.Max(0, centerCol - half);
            int maxCol = Mathf.Min(width -1, centerCol + half);

            for (int r = minRow; r <= maxRow; r++)
            {
                for (int c = minCol; c <= maxCol; c++)
                {
                    result.Add(r * width + c);
                }
            }

            return result;
        }
    }
}