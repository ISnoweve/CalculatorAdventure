using System;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Data.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.CalculatorSys.Sys.Runtime
{
    [Serializable]
    public class CalculatorButton
    {
        [SerializeField] private byte index;
        public byte Index => index;
        
        [SerializeField] private CalculatorButtonType calculatorButtonType;
        public CalculatorButtonType CalculatorButtonType => calculatorButtonType;
        
        [ShowIf("calculatorButtonType", CalculatorButtonType.Operator)]
        [SerializeField] private CalculatorOperator calculatorOperator;
        public CalculatorOperator CalculatorOperator => calculatorOperator;

        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private int originalValue;
        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)]
        [SerializeField] private int currentValue;
        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private int minValue = 0;
        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private int maxValue = 99;
        public int OriginalValue => originalValue;
        public int CurrentValue => currentValue;
        public int MinValue => minValue;
        public int MaxValue => maxValue;
        
        [ShowIf("calculatorButtonType", CalculatorButtonType.Feature)] 
        [SerializeField] private CalculatorFeature calculatorFeature;
        public CalculatorFeature CalculatorFeature => calculatorFeature;
        public CalculatorButton(CalculatorButtonData data)
        {
            index = data.Index;
            calculatorButtonType = data.CalculatorButtonType;
            calculatorOperator = data.CalculatorOperator;
            originalValue = data.OriginalValue;
            currentValue = data.OriginalValue;
            calculatorFeature = data.CalculatorFeature;
        }
    }
}