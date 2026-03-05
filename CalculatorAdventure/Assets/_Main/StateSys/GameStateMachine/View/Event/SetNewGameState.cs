using _Main.StateSys.GameStateMachine.Enum;
using EventSys.Interface;

namespace _Main.StateSys.GameStateMachine.View.Event
{
    public readonly struct SetNewGameState : IEventData
    {
        private readonly GameState _newGameState;
        public GameState NewGameState => _newGameState;
        
        public SetNewGameState(GameState newGameState)
        {
            _newGameState = newGameState;
        }
    }
}