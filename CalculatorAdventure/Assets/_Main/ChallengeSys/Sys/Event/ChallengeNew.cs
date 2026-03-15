using _Main.ChallengeSys.Data;
using EventSys.Interface;

namespace _Main.ChallengeSys.Sys.Event
{
    public readonly struct ChallengeNew : IEventData
    {
        public ChallengeData ChallengeData { get; }

        public ChallengeNew(ChallengeData challengeData)
        {
            ChallengeData = challengeData;
        }
    }
}