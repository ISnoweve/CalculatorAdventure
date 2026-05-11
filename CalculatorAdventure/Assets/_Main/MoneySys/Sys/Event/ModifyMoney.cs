using _Main.MoneySys.Data.Enum;
using EventSys.Interface;

namespace _Main.MoneySys.Sys.Event
{
    public readonly struct ModifyMoney : IEventData
    {
        private readonly int _modifyValue;
        private readonly MoneyModifyType _modifyType;
        public int ModifyValue => _modifyValue;
        public MoneyModifyType ModifyType => _modifyType;
        
        public ModifyMoney(int modifyValue, MoneyModifyType modifyType)
        {
            _modifyValue = modifyValue;
            _modifyType = modifyType;
        }
    }
}