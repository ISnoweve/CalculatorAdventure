using EventSys.Interface;

namespace _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event
{
    public readonly struct OutPutMoneyReward : IEventData
    {
        private readonly int _moneyValue;
        public int MoneyValue => _moneyValue;
        
        public OutPutMoneyReward(int moneyValue)
        {
            _moneyValue = moneyValue;
        }
    }
}