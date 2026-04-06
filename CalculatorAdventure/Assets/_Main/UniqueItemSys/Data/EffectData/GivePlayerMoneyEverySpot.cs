using _Main.UniqueItemSys.Data.EffectData.Base;
using _Main.UniqueItemSys.Data.EffectData.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.UniqueItemSys.Data.EffectData
{
    //12068
    [CreateAssetMenu(fileName = "GivePlayerMoneyEverySpot", menuName = "SoSetting/UniqueItem/GivePlayerMoneyEverySpot", order = 0)]
    public class GivePlayerMoneyEverySpot : EffectBaseData
    {
        [SerializeField] private int moneyAmount;
        public int MoneyAmount => moneyAmount;
        public override void ExecuteTrigger()
        {
            Event_GivePlayerMoneyEverySpot eventData = new Event_GivePlayerMoneyEverySpot(moneyAmount);
            GlobalMessagePipe.GetPublisher<Event_GivePlayerMoneyEverySpot>().Publish(eventData);
        }
    }
}