using EventSys.Interface;

namespace _Main.UniqueItemSys.Data.EffectData.Event
{
    public readonly struct Event_GiveCalculatorNumber : IEventData
    {
        private readonly int giveNumber;
        public int GiveNumber => giveNumber;
        public Event_GiveCalculatorNumber(int giveNumber)
        {
            this.giveNumber = giveNumber;
        }
    }
}