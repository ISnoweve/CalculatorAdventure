using System;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Enum;
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

        public byte Index => index;
        public CalculatorButtonType CalculatorButtonType => calculatorButtonType;
        public CalculatorOperator CalculatorOperator => calculatorOperator;
        public int OriginalValue => originalValue;
        public int CurrentValue => currentValue;
        public int MinValue => minValue;
        public int MaxValue => maxValue;
        public CalculatorFeature CalculatorFeature => calculatorFeature;
        public bool IsClick => isClick;

        #region Set Value

        public void SetValueAndType(int value)
        {
            calculatorButtonType = CalculatorButtonType.NumberActivate;
            originalValue = value;
            currentValue = value;
            isClick = false;
        }

        #endregion
        
        #region Click Feature

        public void RecoverButtonClickAble()
        {
            if (calculatorButtonType == CalculatorButtonType.NumberNotActivate ||
                calculatorButtonType == CalculatorButtonType.Operator ||
                calculatorButtonType == CalculatorButtonType.Feature) return;

            isClick = false;
        }

        public void SetButtonClickAble()
        {
            isClick = false;
        }

        public void CloseButtonClickAble()
        {
            if (calculatorButtonType == CalculatorButtonType.NumberNotActivate ||
                calculatorButtonType == CalculatorButtonType.Operator ||
                calculatorButtonType == CalculatorButtonType.Feature) return;

            isClick = true;
        }

        public void ClickButton()
        {
            if (calculatorButtonType == CalculatorButtonType.NumberNotActivate ||
                calculatorButtonType == CalculatorButtonType.Feature) return;

            if (calculatorOperator == CalculatorOperator.Add ||
                calculatorOperator == CalculatorOperator.Subtract) return;


            isClick = true;
        }

        #endregion

        #region Modify Value

        public void ResetCurrentValue()
        {
            currentValue = originalValue;
        }

        public void ModifyCurrentValue(int value)
        {
            currentValue += value;
        }


        public void GetLock()
        {
        }

        #endregion
    }
}