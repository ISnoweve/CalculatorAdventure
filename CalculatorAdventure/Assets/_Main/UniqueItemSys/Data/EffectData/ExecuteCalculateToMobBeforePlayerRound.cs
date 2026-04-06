using _Main.CalculatorSys.Enum;
using _Main.UniqueItemSys.Data.EffectData.Base;
using _Main.UniqueItemSys.Data.EffectData.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.UniqueItemSys.Data.EffectData
{
    //12001 ~ 12004
    [CreateAssetMenu(fileName = "ExecuteCalculateBeforePlayerRound", menuName = "SoSetting/UniqueItem/ExecuteCalculateToMobBeforePlayerRound", order = 0)]
    public class ExecuteCalculateToMobBeforePlayerRound : EffectBaseData
    {
        [SerializeField] private int modifyNumber;
        public int ModifyNumber => modifyNumber;
        [SerializeField] private CalculatorOperator calculatorOperator; 
        public CalculatorOperator CalculatorOperator => calculatorOperator;
        public override void ExecuteTrigger()
        {
            Event_ExecuteCalculateToMobBeforePlayerRound eventData = new Event_ExecuteCalculateToMobBeforePlayerRound(modifyNumber, calculatorOperator);
            GlobalMessagePipe.GetPublisher<Event_ExecuteCalculateToMobBeforePlayerRound>().Publish(eventData);
        }
    }
}