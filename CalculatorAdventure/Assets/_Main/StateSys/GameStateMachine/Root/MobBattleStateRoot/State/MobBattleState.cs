using System;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;

namespace _Main.StateSys.GameStateMachine.Root.MobBattleStateRoot.State
{
    public class MobBattleState : Singleton<MobBattleState>
    {
        #region Life Cycle

        protected override void Initialize()
        {
            SubscribeEvent();
            base.Initialize();
        }
        
        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion

        #region Behaviour

        private void StateEnter_MobBattleStart()
        {
            
        }

        // skip (prototype version)
        private void StateEnter_MobSpeak()
        {
            
        }
        
        private void StateEnter_BeforePlayerTurn()
        {
            
        }
        
        private void StateEnter_PlayerTurn()
        {
            
        }

        private void StateEnter_PlayerSendResult()
        {
            
        }
        
        private void StateEnter_MobTurn()
        {
            
        }
        
        private void StateEnter_BattleResult()
        {
            
        }

        #endregion
    }
}