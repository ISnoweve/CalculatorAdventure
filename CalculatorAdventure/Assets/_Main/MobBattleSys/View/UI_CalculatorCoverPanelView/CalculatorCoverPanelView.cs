using System;
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
            GlobalMessagePipe.GetSubscriber<ButtonRecoverOldNumber>().Subscribe(UpdateButtonCloseClick);
            _disposable = bag.Build();
        }

        private void SetActiveCoverPanel(NotifyMobBattleNewState data)
        {
            Debug.Log(data.NewState);
            if (data.NewState == MobBattleStateEnum.PlayerTurn)
            {
                coverPanel.SetActive(false);
                buttonCoverPanel.SetActive(false);
                return;
            }

            coverPanel.SetActive(true);
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