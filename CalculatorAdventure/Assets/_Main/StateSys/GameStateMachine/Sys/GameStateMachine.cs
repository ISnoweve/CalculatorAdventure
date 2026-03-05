using System;
using _Main.SnoweveToolKit.ToolKit;
using _Main.StateSys.GameStateMachine.Enum;
using _Main.StateSys.GameStateMachine.Root;
using _Main.StateSys.GameStateMachine.Sys.Event;
using _Main.StateSys.GameStateMachine.View.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.StateSys.GameStateMachine.Sys
{
    [Serializable]
    public class GameStateMachine : Singleton<GameStateMachine>
    {
        [SerializeField] private GameState currentGameState;
        [SerializeField] private GameState previousGameState;

        #region Life Cycle

        protected override void Initialize()
        {
            base.Initialize();
            InitState();
            SubscribeEvent();
        }

        private void InitState()
        {
            currentGameState = GameState.Menu;
            previousGameState = GameState.None;
        }
        
        private IDisposable _disposable;
        
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<SetNewGameState>().Subscribe(ChangeGameState).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }


        #endregion
        
        private void ChangeGameState(SetNewGameState date)
        {
            previousGameState = currentGameState;
            currentGameState = date.NewGameState;
            
            GameStateMachineChangeState gameStateMachineChangeState = new GameStateMachineChangeState(currentGameState);
            GlobalMessagePipe.GetPublisher<GameStateMachineChangeState>().Publish(gameStateMachineChangeState);
        }
    }
}