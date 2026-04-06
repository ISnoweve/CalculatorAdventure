using _Main.UniqueItemSys.Data.EffectData.Base;
using _Main.UniqueItemSys.Data.EffectData.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.UniqueItemSys.Data.EffectData
{
    //12009 ~ 12011
    [CreateAssetMenu(fileName = "IncreaseCalculatorBoxLimit", menuName = "SoSetting/UniqueItem/IncreaseCalculatorBoxLimit", order = 0)]
    public class IncreaseCalculatorBoxLimit : EffectBaseData
    {
        [SerializeField] private int increaseLimit;
        public int IncreaseLimit => increaseLimit;
        public override void ExecuteTrigger()
        {
            Event_IncreaseCalculatorBoxLimit eventData = new Event_IncreaseCalculatorBoxLimit(increaseLimit);
            GlobalMessagePipe.GetPublisher<Event_IncreaseCalculatorBoxLimit>().Publish(eventData);
        }
    }
}