using _Main.ChallengeSys.Enum;
using EventSys.Interface;

namespace _Main.ChallengeSys.Sys.Event
{
    public readonly struct ChallengeSuccess : IEventData
    {
        public ChallengeReward ChallengeReward { get; }

        public ChallengeSuccess(ChallengeReward challengeReward)
        {
            ChallengeReward = challengeReward;
        }
    }
}