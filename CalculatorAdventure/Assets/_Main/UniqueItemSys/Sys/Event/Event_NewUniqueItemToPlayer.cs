using _Main.UniqueItemSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.UniqueItemSys.Sys.Event
{
    public readonly struct Event_NewUniqueItemToPlayer : IEventData
    {
        private readonly UniqueItem _item;
        public UniqueItem Item => _item;
        
        public Event_NewUniqueItemToPlayer(UniqueItem item)
        {
            _item = item;
        }
    }
}