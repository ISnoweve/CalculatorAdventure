using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.View.UI_CalculatorButton.Runtime;
using _Main.CalculatorSys.View.UI_TriggerByRewardPutNumber.Event;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data.EffectData.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.View.UI_TriggerByRewardPutNumber
{
    public class CalculatorPutNumber : SingletonMonoBehaviour<CalculatorPutNumber>
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private List<CalculatorButtonView> _calculatorButtonViews;
        
        #region Life cycle

        protected override void Awake()
        {
            SubscribeEvent();
            Reset();
            base.Awake();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<Event_GiveCalculatorNumber>().Subscribe(ShowIncreaseCalculatorButtonNumber).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_PutNumber>().Subscribe(HideAllCalculatorButtonNumber).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        private void Reset()
        {
            panel.SetActive(false);
            foreach (var calculatorButtonView in _calculatorButtonViews)
            {
                calculatorButtonView.gameObject.SetActive(false);
            }
        }

        #endregion
        
        #region UniqueItem Put Number In Button
        
        private void ShowIncreaseCalculatorButtonNumber(Event_GiveCalculatorNumber data)
        {
            panel.SetActive(true);
            foreach (var calculatorButtonView in _calculatorButtonViews)
            {
                calculatorButtonView.gameObject.SetActive(true);
            }
            foreach (CalculatorButton calculatorButton in CalculatorButtonManager.GetAllNumberButton())
            {
                foreach (var calculatorButtonView in _calculatorButtonViews)
                {
                    if(calculatorButtonView.index == calculatorButton.Index)
                    {
                        if(calculatorButton.CalculatorButtonType == CalculatorButtonType.NumberNotActivate)
                        {
                            calculatorButtonView.ButtonText.text = "";
                            calculatorButtonView.Button.interactable = true;
                        }
                        else
                        {
                            calculatorButtonView.ButtonText.text = calculatorButton.CurrentValue.ToString();
                            calculatorButtonView.Button.interactable = false;
                        }
                    }
                }
            }
        }
        
        private void HideAllCalculatorButtonNumber(Event_PutNumber data)
        {
            panel.SetActive(false);
            foreach (var calculatorButtonView in _calculatorButtonViews)
            {
                calculatorButtonView.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}