using _Main.StateSys.GameStateMachineSys.Enum;
using EventSys.Interface;

namespace _Main.StateSys.GameStateMachineSys.View.Event
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