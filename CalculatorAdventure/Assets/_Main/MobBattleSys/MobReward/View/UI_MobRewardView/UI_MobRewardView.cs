using System;
using _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView
{
    public class UI_MobRewardView : SingletonMonoBehaviour<UI_MobRewardView>
    {
        [SerializeField] private GameObject rewardPanel;
        [SerializeField] private GameObject moneyRewardCover;
        [SerializeField] private Button moneyRewardView;
        [SerializeField] private UI_UniqueRewardButton  uniqueItemRewardViewOne, uniqueItemRewardViewTwo;
        [SerializeField,ReadOnly] private int moneyRewardValue;
        [SerializeField,ReadOnly] private int uniqueItemRewardOneId, uniqueItemRewardTwoId;
        
        #region Life Cycle

        protected override void Awake()
        {
            base.Awake();
            SubscribeEvent(); 
            ResetView();
            SetButtonEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<OutPutMoneyReward>().Subscribe(OutPutMoneyReward).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<OutPutUniqueItemReward>().Subscribe(OutPutUniqueItemReward).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ChooseUniqueReward>().Subscribe(CloseUniqueRewardPanel).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            RemoveButtonEvent();
            base.OnDestroy();
        }

        #endregion
        
        private void SetButtonEvent()
        {
            moneyRewardView.onClick.AddListener(NotifyAfterChooseMoneyReward);
        }
        
        private void RemoveButtonEvent()
        {
            moneyRewardView.onClick.RemoveListener(NotifyAfterChooseMoneyReward);
        }
        
        private void ResetView()
        {
            rewardPanel.SetActive(false);
            moneyRewardCover.SetActive(false);
            uniqueItemRewardViewOne.gameObject.SetActive(false);
            uniqueItemRewardViewTwo.gameObject.SetActive(false);
        }

        private void OutPutMoneyReward(OutPutMoneyReward data)
        {
            rewardPanel.SetActive(true);
            moneyRewardCover.SetActive(true);
            moneyRewardValue = data.MoneyValue;
        }
        
        private void OutPutUniqueItemReward(OutPutUniqueItemReward data)
        {
            switch (data.UniqueItemIdList.Count)
            {
                case <= 0:
                    NoUniqueRewardToChoose eventData = new NoUniqueRewardToChoose();
                    GlobalMessagePipe.GetPublisher<NoUniqueRewardToChoose>().Publish(eventData);
                    break;
                case 1:
                    uniqueItemRewardViewOne.SetUniqueItemReward(data.UniqueItemIdList[0]);
                    uniqueItemRewardViewTwo.SetUniqueItemReward();
                    rewardPanel.SetActive(true);
                    uniqueItemRewardViewOne.gameObject.SetActive(true);
                    uniqueItemRewardViewTwo.gameObject.SetActive(true);
                    break;
                default:
                    uniqueItemRewardViewOne.SetUniqueItemReward(data.UniqueItemIdList[0]);
                    uniqueItemRewardViewTwo.SetUniqueItemReward(data.UniqueItemIdList[1]);
                    rewardPanel.SetActive(true);
                    uniqueItemRewardViewOne.gameObject.SetActive(true);
                    uniqueItemRewardViewTwo.gameObject.SetActive(true);
                    break;
            }
        }
        
        private void CloseUniqueRewardPanel(ChooseUniqueReward data)
        {
            rewardPanel.SetActive(false);
            uniqueItemRewardViewOne.gameObject.SetActive(false);
            uniqueItemRewardViewTwo.gameObject.SetActive(false);
        }
        
        private void NotifyAfterChooseMoneyReward()
        {
            moneyRewardView.gameObject.SetActive(false);
            CloseRewardPanel();
            ChooseMoneyReward data = new ChooseMoneyReward(moneyRewardValue);
            GlobalMessagePipe.GetPublisher<ChooseMoneyReward>().Publish(data);
        }

        public void CloseRewardPanel()
        {
            rewardPanel.SetActive(false);
        }
    }
}