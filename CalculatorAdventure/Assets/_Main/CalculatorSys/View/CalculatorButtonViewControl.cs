using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Sys;
using _Main.CalculatorSys.Sys.EventData;
using _Main.CalculatorSys.Sys.Runtime;
using BolingsUnityTools;
using MessagePipe;
using ToolKit;
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

        public static void InitializeView(List<CalculatorButton> data)
        {
            Instance.CalculatorButtonViews = new Dictionary<byte, CalculatorButtonView>();
            foreach (var buttonView in Instance._calculatorButtonViews)
            {
                Instance.CalculatorButtonViews.Add(buttonView.index, buttonView);
            }
            
            foreach (var calculatorButton in data)
            {
                if (Instance.CalculatorButtonViews.ContainsKey(calculatorButton.Index))
                {
                    Instance.CalculatorButtonViews[calculatorButton.Index].Initialize(calculatorButton);
                }
            }
        }
        
        
        private IDisposable _disposable;
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            DisposableBagBuilder bag = DisposableBag.CreateBuilder();
            _disposable = bag.Build();
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _disposable?.Dispose();
        }

        #endregion
    }
}