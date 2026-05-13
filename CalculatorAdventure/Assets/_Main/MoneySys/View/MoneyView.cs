using System;
using _Main.GameSceneSys.Sys.Event;
using _Main.MoneySys.Sys;
using _Main.MoneySys.Sys.Event;
using MessagePipe;
using TMPro;
using UnityEngine;

namespace _Main.MoneySys.View
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private int currentMoney;
        [SerializeField] private TMP_Text moneyText;

        private void Awake()
        {
            SubscribeEvent();
        }
        
        private IDisposable _disposable;
        
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<AfterSceneChange>().Subscribe(UpdateMoneyView).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ModifyMoney>().Subscribe(UpdateMoneyView).AddTo(bag);
            _disposable = bag.Build();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        private void UpdateMoneyView(AfterSceneChange eventData)
        {
            moneyText.text = MoneySystem.Instance.MoneyValue.ToString();
        }
        
        private void UpdateMoneyView(ModifyMoney eventData)
        {
            moneyText.text = eventData.ModifyValue.ToString();
        }

        public void UpdateMoneyValue(int value)
        {
            currentMoney = value;
            moneyText.text = currentMoney.ToString();
        }
    }
}