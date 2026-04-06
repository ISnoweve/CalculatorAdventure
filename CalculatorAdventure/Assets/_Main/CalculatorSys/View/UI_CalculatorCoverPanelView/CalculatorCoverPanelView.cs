using System;
using System.Collections;
using System.Collections.Generic;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.MobBattleSys.MobBattleState.View.UI_CalculatorCoverPanelView
{
    public class CalculatorCoverPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject coverPanel;
        [SerializeField] private GameObject buttonCoverPanel;

        private IDisposable _disposable;

        private void Awake()
        {
            SubscribeEvent();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(SetActiveCoverPanel).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<CalculatorNotifyIsLastNumberAfterRecover>()
                .Subscribe(UpdateButtonCloseClick).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonRecoverOldNumber>().Subscribe(UpdateButtonCloseClick).AddTo(bag);
            _disposable = bag.Build();
        }

        private void SetActiveCoverPanel(NotifyMobBattleNewState data)
        {
            if (data.NewState == MobBattleStateEnum.PlayerTurn)
            {
                StartCoroutine(Stay());
                return;
            }
            
            coverPanel.SetActive(true);
        }
        
        private IEnumerator Stay()
        {
            yield return new WaitForSeconds(0.1f);
            coverPanel.SetActive(false);
            buttonCoverPanel.SetActive(false);
        }

        private void UpdateButtonCloseClick(CalculatorNotifyIsLastNumberAfterRecover data)
        {
            buttonCoverPanel.SetActive(true);
        }

        private void UpdateButtonCloseClick(ButtonRecoverOldNumber data)
        {
            buttonCoverPanel.SetActive(false);
        }
    }
}