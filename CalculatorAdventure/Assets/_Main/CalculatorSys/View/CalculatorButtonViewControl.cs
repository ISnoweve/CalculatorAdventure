using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Event;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.CalculatorSys.View
{
    public class CalculatorButtonViewControl : SingletonMonoBehaviour<CalculatorButtonViewControl>
    {
        [SerializeField] private List<CalculatorButtonView> _calculatorButtonViews;
        private Dictionary<byte, CalculatorButtonView> CalculatorButtonViews;

        #region Life cycle

        protected override void Awake()
        {
            SubscribeEvent();
            base.Awake();
        }

        private static void InitializeView(ButtonsSpawn data)
        {
            Instance.CalculatorButtonViews = new Dictionary<byte, CalculatorButtonView>();
            foreach (var buttonView in Instance._calculatorButtonViews)
                Instance.CalculatorButtonViews.Add(buttonView.index, buttonView);

            foreach (var calculatorButton in data.Buttons)
                if (Instance.CalculatorButtonViews.ContainsKey(calculatorButton.Index))
                    Instance.CalculatorButtonViews[calculatorButton.Index].Initialize(calculatorButton);
        }


        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ButtonsSpawn>().Subscribe(InitializeView).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonClickSuccess>().Subscribe(UpdateButtonClickView).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<AllButtonClickRecover>().Subscribe(UpdateAllButtonRecover).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonClickRecover>().Subscribe(UpdateButtonRecover).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<CalculatorNotifyIsLastNumberAfterRecover>().Subscribe(UpdateButtonCloseClick).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonRecoverOldNumber>().Subscribe(UpdateOldButton).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _disposable?.Dispose();
        }

        #endregion

        #region Behavior

        #region Click

        private void UpdateButtonClickView(ButtonClickSuccess data)
        {
            if (CalculatorButtonViews.ContainsKey(data.ButtonIndex))
                CalculatorButtonViews[data.ButtonIndex].ChangeButtonState(false);
        }

        #endregion

        #region Recover

        private void UpdateAllButtonRecover(AllButtonClickRecover data)
        {
            foreach (var calculatorButtonView in _calculatorButtonViews)
            {
                calculatorButtonView.ChangeButtonState(true);
            }
        }
        
        private void UpdateButtonRecover(ButtonClickRecover data)
        {
            if (CalculatorButtonViews.ContainsKey(data.ButtonIndex))
                CalculatorButtonViews[data.ButtonIndex].ChangeButtonState(true);
        }

        
        private void UpdateOldButton(ButtonRecoverOldNumber data)
        {
            foreach (var variable in data.LockedButtonIndexes)
            {
                if (CalculatorButtonViews.ContainsKey(variable))
                    CalculatorButtonViews[variable].ChangeButtonState(false);
            }
            
            if (CalculatorButtonViews.ContainsKey(data.ButtonIndexes))
                CalculatorButtonViews[data.ButtonIndexes].ChangeButtonState(true);
        }
        #endregion

        #region Close

        private void UpdateButtonCloseClick(CalculatorNotifyIsLastNumberAfterRecover data)
        {
            foreach (var calculatorButtonView in _calculatorButtonViews)
            {
                calculatorButtonView.ChangeButtonState(false);
            }
            
            Debug.Log("Close Because Detect Last Number After Recover");
        }

        #endregion

        #region Test

        [Button]
        private void UpdateButtonOpenClick()
        {
            foreach (var calculatorButtonView in _calculatorButtonViews)
            {
                calculatorButtonView.ChangeButtonState(false);
            }
        }

        #endregion

        #endregion
    }
}