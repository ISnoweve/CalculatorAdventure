using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_TakeCalculatorButtonsMultiply : IEventData
    {
        public int MobNewQuestionNumber { get; }

        public List<CalculatorButton> TakeButtons { get; }

        public Event_AtkS_Recover_TakeCalculatorButtonsMultiply(List<CalculatorButton> takeButtons,
            int mobNewQuestionNumber)
        {
            TakeButtons = takeButtons;
            MobNewQuestionNumber = mobNewQuestionNumber;
        }
    }
}