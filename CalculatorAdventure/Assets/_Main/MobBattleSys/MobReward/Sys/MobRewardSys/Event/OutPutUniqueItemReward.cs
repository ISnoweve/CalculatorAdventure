using System.Collections.Generic;
using _Main.UniqueItemSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event
{
    public readonly struct OutPutUniqueItemReward : IEventData
    {
        private readonly List<UniqueItem> _uniqueItemIdList;
        public List<UniqueItem> UniqueItemIdList => _uniqueItemIdList;

        public OutPutUniqueItemReward(List<UniqueItem> uniqueItemIdList)
        {
            _uniqueItemIdList = uniqueItemIdList;
        }

    }
}