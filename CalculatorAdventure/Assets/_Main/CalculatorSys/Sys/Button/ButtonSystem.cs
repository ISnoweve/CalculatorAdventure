using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.View.UI_CalculatorButton.Control;
using _Main.CalculatorSys.View.UI_CalculatorButton.Event;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.Sys.Button
{
    [Serializable]
    public class ButtonSystem : Singleton<ButtonSystem>
    {
        [SerializeField] private List<byte> recordUsedNumberIndex;

        #region Lify cycle

        protected override void Initialize()
        {
            base.Initialize();
            recordUsedNumberIndex = new List<byte>();
            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ButtonOnClick>().Subscribe(DetectButtonClickAble).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion

        #region Behaviour

        private void DetectButtonClickAble(ButtonOnClick data)
        {
            var button = CalculatorButtonManager.GetButtonByIndex(data.Index);

            if (DetectIsNumberButton(button)) RecordUsedNumberIndex(button.Index);
            button.ClickButton();
            var buttonClickSuccess = new ButtonClickSuccess(button.Index);
            GlobalMessagePipe.GetPublisher<ButtonClickSuccess>().Publish(buttonClickSuccess);

            DetectAllNumberButtonClickAble();
        }

        private bool DetectIsNumberButton(CalculatorButton button)
        {
            if (button.CalculatorButtonType != CalculatorButtonType.NumberActivate) return false;
            return true;
        }

        private void RecordUsedNumberIndex(byte index)
        {
            recordUsedNumberIndex.Add(index);
        }

        private bool DetectAllNumberButtonClickAble()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                if (calculatorButton.IsClick == false)
                    return false;

            RecoverAllNumberButtonClickAble();
            return true;
        }

        private void RecoverAllNumberButtonClickAble()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                calculatorButton.RecoverButtonClickAble();
            ResetRecordUsedNumberIndex();

            var buttonClickRecover = new AllNumberButtonClickRecover();
            GlobalMessagePipe.GetPublisher<AllNumberButtonClickRecover>().Publish(buttonClickRecover);
        }

        private void ResetRecordUsedNumberIndex()
        {
            recordUsedNumberIndex.Clear();
        }

        #endregion

        #region API Feature

        public static void RecoverNumberButtonByIndex(byte index)
        {
            if (index <= 0) return;

            var detectResult = Instance.DetectOldCalculatorNumber(index);

            if (detectResult)
            {
                Instance.recordUsedNumberIndex.Remove(index);
                var calculatorButton = CalculatorButtonManager.GetButtonByIndex(index);
                calculatorButton.RecoverButtonClickAble();
                var buttonClickRecover = new ButtonSetClickRecover(calculatorButton.Index);
                GlobalMessagePipe.GetPublisher<ButtonSetClickRecover>().Publish(buttonClickRecover);
            }
            else
            {
                foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                {
                    if (calculatorButton.IsClick &&
                        calculatorButton.CalculatorButtonType != CalculatorButtonType.NumberNotActivate) continue;
                    calculatorButton.CloseButtonClickAble();
                    Instance.recordUsedNumberIndex.Add(calculatorButton.Index);
                }

                var recoverButton = CalculatorButtonManager.GetButtonByIndex(index);
                recoverButton.RecoverButtonClickAble();
                Instance.recordUsedNumberIndex.Remove(recoverButton.Index);

                var buttonRecoverOldNumber = new ButtonRecoverOldNumber(index, Instance.recordUsedNumberIndex);
                GlobalMessagePipe.GetPublisher<ButtonRecoverOldNumber>().Publish(buttonRecoverOldNumber);
            }
        }

        private bool DetectOldCalculatorNumber(byte index)
        {
            return recordUsedNumberIndex.Contains(index);
        }

        public static void CloseNumberButtonClickableByAttackSkill(List<CalculatorButton> takeButtons)
        {
            foreach (var calculatorButton in takeButtons)
            {
                calculatorButton.CloseButtonClickAble();
                Instance.RecordUsedNumberIndex(calculatorButton.Index);
            }

            //之後要改
            if (Instance.DetectAllNumberButtonClickAble())
            {
                var buttonClickRecover = new AllNumberButtonClickRecover();
                GlobalMessagePipe.GetPublisher<AllNumberButtonClickRecover>().Publish(buttonClickRecover);
            }
            else
            {
                CalculatorButtonViewControl.Instance.UpdateButtonCloseClick(takeButtons);
            }
        }

        public static void ModifyNumberButtonValueByAttackSkill(List<CalculatorButton> takeButtons, int adjustValue)
        {
            foreach (var calculatorButton in takeButtons) calculatorButton.ModifyCurrentValue(adjustValue);

            var buttonValueModify = new ButtonValueModify(takeButtons);
            GlobalMessagePipe.GetPublisher<ButtonValueModify>().Publish(buttonValueModify);
        }

        public static void ResetNumberButtonToOriginalValue()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllActivateNumberButton())
                calculatorButton.ResetCurrentValue();

            var calculatorButtons = CalculatorButtonManager.GetAllActivateNumberButton();
            var buttonValueModify = new ButtonValueModify(calculatorButtons);
            GlobalMessagePipe.GetPublisher<ButtonValueModify>().Publish(buttonValueModify);
        }

        public static void ResetRecordUsedNumberIndexWhenGameStart()
        {
            Instance.recordUsedNumberIndex.Clear();
        }

        public static void SetOperatorButtonClickAble(CalculatorOperator calculatorOperator)
        {
            switch (calculatorOperator)
            {
                case CalculatorOperator.Multiply:
                    CalculatorButtonManager.GetMultiplyButton().SetButtonClickAble();
                    var multiplyButtonClickRecover =
                        new SetOperatorButton(CalculatorButtonManager.GetMultiplyButton().Index);
                    GlobalMessagePipe.GetPublisher<SetOperatorButton>().Publish(multiplyButtonClickRecover);
                    break;
                case CalculatorOperator.Divide:
                    CalculatorButtonManager.GetDivideButton().SetButtonClickAble();
                    var divideButtonSetClickRecover =
                        new SetOperatorButton(CalculatorButtonManager.GetDivideButton().Index);
                    GlobalMessagePipe.GetPublisher<SetOperatorButton>().Publish(divideButtonSetClickRecover);
                    break;
            }
        }

        #endregion
    }
}