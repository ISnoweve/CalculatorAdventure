using System.Collections.Generic;
using _Main.UniqueItemSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event
{
    public readonly struct OutPutUniqueItemReward : IEventData
    {
        private readonly List<int> _uniqueItemIdList;
        public List<int> UniqueItemIdList => _uniqueItemIdList;

        public OutPutUniqueItemReward(List<int> uniqueItemIdList)
        {
            _uniqueItemIdList = uniqueItemIdList;
        }

    }
}