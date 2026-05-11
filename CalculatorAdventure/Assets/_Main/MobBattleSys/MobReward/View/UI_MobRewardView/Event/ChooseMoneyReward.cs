using EventSys.Interface;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event
{
    public readonly struct ChooseMoneyReward : IEventData
    {
        private readonly int _rewardValue;
        public int RewardValue => _rewardValue;
        public ChooseMoneyReward(int rewardValue)
        {
            _rewardValue = rewardValue;
        }
    }
}