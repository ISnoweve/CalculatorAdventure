using EventSys.Interface;

namespace _Main.CalculatorSys.View.UI_TriggerByRewardPutNumber.Event
{
    public readonly struct Event_PutNumber : IEventData
    {
        private readonly byte index;
        public byte Index => index;

        public Event_PutNumber(byte index)
        {
            this.index = index;
        }
    }
}