using _Main.StateSys.GameStateMachineSys.Enum;
using EventSys.Interface;

namespace _Main.StateSys.GameStateMachineSys.View.Event
{
    public readonly struct SetNewGameState : IEventData
    {
        public GameState NewGameState { get; }

        public SetNewGameState(GameState newGameState)
        {
            NewGameState = newGameState;
        }
    }
}