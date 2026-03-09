using _Main.StateSys.GameStateMachineSys.Enum;
using EventSys.Interface;

namespace _Main.StateSys.GameStateMachineSys.Sys.Event
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