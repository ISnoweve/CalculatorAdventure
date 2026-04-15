using System;
using _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardSlider
{
    public class UIMobBattleRewardView : SingletonMonoBehaviour<UIMobBattleRewardView>
    {
        [SerializeField] private Slider rewardSlider;
        [SerializeField] private TMP_Text moneyRewardValueText, uniqueItemRewardValueText;
        
        #region Life Cycle
        
        protected override void Awake()
        {
            base.Awake();
            SubscribeEvent();
            ResetView();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<UpdateRewardRoundAndValue>().Subscribe(UpdateSliderInfo).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }
        
        #endregion

        private void UpdateSliderInfo(UpdateRewardRoundAndValue data)
        {
            if (data.IsBossRound)
            {
                IsBossBattle();
                moneyRewardValueText.text = $"{0:F2}";
                uniqueItemRewardValueText.text = $"{100:F2}";
                return;
            }
            
            rewardSlider.value = data.RewardMoneyValue;
            moneyRewardValueText.text = $"{data.RewardMoneyValue:F2}";
            uniqueItemRewardValueText.text = $"{data.RewardUniqueItemValue:F2}";
        }

        private void ResetView()
        {
            rewardSlider.fillRect.gameObject.SetActive(true);
        }

        private void IsBossBattle()
        {
            rewardSlider.fillRect.gameObject.SetActive(false);
        }
    }
}