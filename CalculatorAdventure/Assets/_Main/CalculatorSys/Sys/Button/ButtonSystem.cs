using System;
using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.View.EventData;
using BolingsUnityTools;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.Sys.Button
{
    [Serializable]
    public class ButtonSystem : Singleton<ButtonSystem>
    {
        #region Lify cycle

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
            GlobalMessagePipe.GetSubscriber<ButtonOnClick>().Subscribe(DetectButtonClickAble).AddTo(bag);
            _disposable = bag.Build();
        }

        #endregion

        #region Behaviour

        private void DetectButtonClickAble(ButtonOnClick data)
        {
            var button = CalculatorButtonManager.GetButtonByIndex(data.Index);

            button.ClickButton();
            var buttonClickSuccess = new ButtonClickSuccess(button);
            GlobalMessagePipe.GetPublisher<ButtonClickSuccess>().Publish(buttonClickSuccess);

            DetectAllButtonClickAble();
        }

        private void DetectAllButtonClickAble()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                if (!calculatorButton.CheckIsClickAble())
                    return;

            RecoverAllButtonClickAble();
        }

        private void RecoverAllButtonClickAble()
        {
            foreach (var calculatorButton in CalculatorButtonManager.GetAllNumberButton())
                calculatorButton.RecoverButtonClickAble();

            var buttonClickRecover =
                new AllButtonClickRecover(CalculatorButtonManager.GetAllNumberButton());
            GlobalMessagePipe.GetPublisher<AllButtonClickRecover>().Publish(buttonClickRecover);
        }
        
        public static void RecoverNumberButtonByIndex(byte index)
        {
            if(index <= 0) return;
            
            var calculatorButton = CalculatorButtonManager.GetButtonByIndex(index); 
            calculatorButton.RecoverButtonClickAble();
            ButtonClickRecover buttonClickRecover = new ButtonClickRecover(calculatorButton);
            GlobalMessagePipe.GetPublisher<ButtonClickRecover>().Publish(buttonClickRecover);
        }

        #endregion
    }
}