using System;
using _Main.SnoweveToolKit.ToolKit;
using _Main.StateSys.GameStateMachineSys.Enum;
using _Main.StateSys.GameStateMachineSys.Sys.Event;
using _Main.StateSys.GameStateMachineSys.View.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.StateSys.GameStateMachineSys.Sys
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