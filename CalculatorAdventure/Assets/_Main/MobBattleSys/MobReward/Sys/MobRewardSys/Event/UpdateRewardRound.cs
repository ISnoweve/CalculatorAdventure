using EventSys.Interface;

namespace _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event
{
    public readonly struct UpdateRewardRound : IEventData
    {
        private readonly int _rewardRound;
        public int RewardRound => _rewardRound;
        public UpdateRewardRound(in int rewardRound)
        {
            _rewardRound = rewardRound;
        }
    }
}