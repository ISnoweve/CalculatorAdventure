using _Main.StateSys.GameStateMachineSys.Enum;
using EventSys.Interface;

namespace _Main.GameSceneSys.Sys.Event
{
    public readonly struct AfterSceneChange : IEventData
    {
        private readonly GameState _currentGameState;
        public GameState CurrentGameState => _currentGameState;
        
        public AfterSceneChange(GameState currentGameState)
        {
            _currentGameState = currentGameState;
        }
    }
}