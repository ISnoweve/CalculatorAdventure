using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Sys;
using _Main.CalculatorSys.Sys.EventData;
using BolingsUnityTools;
using MessagePipe;
using ToolKit;
using UnityEngine;

namespace _Main.CalculatorSys.View
{
    public sealed class CalculatorButtonViewControl : SingletonMonoBehaviour<CalculatorButtonViewControl>
    {
        [SerializeField] private List<CalculatorButtonView> _calculatorButtonViews;
        private Dictionary<byte, CalculatorButtonView> CalculatorButtonViews;
        
        #region Life cycle

        protected override void Awake()
        {
            Initialize();
            SubscribeEvent();
            CalculatorSystem.Instance.Test();
            base.Awake();
        }

        private void Initialize()
        {
            CalculatorButtonViews = new Dictionary<byte, CalculatorButtonView>();
            foreach (var buttonView in _calculatorButtonViews)
            {
                CalculatorButtonViews.Add(buttonView.index, buttonView);
            }
            
            foreach (var calculatorButton in CalculatorButtonManager.Instance.GetAllButtonData())
            {
                if (CalculatorButtonViews.ContainsKey(calculatorButton.Index))
                {
                    CalculatorButtonViews[calculatorButton.Index].Initialize(calculatorButton);
                }
            }
        }
        
        
        private IDisposable _disposable;
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            DisposableBagBuilder bag = DisposableBag.CreateBuilder();
            //GlobalMessagePipe.GetSubscriber<ButtonsSpawn>().Subscribe(InitializeButton).AddTo(bag);
            _disposable = bag.Build();
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _disposable?.Dispose();
        }

        #endregion

        #region Behaviour

        private void InitializeButton(ButtonsSpawn data)
        {
            foreach (var calculatorButton in data.Buttons)
            {
                if (CalculatorButtonViews.ContainsKey(calculatorButton.Index))
                {
                    CalculatorButtonViews[calculatorButton.Index].Initialize(calculatorButton);
                }
            }
        }

        #endregion
    }
}