using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.Sys.Calculator.Enum;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.SnoweveToolKit.ToolKit;
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
        [SerializeField] private byte[] currentButtonIndexInBox;
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
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ButtonClickSuccess>().Subscribe(GetButtonClick).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<AllButtonClickRecover>().Subscribe(NotifyIsLastNumberInBox).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        public static void InitializeSystem(CalculatorSystemData data)
        {
            Instance.originalCalculatorOperationAndValueCount = data.CalculatorOperationAndValueCount;
            Instance.currentCalculatorOperationAndValueCount = data.CalculatorOperationAndValueCount;
            Instance.currentOperators = new CalculatorOperator[Instance.currentCalculatorOperationAndValueCount];
            Instance.numbersInBox = new int[Instance.currentCalculatorOperationAndValueCount];
            Instance.currentButtonIndexInBox = new byte[Instance.currentCalculatorOperationAndValueCount];
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
            else if (originalCalculatorOperationAndValueCount > currentCalculatorOperationAndValueCount)
            {
                Array.Resize(ref currentOperators, originalCalculatorOperationAndValueCount);
                Array.Resize(ref numbersInBox, originalCalculatorOperationAndValueCount);
            }
        }

        #endregion

        #region ButtonClick

        private void GetButtonClick(ButtonClickSuccess data)
        {
            if (data.ButtonIndex <= 0)
            {
                Debug.Log("Button data is null for index: " + data.ButtonIndex);
                return;
            }
            
            TriggerWithButtonType(CalculatorButtonManager.GetButtonByIndex(data.ButtonIndex));
        }

        private void TriggerWithButtonType(CalculatorButton button)
        {
            switch (button.CalculatorButtonType)
            {
                case CalculatorButtonType.NumberActivate:
                    PutButtonIndexInBox(button.Index);
                    PutNumberInBox(button.CurrentValue);
                    break;
                case CalculatorButtonType.Operator:
                    PutOperatorInBox(button.CalculatorOperator);
                    break;
                case CalculatorButtonType.Feature:
                    DetectFeature(button.CalculatorFeature);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        #region Detect

        private void PutButtonIndexInBox(byte index)
        {
            var length = currentCalculatorOperationAndValueCount;

            if (length <= 1)
            {
                ButtonSystem.RecoverNumberButtonByIndex(currentButtonIndexInBox[0]);
                currentButtonIndexInBox[0] = index;
            }
            else
                for (var i = 0; i < length; i++)
                {
                    if (currentButtonIndexInBox[i] > 0)
                    {
                        if (i + 1 >= length)
                        {
                            ButtonSystem.RecoverNumberButtonByIndex(currentButtonIndexInBox[i]);
                            currentButtonIndexInBox[i] = index;
                        }
                        continue;
                    }
                    ButtonSystem.RecoverNumberButtonByIndex(currentButtonIndexInBox[i]);
                    currentButtonIndexInBox[i] = index;
                    return;
                }
        }
        
        private void PutNumberInBox(int value)
        {
            var length = currentCalculatorOperationAndValueCount;

            if (length <= 1)
            {
                var data = new CalculatorNotify(currentOperators, numbersInBox);
                GlobalMessagePipe.GetPublisher<CalculatorNotify>().Publish(data);
                numbersInBox[0] = value;
            }
            else
                for (var i = 0; i < length; i++)
                {
                    if (numbersInBox[i] > 0)
                    {
                        if (i + 1 >= length) numbersInBox[i] = value;
                        continue;
                    }

                    numbersInBox[i] = value;
                    var data = new CalculatorNotify(currentOperators, numbersInBox);
                    GlobalMessagePipe.GetPublisher<CalculatorNotify>().Publish(data);
                    return;
                }
        }

        private void PutOperatorInBox(in CalculatorOperator calculatorOperator)
        {
            var length = currentCalculatorOperationAndValueCount;

            if (length <= 1)
            {
                currentOperators[0] = calculatorOperator;
                var data = new CalculatorNotify(currentOperators, numbersInBox);
                GlobalMessagePipe.GetPublisher<CalculatorNotify>().Publish(data);
            }
            else
                for (var i = 0; i < length; i++)
                {
                    if (currentOperators[i] != CalculatorOperator.None)
                    {
                        if (i + 1 >= length) currentOperators[i] = calculatorOperator;
                        continue;
                    }

                    currentOperators[i] = calculatorOperator;
                    var data = new CalculatorNotify(currentOperators, numbersInBox);
                    GlobalMessagePipe.GetPublisher<CalculatorNotify>().Publish(data);
                    return;
                }
        }

        private void DetectFeature(in CalculatorFeature calculatorFeature)
        {
            switch (calculatorFeature)
            {
                case CalculatorFeature.DelOperator:
                    DeleteOperator();
                    UpdateCalculatorNewInfo();
                    break;
                case CalculatorFeature.DelNumber:
                    RecoverNumberButton();
                    DeleteNumber();
                    UpdateCalculatorNewInfo();
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
            if (currentOperators[0] == 0)
            {
                CalculatorWarning warningData = new CalculatorWarning(CalculatorWarningEnum.OperatorIsEmpty);
                GlobalMessagePipe.GetPublisher<CalculatorWarning>().Publish(warningData);
                return;
            }
            
            if (currentCalculatorOperationAndValueCount == 1)
            {
                currentOperators[currentCalculatorOperationAndValueCount - 1] = CalculatorOperator.None;
                return;
            }

            var arrayIndex = currentCalculatorOperationAndValueCount - 1;

            for (var i = arrayIndex; i >= 0; i--)
            {
                if (currentOperators[i] == CalculatorOperator.None) continue;
                currentOperators[i] = CalculatorOperator.None;
                return;
            }
        }

        private void RecoverNumberButton()
        {
            if (currentCalculatorOperationAndValueCount == 1)
            {
                ButtonSystem.RecoverNumberButtonByIndex(currentButtonIndexInBox[0]);
                currentButtonIndexInBox[currentCalculatorOperationAndValueCount - 1] = 0;
                return;
            }

            var arrayIndex = currentCalculatorOperationAndValueCount - 1;

            for (var i = arrayIndex; i >= 0; i--)
            {
                if (currentButtonIndexInBox[i] <= 0) continue;
                ButtonSystem.RecoverNumberButtonByIndex(currentButtonIndexInBox[i]);
                currentButtonIndexInBox[i] = 0;
                return;
            }
        }
        
        private void DeleteNumber()
        {
            if (numbersInBox[0] == 0)
            {
                CalculatorWarning warningData = new CalculatorWarning(CalculatorWarningEnum.NumberIsEmpty);
                GlobalMessagePipe.GetPublisher<CalculatorWarning>().Publish(warningData);
                return;
            }
            
            if (currentCalculatorOperationAndValueCount == 1)
            {
                numbersInBox[currentCalculatorOperationAndValueCount - 1] = 0;
                return;
            }

            var arrayIndex = currentCalculatorOperationAndValueCount - 1;

            for (var i = arrayIndex; i >= 0; i--)
            {
                if (numbersInBox[i] <= 0) continue;
                numbersInBox[i] = 0;
                return;
            }
        }
        
        private void UpdateCalculatorNewInfo()
        {
            var data = new CalculatorNotify(currentOperators, numbersInBox);
            GlobalMessagePipe.GetPublisher<CalculatorNotify>().Publish(data);
        }

        #region Calculate Result

        private void CalculateResult()
        {
            if (DetectAllBoxFilled())
            {
                CalculatorWarning warningData = new CalculatorWarning(CalculatorWarningEnum.CantGiveResult);
                GlobalMessagePipe.GetPublisher<CalculatorWarning>().Publish(warningData);
                return;
            }

            if (currentCalculatorOperationAndValueCount <= 1)
                result = numbersInBox[0];
            else
                result = CalculateMultiNumber();

            var data = new CalculatorResultNotify(result, currentOperators[0]);
            GlobalMessagePipe.GetPublisher<CalculatorResultNotify>().Publish(data);

            ResetBox();
        }

        private bool DetectAllBoxFilled()
        {
            var length = currentCalculatorOperationAndValueCount;

            for (var i = 0; i < length; i++)
                if (numbersInBox[i] <= 0 || currentOperators[i] == CalculatorOperator.None)
                    return true;

            return false;
        }

        private int CalculateMultiNumber()
        {
            var result = 0;

            var recordSkipNumber = new List<int>();

            CalculateMultiplyAndDivide(recordSkipNumber);
            result = CalculateAddAndSubtract(recordSkipNumber);

            return result;
        }

        private void CalculateMultiplyAndDivide(in List<int> recordSkipNumber)
        {
            var arrayIndex = currentCalculatorOperationAndValueCount - 1;

            for (var i = 1; i <= arrayIndex; i++)
            {
                var op = currentOperators[i];

                if (op != CalculatorOperator.Multiply && op != CalculatorOperator.Divide)
                    continue;

                var leftIndex = GetPreviousValidIndex(i - 1, recordSkipNumber);

                if (leftIndex == 0) Debug.Log("Left index is 0, which means no valid operator found for index: " + i);

                if (op == CalculatorOperator.Multiply)
                    numbersInBox[leftIndex] *= numbersInBox[i];
                else
                    numbersInBox[leftIndex] /= numbersInBox[i];

                recordSkipNumber.Add(i);
            }
        }

        private int GetPreviousValidIndex(int startIndex, List<int> skippedIndices)
        {
            for (var j = startIndex; j >= 0; j--)
                if (!skippedIndices.Contains(j))
                    return j;
            return 0;
        }

        private int CalculateAddAndSubtract(in List<int> recordSkipNumber)
        {
            var arrayIndex = currentCalculatorOperationAndValueCount - 1;
            var result = numbersInBox[0];

            for (var i = 1; i <= arrayIndex; i++)
            {
                if (recordSkipNumber.Contains(i)) continue;

                if (currentOperators[i] == CalculatorOperator.Add)
                    result += numbersInBox[i];
                else if (currentOperators[i] == CalculatorOperator.Subtract) result -= numbersInBox[i];
            }

            return result;
        }

        private void ResetBox()
        {
            var length = currentCalculatorOperationAndValueCount;

            for (var i = 0; i < length; i++)
            {
                currentOperators[i] = CalculatorOperator.None;
                numbersInBox[i] = 0;
                currentButtonIndexInBox[i] = 0;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Detect Calculator NumbersInBox

        private void NotifyIsLastNumberInBox(AllButtonClickRecover data)
        {
            int lastNumberInBoxIndex = currentCalculatorOperationAndValueCount - 1;
            
            if(numbersInBox[lastNumberInBoxIndex] <= 0)return;
            CalculatorNotifyIsLastNumberAfterRecover notifyData = new CalculatorNotifyIsLastNumberAfterRecover();
            GlobalMessagePipe.GetPublisher<CalculatorNotifyIsLastNumberAfterRecover>().Publish(notifyData);
        }

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