using _Main.StateSys.GameStateMachineSys.Enum;
using EventSys.Interface;

namespace _Main.GameSceneSys.Sys.Event
{
    public readonly struct AfterSceneChange : IEventData
    {
        public GameState CurrentGameState { get; }

        public AfterSceneChange(GameState currentGameState)
        {
            CurrentGameState = currentGameState;
        }
    }
}