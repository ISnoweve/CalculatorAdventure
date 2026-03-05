using UnityEngine;

namespace _Main.CalculatorSys.Data
{
    [CreateAssetMenu(fileName = "CalculatorGameSettingSoList", menuName = "SoSetting/Calculator/CalculatorGameSettingSoList",
        order = 0)]
    public class CalculatorGameSettingSoList : ScriptableObject
    {
        [SerializeField] private CalculatorGameSetting[] calculatorGameSettings;
        public CalculatorGameSetting[] CalculatorGameSettings => calculatorGameSettings;
    }
}