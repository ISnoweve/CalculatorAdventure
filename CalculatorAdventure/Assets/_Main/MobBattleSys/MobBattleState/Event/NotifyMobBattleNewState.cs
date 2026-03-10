using _Main.MobBattleSys.MobBattleState.Enum;
using EventSys.Interface;

namespace _Main.MobBattleSys.MobBattleState.Event
{
    public readonly struct NotifyMobBattleNewState : IEventData
    {
        public MobBattleStateEnum NewState { get; }

        public NotifyMobBattleNewState(MobBattleStateEnum newState)
        {
            NewState = newState;
        }
    }
}