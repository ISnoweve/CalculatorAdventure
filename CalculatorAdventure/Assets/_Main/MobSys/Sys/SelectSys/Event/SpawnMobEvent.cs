using _Main.MobSys.Manager.RunTime;
using EventSys.Interface;

namespace _Main.MobSys.Sys.SelectSys.Event
{
    public readonly struct SpawnMobEvent : IEventData
    {
        private readonly Mob mob; 
        public Mob Mob => mob;
        
        public SpawnMobEvent(Mob data)
        {
            mob = data;
        }
    }
}