using UnityEngine;

namespace _Main.CalculatorSys.Data
{
    [CreateAssetMenu(fileName = "CalculatorGameSetting", menuName = "SoSetting/Calculator/CalculatorGameSetting", order = 0)]
    public class CalculatorGameSetting : ScriptableObject
    {
        [SerializeField] private CalculatorButtonsData buttonsData;
        public CalculatorButtonsData ButtonsData => buttonsData;
        [SerializeField] private CalculatorSystemData calculatorSystemData;
        public CalculatorSystemData CalculatorSystemData => calculatorSystemData;
    }
}