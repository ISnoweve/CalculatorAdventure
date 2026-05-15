using System;
using _Main.HealthSys.View.UI_HealthView.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.HealthSys.View.UI_GetStrike
{
    public class PlayerGetStrike : MonoBehaviour
    {
        [SerializeField] private GameObject getStrikePanel;
        
        private void Awake()
        {
            SubscribeEvent();
            Reset();
        }
        
        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(ShowGoBackMenu).AddTo(bag);
            _disposable = bag.Build();
        }
        
        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        private void Reset()
        {
            getStrikePanel.SetActive(false);
        }
        
        private void ShowGoBackMenu(NotifyMobBattleNewState data)
        {
            if(data.NewState != MobBattleStateEnum.GetStrike)return;
            getStrikePanel.SetActive(true);
        }
    }
}