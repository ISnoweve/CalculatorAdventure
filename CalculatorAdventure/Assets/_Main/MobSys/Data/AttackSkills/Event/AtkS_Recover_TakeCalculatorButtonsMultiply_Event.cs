using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;

namespace _Main.MobSys.Data.AttackSkills.Event
{
    public readonly struct AtkS_Recover_TakeCalculatorButtonsMultiply_Event : IEventData
    {
        private readonly List<CalculatorButton> _takeButtons;
        public List<CalculatorButton> TakeButtons => _takeButtons;

        public AtkS_Recover_TakeCalculatorButtonsMultiply_Event(List<CalculatorButton> takeButtons)
        {
            _takeButtons = takeButtons;
        }
    }
}