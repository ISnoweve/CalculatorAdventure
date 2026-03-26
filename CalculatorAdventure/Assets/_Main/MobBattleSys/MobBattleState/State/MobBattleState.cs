using System;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Sys.Button;
using _Main.ChallengeSys.Sys;
using _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView.Event;
using _Main.GameSceneSys.Sys.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobBattleSys.Sys.MobSys.Event;
using _Main.MobBattleSys.Sys.SelectSys;
using _Main.MobBattleSys.View.UI_Mob.Runtime.Event;
using _Main.MobSys.Manager;
using _Main.StateSys.GameStateMachineSys.Enum;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine; 

namespace _Main.MobBattleSys.MobBattleState.State
{
    [Serializable]
    public class MobBattleState : Singleton<MobBattleState>
    {
        [SerializeField] private GameState detectGameState;
        [SerializeField] private MobBattleStateEnum mobBattleStateEnum;

        #region Life Cycle

        protected override void Initialize()
        {
            detectGameState = GameState.InMobBattle;
            SubscribeEvent();
            base.Initialize(); 
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<AfterSceneChange>().Subscribe(SetMobBattle).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_FadeOutAnimationEnd>().Subscribe(StartGame).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Calculate_MobDefeated>().Subscribe(StateEnter_BattleResult).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<FinishedUpdateNewNumber>().Subscribe(StateEnter_MobTurn).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<FinishedUpdateBehaviourCountDown>().Subscribe(StateEnter_PlayerTurn)
                .AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion

        #region Behaviour

        private void SetMobBattle(AfterSceneChange data)
        {
            if (data.CurrentGameState != detectGameState) return;
            NotifySetMobBattle notifySetMobBattle = new NotifySetMobBattle();
            GlobalMessagePipe.GetPublisher<NotifySetMobBattle>().Publish(notifySetMobBattle);
        }

        private void StartGame(Event_FadeOutAnimationEnd data)
        {
            StateEnter_PlayerTurn();
        }

        private void StateEnterMobBattleStart()
        {
            mobBattleStateEnum = MobBattleStateEnum.BattleStart;
            CallNewState();
        }

        private void StateEnter_MobSpeak()
        {
            mobBattleStateEnum = MobBattleStateEnum.MobSpeak;
            CallNewState();
        }

        private void StateEnter_BeforePlayerTurn()
        {
            mobBattleStateEnum = MobBattleStateEnum.BeforePlayerTurn;
            CallNewState();
        }

        //for test
        private void StateEnter_PlayerTurn()
        {
            mobBattleStateEnum = MobBattleStateEnum.PlayerTurn;
            CallNewState();
        }

        private void StateEnter_PlayerTurn(FinishedUpdateBehaviourCountDown data)
        {
            mobBattleStateEnum = MobBattleStateEnum.PlayerTurn;
            CallNewState();
        }

        private void StateEnter_MobTurn(FinishedUpdateNewNumber data)
        {
            mobBattleStateEnum = MobBattleStateEnum.MobTurn;
            CallNewState();
        }

        private void StateEnter_BattleResult(Calculate_MobDefeated data)
        {
            mobBattleStateEnum = MobBattleStateEnum.BattleResult;
            CallNewState();
        }

        private void CallNewState()
        {
            var newState = new NotifyMobBattleNewState(mobBattleStateEnum);
            GlobalMessagePipe.GetPublisher<NotifyMobBattleNewState>().Publish(newState);
        }

        #endregion
    }
}