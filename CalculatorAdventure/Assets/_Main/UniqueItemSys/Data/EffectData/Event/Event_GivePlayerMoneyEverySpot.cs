using EventSys.Interface;

namespace _Main.UniqueItemSys.Data.EffectData.Event
{
    public readonly struct Event_GivePlayerMoneyEverySpot : IEventData
    {
        private readonly int moneyAmount;
        public int MoneyAmount => moneyAmount;
        public Event_GivePlayerMoneyEverySpot(int moneyAmount)
        {
            this.moneyAmount = moneyAmount;
        }
    }
}