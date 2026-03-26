using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager.Event;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.View.UI_CalculatorButton.Runtime;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.CalculatorSys.View.UI_CalculatorButton.Control
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

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ButtonsSpawn>().Subscribe(InitializeView).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonClickSuccess>().Subscribe(UpdateButtonClickView).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<AllNumberButtonClickRecover>().Subscribe(UpdateAllButtonRecover).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<SetOperatorButton>().Subscribe(UpdateOperatorButtonRecover).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonSetClickRecover>().Subscribe(UpdateButtonRecover).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonRecoverOldNumber>().Subscribe(UpdateOldButton).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonValueModify>().Subscribe(UpdateButtonValueModify).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion

        #region Behavior

        private static void InitializeView(ButtonsSpawn data)
        {
            Instance.CalculatorButtonViews = new Dictionary<byte, CalculatorButtonView>();
            foreach (var buttonView in Instance._calculatorButtonViews)
                Instance.CalculatorButtonViews.Add(buttonView.index, buttonView);

            foreach (var calculatorButton in data.Buttons)
                if (Instance.CalculatorButtonViews.ContainsKey(calculatorButton.Index))
                    Instance.CalculatorButtonViews[calculatorButton.Index].Initialize(calculatorButton);
        }

        #region GetView

        public void GetButtonViewByIndex(byte index, out CalculatorButtonView buttonView)
        {
            buttonView = null;
            if (CalculatorButtonViews.ContainsKey(index))
                buttonView = CalculatorButtonViews[index];
        }

        #endregion

        #region Click

        private void UpdateButtonClickView(ButtonClickSuccess data)
        {
            if (CalculatorButtonViews.ContainsKey(data.ButtonIndex))
                CalculatorButtonViews[data.ButtonIndex].ChangeButtonState(false);

            if (CalculatorButtonViews[data.ButtonIndex].CalculatorButtonType == CalculatorButtonType.Operator)
                CalculatorButtonViews[data.ButtonIndex].ChangeOperatorButtonState(false);
        }

        #endregion

        #region Recover

        private void UpdateAllButtonRecover(AllNumberButtonClickRecover data)
        {
            foreach (var calculatorButtonView in _calculatorButtonViews) calculatorButtonView.ChangeButtonState(true);
        }

        private void UpdateButtonRecover(ButtonSetClickRecover data)
        {
            if (CalculatorButtonViews.ContainsKey(data.ButtonIndex))
                CalculatorButtonViews[data.ButtonIndex].ChangeButtonState(true);
        }

        private void UpdateOperatorButtonRecover(SetOperatorButton data)
        {
            if (CalculatorButtonViews.ContainsKey(data.ButtonIndex))
                CalculatorButtonViews[data.ButtonIndex].ChangeOperatorButtonState(true);
        }


        private void UpdateOldButton(ButtonRecoverOldNumber data)
        {
            foreach (var variable in data.LockedButtonIndexes)
                if (CalculatorButtonViews.ContainsKey(variable))
                    CalculatorButtonViews[variable].ChangeButtonState(false);

            if (CalculatorButtonViews.ContainsKey(data.ButtonIndexes))
                CalculatorButtonViews[data.ButtonIndexes].ChangeButtonState(true);
        }

        #endregion

        #region Close

        public void UpdateButtonCloseClick(List<CalculatorButton> buttonIndexes)
        {
            foreach (var buttonIndex in buttonIndexes)
                if (CalculatorButtonViews.ContainsKey(buttonIndex.Index))
                    CalculatorButtonViews[buttonIndex.Index].ChangeButtonState(false);
        }

        #endregion

        #region Value

        private void UpdateButtonValueModify(ButtonValueModify data)
        {
            foreach (var calculatorButton in data.Buttons)
                if (CalculatorButtonViews.ContainsKey(calculatorButton.Index))
                    CalculatorButtonViews[calculatorButton.Index].ChangeTextByValue(calculatorButton.CurrentValue);
        }

        #endregion

        #region Test

        [Button]
        private void UpdateButtonOpenClick()
        {
            foreach (var calculatorButtonView in _calculatorButtonViews) calculatorButtonView.ChangeButtonState(false);
        }

        #endregion

        #endregion
    }
}