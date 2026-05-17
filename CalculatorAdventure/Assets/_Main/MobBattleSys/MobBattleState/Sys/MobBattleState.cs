using System;
using _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView.Event;
using _Main.GameSceneSys.Sys.Event;
using _Main.HealthSys.Sys.Event;
using _Main.HealthSys.View.UI_HealthView.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobSys.Sys.MobSys.Event;
using _Main.MobSys.View.UI_Mob.Runtime.Enum;
using _Main.MobSys.View.UI_Mob.Runtime.Event;
using _Main.StateSys.GameStateMachineSys.Enum;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Sys.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.MobBattleSys.MobBattleState.Sys
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
            GlobalMessagePipe.GetSubscriber<FinishedUpdateNewNumber>().Subscribe(StateEnter_PlayerTurn).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<FinishedUpdateNewNumber>().Subscribe(StateEnter_BeforePlayerTurn).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_UpdateHealthFinished>().Subscribe(StateEnter_BeforePlayerTurn).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<FinishedUpdateBehaviourCountDown>().Subscribe(StateEnter_BeforePlayerTurn).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<NoUniqueItemTrigger>().Subscribe(StateEnter_PlayerTurn).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_UpdateHealthEmpty>().Subscribe(StateEnter_GetStrike).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<FinishedUpdateModifySkill>().Subscribe(StateEnter_BeforePlayerTurn).AddTo(bag);
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
            StateEnter_BeforePlayerTurn();
        }

        private void StateEnter_BeforePlayerTurn()
        {
            mobBattleStateEnum = MobBattleStateEnum.BeforePlayerTurn;
            CallNewState();
        }
        
        private void StateEnter_BeforePlayerTurn(Event_UpdateHealthFinished data)
        {
            mobBattleStateEnum = MobBattleStateEnum.BeforePlayerTurn;
            CallNewState();
        }
        
        private void StateEnter_BeforePlayerTurn(FinishedUpdateBehaviourCountDown data)
        {
            mobBattleStateEnum = MobBattleStateEnum.BeforePlayerTurn;
            CallNewState();
        }
        
        private void StateEnter_BeforePlayerTurn(FinishedUpdateNewNumber data)
        {
            if(data.FinishedUpdateNewNumberType != FinishedUpdateNewNumberType.ByAttackSkillRecover)return;
            mobBattleStateEnum = MobBattleStateEnum.BeforePlayerTurn;
            CallNewState();
        }

        private void StateEnter_BeforePlayerTurn(FinishedUpdateModifySkill data)
        {
            mobBattleStateEnum = MobBattleStateEnum.BeforePlayerTurn;
            CallNewState();
        }
        
        private void StateEnter_PlayerTurn(NoUniqueItemTrigger data)
        {
            
            mobBattleStateEnum = MobBattleStateEnum.PlayerTurn;
            CallNewState();
        }
        
        private void StateEnter_PlayerTurn(FinishedUpdateNewNumber data)
        {
            if(data.FinishedUpdateNewNumberType != FinishedUpdateNewNumberType.ByUniqueItem)return;
            mobBattleStateEnum = MobBattleStateEnum.PlayerTurn;
            CallNewState();
        }

        private void StateEnter_MobTurn(FinishedUpdateNewNumber data)
        {
            if(data.FinishedUpdateNewNumberType != FinishedUpdateNewNumberType.ByCalculateResult)return;
            mobBattleStateEnum = MobBattleStateEnum.MobTurn;
            CallNewState();
        }

        private void StateEnter_BattleResult(Calculate_MobDefeated data)
        {
            mobBattleStateEnum = MobBattleStateEnum.BattleResult;
            CallNewState();
        }
        
        private void StateEnter_GetStrike(Event_UpdateHealthEmpty data)
        {
            mobBattleStateEnum = MobBattleStateEnum.GetStrike;
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