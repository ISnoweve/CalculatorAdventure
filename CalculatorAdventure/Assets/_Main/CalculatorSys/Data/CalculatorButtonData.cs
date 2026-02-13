using _Main.CalculatorSys.Data.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.CalculatorSys.Data
{
    [CreateAssetMenu(fileName = "CalculatorButtonData", menuName = "SoSetting/Calculator/CalculatorButtonData", order = 0)]
    public class CalculatorButtonData : ScriptableObject
    {
        [SerializeField] private byte index;
        public byte Index => index;
        
        [SerializeField] private CalculatorButtonType _calculatorButtonType;
        public CalculatorButtonType CalculatorButtonType => _calculatorButtonType;
        
        [ShowIf("_calculatorButtonType", CalculatorButtonType.Operator)]
        [SerializeField] private CalculatorOperator _calculatorOperator;
        public CalculatorOperator CalculatorOperator => _calculatorOperator;

        
        [HideIf("_calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private int _originalValue;
        public int OriginalValue => _originalValue;
    }
}