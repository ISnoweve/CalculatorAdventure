using _Main.CalculatorSys.Data.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.CalculatorSys.Data
{
    [CreateAssetMenu(fileName = "CalculatorButtonData", menuName = "SoSetting/Calculator/CalculatorButtonData",
        order = 0)]
    public class CalculatorButtonData : ScriptableObject
    {
        [SerializeField] private byte index;

        [SerializeField] private CalculatorButtonType calculatorButtonType;

        [ShowIf("calculatorButtonType", CalculatorButtonType.Operator)] [SerializeField]
        private CalculatorOperator calculatorOperator;


        [ShowIf("calculatorButtonType", CalculatorButtonType.NumberActivate)] [SerializeField]
        private int originalValue;

        [ShowIf("calculatorButtonType", CalculatorButtonType.Feature)] [SerializeField]
        private CalculatorFeature calculatorFeature;

        public byte Index => index;
        public CalculatorButtonType CalculatorButtonType => calculatorButtonType;
        public CalculatorOperator CalculatorOperator => calculatorOperator;
        public int OriginalValue => originalValue;
        public CalculatorFeature CalculatorFeature => calculatorFeature;
    }
}