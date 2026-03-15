using _Main.StateSys.GameStateMachineSys.Enum;
using EventSys.Interface;

namespace _Main.StateSys.GameStateMachineSys.Sys.Event
{
    public readonly struct GameStateMachineChangeState : IEventData
    {
        public GameState NewGameState { get; }

        public GameStateMachineChangeState(GameState newGameState)
        {
            NewGameState = newGameState;
        }
    }
}