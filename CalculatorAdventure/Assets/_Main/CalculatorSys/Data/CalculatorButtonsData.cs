using UnityEngine;

namespace _Main.CalculatorSys.Data
{
    [CreateAssetMenu(fileName = "CalculatorBase", menuName = "SoSetting/Calculator/CalculatorBase", order = 0)]
    public class CalculatorButtonsData : ScriptableObject
    {
        [SerializeField] private CalculatorButtonData[] numberButtons = new CalculatorButtonData[32];
        public CalculatorButtonData[] NumberButtons => numberButtons;
    }
}