using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_TakeCalculatorButtonsMultiply : IEventData
    {
        private readonly int _mobNewQuestionNumber;
        public int MobNewQuestionNumber => _mobNewQuestionNumber;
        private readonly List<CalculatorButton> _takeButtons;
        public List<CalculatorButton> TakeButtons => _takeButtons;

        public Event_AtkS_Recover_TakeCalculatorButtonsMultiply(List<CalculatorButton> takeButtons, int mobNewQuestionNumber)
        {
            _takeButtons = takeButtons;
            _mobNewQuestionNumber = mobNewQuestionNumber;
        }
    }
}