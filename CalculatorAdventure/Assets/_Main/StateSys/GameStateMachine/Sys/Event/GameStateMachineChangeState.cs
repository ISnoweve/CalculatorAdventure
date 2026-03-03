using _Main.StateSys.GameStateMachine.Enum;

namespace _Main.StateSys.GameStateMachine.Sys.Event
{
    public readonly struct GameStateMachineChangeState
    {
        private readonly GameState newGameState;
        public GameState NewGameState => newGameState;
        public GameStateMachineChangeState(GameState newGameState)
        {
            this.newGameState = newGameState;
        }
    }
}