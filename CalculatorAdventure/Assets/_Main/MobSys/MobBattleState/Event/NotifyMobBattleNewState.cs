using _Main.MobSys.MobBattleState.Enum;
using EventSys.Interface;

namespace _Main.MobSys.MobBattleState.Event
{
    public readonly struct NotifyMobBattleNewState : IEventData
    {
        private readonly MobBattleStateEnum _newState;
        public MobBattleStateEnum NewState => _newState;
        public NotifyMobBattleNewState (MobBattleStateEnum newState)
        {
            _newState = newState;
        }
    }
}