using EventSys.Interface;

namespace _Main.UniqueItemSys.Data.EffectData.Event
{
    public readonly struct Event_IncreaseCalculatorBoxLimit : IEventData
    {
        private readonly int increaseLimit;
        public int IncreaseLimit => increaseLimit;
        
        public Event_IncreaseCalculatorBoxLimit(int increaseLimit)
        {
            this.increaseLimit = increaseLimit;
        }
    }
}