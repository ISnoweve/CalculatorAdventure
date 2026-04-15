using _Main.MobSys.View.UI_Mob.Runtime.Enum;
using EventSys.Interface;

namespace _Main.MobSys.View.UI_Mob.Runtime.Event
{
    public readonly struct FinishedUpdateNewNumber : IEventData
    {
        private readonly FinishedUpdateNewNumberType _finishedUpdateNewNumberType; 
        public FinishedUpdateNewNumberType FinishedUpdateNewNumberType => _finishedUpdateNewNumberType;
        
        public FinishedUpdateNewNumber(FinishedUpdateNewNumberType finishedUpdateNewNumberType)
        {
            _finishedUpdateNewNumberType = finishedUpdateNewNumberType;
        }
    }
}