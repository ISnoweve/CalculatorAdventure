using System;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.MobSys.View.UI_Mob.Runtime
{
    public class UI_MobGetDefeated : MonoBehaviour
    {
        [SerializeField] private GameObject defeatedPanel;

        private void Awake()
        {
            defeatedPanel.SetActive(false);
            SubscribeEvent();
        }
        
        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ChooseMoneyReward>().Subscribe(ShowDefeatedPanel).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ChooseUniqueReward>().Subscribe(ShowDefeatedPanel).AddTo(bag);
            _disposable = bag.Build();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        private void ShowDefeatedPanel(ChooseMoneyReward data)
        {
            defeatedPanel.SetActive(true);
        }

        private void ShowDefeatedPanel(ChooseUniqueReward data)
        {
            defeatedPanel.SetActive(true);
        }
    }
}