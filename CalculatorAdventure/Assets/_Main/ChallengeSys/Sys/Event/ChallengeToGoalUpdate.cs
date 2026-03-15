using EventSys.Interface;

namespace _Main.ChallengeSys.Sys.Event
{
    public readonly struct ChallengeToGoalUpdate : IEventData
    {
        public int CurrentGoalCount { get; }

        public int CurrentToGoalCount { get; }

        public ChallengeToGoalUpdate(int currentToGoalCount, int currentGoalCount)
        {
            CurrentToGoalCount = currentToGoalCount;
            CurrentGoalCount = currentGoalCount;
        }
    }
}