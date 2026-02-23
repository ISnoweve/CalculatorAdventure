using System;
using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.View.EventData;
using MessagePipe;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.CalculatorSys.View
{
    public class CalculatorButtonView : MonoBehaviour
    {
        [SerializeField] private byte _index;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private CalculatorButtonType _calculatorButtonType;

        [ShowIf("_calculatorButtonType", CalculatorButtonType.Operator)] [SerializeField]
        private CalculatorOperator _calculatorOperator;

        public byte index => _index;
        public Button Button => _button;
        public TMP_Text ButtonText => _buttonText;
        public CalculatorButtonType CalculatorButtonType => _calculatorButtonType;
        public CalculatorOperator CalculatorOperator => _calculatorOperator;

        #region Behaviour

        public void Initialize(CalculatorButton button)
        {
            _index = button.Index;
            DetectButtonType(button);

            _button.onClick.AddListener(OnButtonClick);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void DetectButtonType(CalculatorButton button)
        {
            switch (button.CalculatorButtonType)
            {
                case CalculatorButtonType.None:
                    break;
                case CalculatorButtonType.NumberLock:
                    _calculatorButtonType = CalculatorButtonType.NumberLock;
                    _button.interactable = false;
                    _buttonText.text = " ";
                    break;
                case CalculatorButtonType.NumberActivate:
                    _calculatorButtonType = CalculatorButtonType.NumberActivate;
                    _button.interactable = true;
                    _buttonText.text = button.CurrentValue.ToString();
                    break;
                case CalculatorButtonType.Operator:
                    _calculatorButtonType = CalculatorButtonType.Operator;
                    DetectOperator(button.CalculatorOperator);
                    break;
                case CalculatorButtonType.Feature:
                    _calculatorButtonType = CalculatorButtonType.Feature;
                    DetectFeature(button.CalculatorFeature);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DetectOperator(CalculatorOperator calculatorOperator)
        {
            _buttonText.text = calculatorOperator switch
            {
                CalculatorOperator.Add => "+",
                CalculatorOperator.Subtract => "-",
                CalculatorOperator.Multiply => "*",
                CalculatorOperator.Divide => "/",

                _ => throw new ArgumentOutOfRangeException(nameof(calculatorOperator), calculatorOperator, null)
            };
        }

        private void DetectFeature(CalculatorFeature calculatorFeature)
        {
            _buttonText.text = calculatorFeature switch
            {
                CalculatorFeature.Equal => "=",
                CalculatorFeature.DelOperator => "DelO",
                CalculatorFeature.DelNumber => "DelN",
                _ => throw new ArgumentOutOfRangeException(nameof(calculatorFeature), calculatorFeature, null)
            };
        }

        private void OnButtonClick()
        {
            var buttonOnClick = new ButtonOnClick(_index);
            GlobalMessagePipe.GetPublisher<ButtonOnClick>().Publish(buttonOnClick);
        }

        public void ChangeButtonState(bool isClickAble)
        {
            if (_calculatorButtonType == CalculatorButtonType.NumberLock ||
                _calculatorButtonType == CalculatorButtonType.Operator ||
                _calculatorButtonType == CalculatorButtonType.Feature) return;

            _button.interactable = isClickAble;
        }

        #endregion
    }
}