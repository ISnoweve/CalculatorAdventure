using System;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using _Main.MoneySys.Data.Enum;
using _Main.MoneySys.Sys.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine;

namespace _Main.MoneySys.Sys
{
    [Serializable]
    public class MoneySystem : Singleton<MoneySystem>
    {
        [SerializeField] private int moneyValue;
        
        #region Life cycle

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
            GlobalMessagePipe.GetSubscriber<ChooseMoneyReward>().Subscribe(ModifyMoneyValueAfterChooseMoneyReward).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion
        
        private void ModifyMoneyValueAfterChooseMoneyReward(ChooseMoneyReward data)
        {
            moneyValue += data.RewardValue;

            ModifyMoney modifyMoney = new ModifyMoney(data.RewardValue, MoneyModifyType.Add);
            GlobalMessagePipe.GetPublisher<ModifyMoney>().Publish(modifyMoney);
        }
    }
}