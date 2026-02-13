using System;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.View.EventData;
using BolingsUnityTools;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.Sys
{
    [Serializable]
    public class CalculatorSystem : Singleton<CalculatorSystem>
    {
        [SerializeField] private CalculatorOperator[] currentOperators;
        [SerializeField] private int[] numbersInBox;
        [SerializeField] private CalculatorButtonsData calculatorButtonsData;
        // 備註: 需要在初始化的時候去抓資料。 
        
        private IDisposable _disposable;
        protected override void Initialize()
        {
            base.Initialize();
        }

        public void Test()
        {
            SubscribeEvent();
        }
        
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            DisposableBagBuilder bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ButtonOnClick>().Subscribe(GetButtonNotify).AddTo(bag);
            _disposable = bag.Build();
        }

        private void GetButtonNotify(ButtonOnClick data)
        {
            CalculatorButtonManager.Instance.GetButtonByIndex(data.Index);
        }

        protected override void Release()
        {
            base.Release();
        }
    }
}