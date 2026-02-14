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
        
        [SerializeField] private CalculatorButtonType calculatorButtonType;
        public CalculatorButtonType CalculatorButtonType => calculatorButtonType;
        
        [ShowIf("calculatorButtonType", CalculatorButtonType.Operator)]
        [SerializeField] private CalculatorOperator calculatorOperator;
        public CalculatorOperator CalculatorOperator => calculatorOperator;

        
        [ShowIf("calculatorButtonType", CalculatorButtonType.NumberActivate)] 
        [SerializeField] private int originalValue;
        public int OriginalValue => originalValue;
        
        [ShowIf("calculatorButtonType", CalculatorButtonType.Feature)] 
        [SerializeField] private CalculatorFeature calculatorFeature;
        public CalculatorFeature CalculatorFeature => calculatorFeature;
    }
}