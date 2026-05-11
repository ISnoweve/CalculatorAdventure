using _Main.UniqueItemSys.Data.EffectData.Base;
using _Main.UniqueItemSys.Data.EffectData.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.UniqueItemSys.Data.EffectData
{
    //12012 ~ 12027
    [CreateAssetMenu(fileName = "GiveCalculatorNumber", menuName = "SoSetting/UniqueItem/GiveCalculatorNumber", order = 0)]
    public class GiveCalculatorNumber : EffectBaseData
    {
        [SerializeField] private int giveNumber;
        public int GiveNumber => giveNumber;
        public override void ExecuteTrigger()
        {
            Event_GiveCalculatorNumber eventData = new Event_GiveCalculatorNumber(GiveNumber);
            GlobalMessagePipe.GetPublisher<Event_GiveCalculatorNumber>().Publish(eventData);
        }
    }
}