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
        
        [SerializeField] private CalculatorButtonType _calculatorButtonType;
        public CalculatorButtonType CalculatorButtonType => _calculatorButtonType;
        
        [ShowIf("_calculatorButtonType", CalculatorButtonType.Operator)]
        [SerializeField] private CalculatorOperator _calculatorOperator;
        public CalculatorOperator CalculatorOperator => _calculatorOperator;

        [HideIf("_calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private int originalValue;
        [HideIf("_calculatorButtonType", CalculatorButtonType.Operator)]
        [SerializeField] private int currentValue;
        [HideIf("_calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private int minValue = 0;
        [HideIf("_calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private int maxValue = 99;
        public int OriginalValue => originalValue;
        public int CurrentValue => currentValue;
        public int MinValue => minValue;
        public int MaxValue => maxValue;
        public CalculatorButton(CalculatorButtonData data)
        {
            index = data.Index;
            _calculatorButtonType = data.CalculatorButtonType;
            _calculatorOperator = data.CalculatorOperator;
            originalValue = data.OriginalValue;
            currentValue = data.OriginalValue;
        }
    }
}