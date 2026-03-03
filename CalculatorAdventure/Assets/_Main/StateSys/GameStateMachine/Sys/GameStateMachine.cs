using System;
using _Main.SnoweveToolKit.ToolKit;
using _Main.StateSys.GameStateMachine.Enum;
using _Main.StateSys.GameStateMachine.Root;
using _Main.StateSys.GameStateMachine.Sys.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.StateSys.GameStateMachine.Sys
{
    [Serializable]
    public class GameStateMachine : Singleton<GameStateMachine>
    {
        [SerializeField] private GameState currentGameState;
        [SerializeField] private GameState previousGameState;

        protected override void Initialize()
        {
            base.Initialize();
            InitState();
        }

        private void InitState()
        {
            currentGameState = GameState.Menu;
            previousGameState = GameState.None;
        }

        private void ChangeGameState(GameState newGameState)
        {
            previousGameState = currentGameState;
            currentGameState = newGameState;
            
            GameStateMachineChangeState gameStateMachineChangeState = new GameStateMachineChangeState(newGameState);
            GlobalMessagePipe.GetPublisher<GameStateMachineChangeState>().Publish(gameStateMachineChangeState);
        }
    }
}