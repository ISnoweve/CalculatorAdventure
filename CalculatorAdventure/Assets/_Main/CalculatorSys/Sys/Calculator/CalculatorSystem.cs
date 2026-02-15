using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.CalculatorSys.View.EventData;
using BolingsUnityTools;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.Sys.Calculator
{
    [Serializable]
    public class CalculatorSystem : Singleton<CalculatorSystem>
    {
        [SerializeField] private CalculatorOperator[] currentOperators;
        [SerializeField] private int[] numbersInBox;
        [SerializeField] private int originalCalculatorOperationAndValueCount;
        [SerializeField] private int currentCalculatorOperationAndValueCount;
        [SerializeField] private int result;
        public CalculatorOperator[] CurrentOperators => currentOperators;
        public int[] NumbersInBox => numbersInBox;
        public int OriginalCalculatorOperationAndValueCount => originalCalculatorOperationAndValueCount;
        public int CurrentCalculatorOperationAndValueCount => currentCalculatorOperationAndValueCount;
        public int Result => result;

        #region Life cycle

        protected override void Initialize()
        {
            base.Initialize();
            SubscribeEvent();
        }
        
        private IDisposable _disposable;
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            DisposableBagBuilder bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ButtonOnClick>().Subscribe(GetButtonClick).AddTo(bag);
            _disposable = bag.Build();
        }

        public static void InitializeSystem(CalculatorSystemData data)
        {
            Instance.originalCalculatorOperationAndValueCount = data.CalculatorOperationAndValueCount;
            Instance.currentCalculatorOperationAndValueCount = data.CalculatorOperationAndValueCount;
            Instance.currentOperators = new CalculatorOperator[Instance.currentCalculatorOperationAndValueCount];
            Instance.numbersInBox = new int[Instance.currentCalculatorOperationAndValueCount];
        }

        #endregion
        
        #region Behaviour

        #region ArrayResize

        private void ArrayResize()
        {
            if (currentOperators == null || numbersInBox == null)
            {
                currentOperators = new CalculatorOperator[currentCalculatorOperationAndValueCount];
                numbersInBox = new int[currentCalculatorOperationAndValueCount];
                return;
            }
            
            if (currentCalculatorOperationAndValueCount > originalCalculatorOperationAndValueCount)
            {
                Array.Resize(ref currentOperators, currentCalculatorOperationAndValueCount);
                Array.Resize(ref numbersInBox, currentCalculatorOperationAndValueCount);

            }
            else if(originalCalculatorOperationAndValueCount > currentCalculatorOperationAndValueCount)
            {
                Array.Resize(ref currentOperators, originalCalculatorOperationAndValueCount);
                Array.Resize(ref numbersInBox, originalCalculatorOperationAndValueCount);
            }
        }

        #endregion

        #region ButtonClick

        private void GetButtonClick(ButtonOnClick data)
        {
            var button = CalculatorButtonManager.GetButtonByIndex(data.Index);
            
            if (button == null)
            {
                Debug.Log("Button data is null for index: " + data.Index);
                return;
            }
            
            TriggerWithButtonType(button);
        }
        
        private void TriggerWithButtonType(CalculatorButton button)
        {
            switch (button.CalculatorButtonType)
            {
                case CalculatorButtonType.NumberActivate:
                    DetectNumberInBox(button.CurrentValue);
                    break;
                case CalculatorButtonType.Operator:
                    DetectOperatorInBox(button.CalculatorOperator);
                    break;
                case CalculatorButtonType.Feature:
                    DetectFeature(button.CalculatorFeature);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (button.CalculatorFeature == CalculatorFeature.Equal)return;
            
            CalculatorNotify data = new CalculatorNotify(currentOperators, numbersInBox);
            GlobalMessagePipe.GetPublisher<CalculatorNotify>().Publish(data);
        }


        #region Detect

        private void DetectNumberInBox(int value)
        {
            int length = currentCalculatorOperationAndValueCount;

            if (length <= 1)
            {
                numbersInBox[0] = value;
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    if (numbersInBox[i] > 0)
                    {
                        if (i + 1 >= length)
                        {
                            numbersInBox[i] = value;
                        }
                        continue;
                    }
                    numbersInBox[i] = value;
                    return;
                }
            }
        }
        
        private void DetectOperatorInBox(in CalculatorOperator calculatorOperator)
        {
            int length = currentCalculatorOperationAndValueCount;
            
            if (length <= 1)
            {
                currentOperators[0] = calculatorOperator;
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    if (currentOperators[i] != CalculatorOperator.None)
                    {
                        if (i + 1 >= length)
                        {
                            currentOperators[i] = calculatorOperator;
                        }
                        continue;
                    }
                    currentOperators[i] = calculatorOperator;
                    return;
                }
            }
        }

        private void DetectFeature(in CalculatorFeature calculatorFeature)
        {
            switch (calculatorFeature)
            {
                case CalculatorFeature.DelOperator:
                    DeleteOperator();
                    break;
                case CalculatorFeature.DelNumber:
                    DeleteNumber();
                    break;
                case CalculatorFeature.Equal:
                    CalculateResult();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(calculatorFeature), calculatorFeature, null);
            }
        }

        #endregion

        #region Feature

        private void DeleteOperator()
        {
            if (currentCalculatorOperationAndValueCount == 1)
            {
                currentOperators[currentCalculatorOperationAndValueCount-1] = CalculatorOperator.None;
                return;
            }
            
            int arrayIndex = currentCalculatorOperationAndValueCount-1;
            
            for (int i = arrayIndex; i >= 0; i--)
            {
                if (currentOperators[i] == CalculatorOperator.None) continue;
                currentOperators[i] = CalculatorOperator.None;
                return;
            }
        }
        
        private void DeleteNumber()
        {
            if (currentCalculatorOperationAndValueCount == 1)
            {
                numbersInBox[currentCalculatorOperationAndValueCount - 1] = 0;
                return;
            }
            
            int arrayIndex = currentCalculatorOperationAndValueCount-1;
            
            for (int i = arrayIndex; i >= 0; i--)
            {
                if (numbersInBox[i] <= 0) continue;
                numbersInBox[i] = 0;
                return;
            }
        }

        #region Calculate Result

        private void CalculateResult()
        {
            if (DetectAllBoxFilled())
            {
                // 發一個事件，顯示沒有放滿的UI
                Debug.Log("Cannot calculate result because not all boxes are filled");
                return;
            }

            if (currentCalculatorOperationAndValueCount <= 1)
            {
                result = numbersInBox[0];
            }
            else
            {
                result = CalculateMultiNumber();
            }
            
            CalculatorResultNotify data = new CalculatorResultNotify(result,currentOperators[0]);
            GlobalMessagePipe.GetPublisher<CalculatorResultNotify>().Publish(data);
            
            ResetBox();
        }

        private bool DetectAllBoxFilled()
        {
            int length = currentCalculatorOperationAndValueCount;
            
            for (int i = 0; i < length; i++)
            {
                if (numbersInBox[i] <= 0 || currentOperators[i] == CalculatorOperator.None)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        private int CalculateMultiNumber()
        {
            int result = 0;
            
            List<int> recordSkipNumber = new List<int>();
            
            CalculateMultiplyAndDivide(recordSkipNumber);
            result = CalculateAddAndSubtract(recordSkipNumber);

            return result;
        }

        private void CalculateMultiplyAndDivide(in List<int> recordSkipNumber)
        {
            int arrayIndex = currentCalculatorOperationAndValueCount-1;
            
            for (int i = 1; i <= arrayIndex; i++)
            {
                CalculatorOperator op = currentOperators[i];
                
                if (op != CalculatorOperator.Multiply && op != CalculatorOperator.Divide) 
                    continue;
                
                int leftIndex = GetPreviousValidIndex(i - 1, recordSkipNumber);
                
                if(leftIndex == 0) Debug.Log("Left index is 0, which means no valid operator found for index: " + i);

                if (op == CalculatorOperator.Multiply)
                {
                    numbersInBox[leftIndex] *= numbersInBox[i];
                }
                else
                {
                    numbersInBox[leftIndex] /= numbersInBox[i];
                }

                recordSkipNumber.Add(i);
            }
        }
        
        private int GetPreviousValidIndex(int startIndex, List<int> skippedIndices)
        {
            for (int j = startIndex; j >= 0; j--)
            {
                if (!skippedIndices.Contains(j))
                    return j;
            }
            return 0; 
        }
        
        private int CalculateAddAndSubtract(in List<int> recordSkipNumber)
        {
            int arrayIndex = currentCalculatorOperationAndValueCount-1;
            int result = numbersInBox[0];

            for (int i = 1; i <= arrayIndex; i++)
            {
                if (recordSkipNumber.Contains(i)) continue;
                
                if (currentOperators[i] == CalculatorOperator.Add)
                {
                    result += numbersInBox[i];
                }
                else if (currentOperators[i] == CalculatorOperator.Subtract)
                {
                    result -= numbersInBox[i];
                }
            }
            
            return result;
        }
        
        private void ResetBox()
        {
            int length = currentCalculatorOperationAndValueCount;
            
            for (int i = 0; i < length; i++)
            {
                currentOperators[i] = CalculatorOperator.None;
                numbersInBox[i] = 0;
            }
        }

        #endregion

        #endregion
        
        #endregion

        #endregion

        #region Unit Test Feature

        public void SetCurrentOperators(CalculatorOperator[] operators)
        {
            currentOperators = operators;
        }
        
        public void SetNumbersInBox(int[] numbers)
        {
            numbersInBox = numbers;
        }
        
        public void SetCurrentCalculatorOperationAndValueCount(int count)
        {
            currentCalculatorOperationAndValueCount = count;
            ArrayResize();
        }

        public void SetDeleteNumber()
        {
            DeleteNumber();
        }
        
        public void SetDeleteOperator()
        {
            DeleteOperator();
        }
        
        public int SetEqualTest()
        {
            return CalculateMultiNumber();
        }

        #endregion
    }
}