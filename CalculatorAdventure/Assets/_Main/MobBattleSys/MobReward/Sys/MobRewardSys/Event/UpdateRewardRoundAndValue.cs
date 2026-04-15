using EventSys.Interface;

namespace _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event
{
    public readonly struct UpdateRewardRoundAndValue : IEventData
    {
        private readonly int _rewardRound;
        private readonly float _rewardMoneyValue;
        private readonly float _rewardUniqueItemValue;
        private readonly bool _isBossRound;
        
        public int RewardRound => _rewardRound;
        public float RewardMoneyValue => _rewardMoneyValue;
        public float RewardUniqueItemValue => _rewardUniqueItemValue;
        public bool IsBossRound => _isBossRound;
        
        
        public UpdateRewardRoundAndValue(in int rewardRound, in float rewardMoneyValue, in float rewardUniqueItemValue, bool isBossRound)
        {
            _rewardRound = rewardRound;
            _rewardMoneyValue = rewardMoneyValue;
            _rewardUniqueItemValue = rewardUniqueItemValue;
            _isBossRound = isBossRound;
        }
    }
}