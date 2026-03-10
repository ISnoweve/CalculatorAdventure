using _Main.MobSys.Manager.RunTime;
using EventSys.Interface;

namespace _Main.MobSys.Manager.Event
{
    public readonly struct SpawnMobEvent : IEventData
    {
        public Mob Mob { get; }

        public SpawnMobEvent(Mob data)
        {
            Mob = data;
        }
    }
}