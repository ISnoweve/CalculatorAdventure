using System;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.View.EventData;
using BolingsUnityTools;
using MessagePipe;

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
            DisposableBagBuilder bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ButtonOnClick>().Subscribe(DetectButtonClickAble).AddTo(bag);
            _disposable = bag.Build();
        }


        #endregion 
        
        private void DetectButtonClickAble(ButtonOnClick data)
        {
            CalculatorButton button = CalculatorButtonManager.GetButtonByIndex(data.Index);

            if (button.CheckIsClickAble())
            {
                ButtonUpdateSuccess buttonUpdateSuccess = new ButtonUpdateSuccess(button);
                GlobalMessagePipe.GetPublisher<ButtonUpdateSuccess>().Publish(buttonUpdateSuccess);
            }
            else
            {
                ButtonUpdateFail buttonUpdateFail = new ButtonUpdateFail(button);
                GlobalMessagePipe.GetPublisher<ButtonUpdateFail>().Publish(buttonUpdateFail);
            }
            
            DetectAllButtonClickAble();
        }

        private void DetectAllButtonClickAble()
        {
            foreach (CalculatorButton calculatorButton in CalculatorButtonManager.GetAllButton())
            {
                if(!calculatorButton.IsClick)return;
            }
        }
        
        private void RecoverAllButtonClickAble()
        {
            
        }
    }
}