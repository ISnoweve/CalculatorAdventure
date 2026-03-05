using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct Event_AtkS_Recover_TakeCalculatorButtonsMultiply : IEventData
    {
        private readonly List<CalculatorButton> _takeButtons;
        public List<CalculatorButton> TakeButtons => _takeButtons;

        public Event_AtkS_Recover_TakeCalculatorButtonsMultiply(List<CalculatorButton> takeButtons)
        {
            _takeButtons = takeButtons;
        }
    }
}