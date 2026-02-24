using System;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Data.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.CalculatorSys.Manager.Runtime
{
    [Serializable]
    public class CalculatorButton
    {
        [SerializeField] private byte index;

        [SerializeField] private CalculatorButtonType calculatorButtonType;

        [ShowIf("calculatorButtonType", CalculatorButtonType.Operator)] [SerializeField]
        private CalculatorOperator calculatorOperator;

        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)] [SerializeField]
        private int originalValue;

        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)] [SerializeField]
        private int currentValue;

        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)] [SerializeField]
        private int minValue;

        [HideIf("calculatorButtonType", CalculatorButtonType.Operator)] [SerializeField]
        private int maxValue = 99;

        [ShowIf("calculatorButtonType", CalculatorButtonType.Feature)] [SerializeField]
        private CalculatorFeature calculatorFeature;

        [SerializeField] private bool isClick = true;
        
        public byte Index => index;
        public CalculatorButtonType CalculatorButtonType => calculatorButtonType;
        public CalculatorOperator CalculatorOperator => calculatorOperator;
        public int OriginalValue => originalValue;
        public int CurrentValue => currentValue;
        public int MinValue => minValue;
        public int MaxValue => maxValue;
        public CalculatorFeature CalculatorFeature => calculatorFeature;

        public CalculatorButton(CalculatorButtonData data)
        {
            index = data.Index;
            calculatorButtonType = data.CalculatorButtonType;
            calculatorOperator = data.CalculatorOperator;
            originalValue = data.OriginalValue;
            currentValue = data.OriginalValue;
            calculatorFeature = data.CalculatorFeature;
            if (calculatorButtonType == CalculatorButtonType.NumberActivate) isClick = false;
        }

        public void RecoverButtonClickAble()
        {
            if (calculatorButtonType == CalculatorButtonType.NumberLock ||
                calculatorButtonType == CalculatorButtonType.Operator ||
                calculatorButtonType == CalculatorButtonType.Feature) return;

            isClick = false;
        }
        
        public void CloseButtonClickAble()
        {
            if (calculatorButtonType == CalculatorButtonType.NumberLock ||
                calculatorButtonType == CalculatorButtonType.Operator ||
                calculatorButtonType == CalculatorButtonType.Feature) return;

            isClick = true;
        }

        public void ClickButton()
        {
            if (calculatorButtonType == CalculatorButtonType.NumberLock ||
                calculatorButtonType == CalculatorButtonType.Operator ||
                calculatorButtonType == CalculatorButtonType.Feature) return;

            isClick = true;
        }

        public bool CheckIsClickAble()
        {
            return isClick;
        }
    }
}