using _Main.StateSys.GameStateMachine.Enum;
using EventSys.Interface;

namespace _Main.StateSys.GameStateMachine.Sys.Event
{
    public readonly struct GameStateMachineChangeState : IEventData
    {
        private readonly GameState newGameState;
        public GameState NewGameState => newGameState;
        public GameStateMachineChangeState(GameState newGameState)
        {
            this.newGameState = newGameState;
        }
    }
}