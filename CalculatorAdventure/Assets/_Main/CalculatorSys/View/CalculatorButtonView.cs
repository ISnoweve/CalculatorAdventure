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
        public byte index => _index;

        [SerializeField] private Button _button;
        public Button Button => _button;
        [SerializeField] private TMP_Text _buttonText;
        public TMP_Text ButtonText => _buttonText;
        [SerializeField] private CalculatorButtonType _calculatorButtonType;
        public CalculatorButtonType CalculatorButtonType => _calculatorButtonType;
        [ShowIf("_calculatorButtonType", CalculatorButtonType.Operator)] 
        [SerializeField] private CalculatorOperator _calculatorOperator;
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
                    _button.interactable = false;
                    _buttonText.text = " ";
                    break;
                case CalculatorButtonType.NumberActivate:
                    _button.interactable = true;
                    _buttonText.text = button.CurrentValue.ToString();
                    break;
                case CalculatorButtonType.Operator:
                    DetectOperator(button.CalculatorOperator);
                    break;
                case CalculatorButtonType.Feature:
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
        
        public void OnButtonClick()
        {
            ButtonOnClick buttonOnClick = new ButtonOnClick(_index);
            GlobalMessagePipe.GetPublisher<ButtonOnClick>().Publish(buttonOnClick);   
        }

        public void GetRuntimeButtonNotify()
        {
            
        }

        #endregion
    }
}