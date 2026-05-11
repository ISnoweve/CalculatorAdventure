using EventSys.Interface;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event
{
    public readonly struct ChooseUniqueReward : IEventData
    {
        private readonly int _rewardIndex;
        public int RewardIndex => _rewardIndex;
        public ChooseUniqueReward(int rewardIndex)
        {
            _rewardIndex = rewardIndex;
        }
    }
}