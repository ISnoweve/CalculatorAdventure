using System;
using System.Collections.Generic;
using _Main.CalculatorSys.View.UI_CalculatorButton.Runtime;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.View.UI_SetCalculatorNumberView.Control
{
    public class SetCalculatorNumberViewControl : SingletonMonoBehaviour<SetCalculatorNumberViewControl>
    {
        [SerializeField] private List<CalculatorButtonView> _calculatorButtonViews;
        private Dictionary<byte, CalculatorButtonView> CalculatorButtonViews;
        
        #region Life Cycle

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
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion
    }
}