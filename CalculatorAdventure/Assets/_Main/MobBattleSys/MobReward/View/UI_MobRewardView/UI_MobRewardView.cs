using System;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event;
using _Main.MobSys.Manager.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView
{
    public class UI_MobRewardView : SingletonMonoBehaviour<UI_MobRewardView>
    {
        [SerializeField] private Button moneyRewardView, uniqueItemRewardViewOne, uniqueItemRewardViewTwo;
        
        #region Life Cycle

        protected override void Awake()
        {
            base.Awake();
            SubscribeEvent(); 
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            //GlobalMessagePipe.GetSubscriber<OutPutMoneyReward>()
            //GlobalMessagePipe.GetSubscriber<OutPutUniqueItemReward>()
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion
        
        private void OutPutMoneyReward(OutPutMoneyReward data)
        {
            moneyRewardView.gameObject.SetActive(true);
        }


        private void OutPutUniqueItemReward(OutPutUniqueItemReward data)
        {
            uniqueItemRewardViewOne.gameObject.SetActive(true);
            uniqueItemRewardViewTwo.gameObject.SetActive(true);
        }
    }
}