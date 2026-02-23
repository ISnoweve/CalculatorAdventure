using UnityEngine;

namespace _Main.CalculatorSys.Data
{
    [CreateAssetMenu(fileName = "CalculatorSystemData", menuName = "SoSetting/Calculator/CalculatorSystemData",
        order = 0)]
    public class CalculatorSystemData : ScriptableObject
    {
        [SerializeField] private int calculatorOperationAndValueCount;
        public int CalculatorOperationAndValueCount => calculatorOperationAndValueCount;
    }
}