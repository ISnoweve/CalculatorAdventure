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
            CalculatorButton button = CalculatorButtonManager.GetButtonByIndex(data.Index);

            if (DetectNumber(button))
            {
                RecordUsedNumberIndex(button.Index);
            }
            button.ClickButton();
            var buttonClickSuccess = new ButtonClickSuccess(button.Index);
            GlobalMessagePipe.GetPublisher<ButtonClickSuccess>().Publish(buttonClickSuccess);

            DetectAllButtonClickAble();
        }

        private bool DetectNumber(CalculatorButton button)
        {
            if(button.CalculatorButtonType != CalculatorButtonType.NumberActivate) return false;
            return true;
        }
        
        private void RecordUsedNumberIndex(byte index)
        {
            recordUsedNumberIndex.Add(index);
        }

        private bool DetectAllButtonClickAble()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                if (calculatorButton.IsClick == false)
                    return false;
            
            RecoverAllButtonClickAble();
            return true;
        }

        private void RecoverAllButtonClickAble()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                calculatorButton.RecoverButtonClickAble();
            ResetRecordUsedNumberIndex();
            
            var buttonClickRecover = new AllButtonClickRecover();
            GlobalMessagePipe.GetPublisher<AllButtonClickRecover>().Publish(buttonClickRecover);
        }
        
        private void ResetRecordUsedNumberIndex()
        {
            recordUsedNumberIndex.Clear();
        }
        
        private bool DetectOldCalculatorNumber(byte index)
        {
            return recordUsedNumberIndex.Contains(index);
        }
        
        public static void RecoverNumberButtonByIndex(byte index)
        {
            if(index <= 0) return;

            bool detectResult = Instance.DetectOldCalculatorNumber(index);

            if (detectResult)
            {
                Instance.recordUsedNumberIndex.Remove(index);
                var calculatorButton = CalculatorButtonManager.GetButtonByIndex(index); 
                calculatorButton.RecoverButtonClickAble();
                ButtonClickRecover buttonClickRecover = new ButtonClickRecover(calculatorButton.Index);
                GlobalMessagePipe.GetPublisher<ButtonClickRecover>().Publish(buttonClickRecover);
            }
            else
            {
                foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                {
                    if(calculatorButton.IsClick&&
                       calculatorButton.CalculatorButtonType != CalculatorButtonType.NumberNotActivate)continue;
                    calculatorButton.CloseButtonClickAble();
                    Instance.recordUsedNumberIndex.Add(calculatorButton.Index);
                }
                
                var recoverButton = CalculatorButtonManager.GetButtonByIndex(index); 
                recoverButton.RecoverButtonClickAble();
                Instance.recordUsedNumberIndex.Remove(recoverButton.Index);
                
                ButtonRecoverOldNumber buttonRecoverOldNumber = new ButtonRecoverOldNumber(index, Instance.recordUsedNumberIndex);
                GlobalMessagePipe.GetPublisher<ButtonRecoverOldNumber>().Publish(buttonRecoverOldNumber);
            }
        }

        public static void CloseButtonClickableByAttackSkill(List<CalculatorButton> takeButtons)
        {
            foreach (var calculatorButton in takeButtons)
            {
                calculatorButton.CloseButtonClickAble();
                Instance.RecordUsedNumberIndex(calculatorButton.Index);
            }

            //之後要改
            if (Instance.DetectAllButtonClickAble())
            {
                var buttonClickRecover = new AllButtonClickRecover();
                GlobalMessagePipe.GetPublisher<AllButtonClickRecover>().Publish(buttonClickRecover);
            }
            else
            {
                CalculatorButtonViewControl.Instance.UpdateButtonCloseClick(takeButtons);
            }
        }

        #endregion
    }
}