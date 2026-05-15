using EventSys.Interface;

namespace _Main.HealthSys.Sys.Event
{
    public readonly struct Event_HealthUpdate : IEventData
    {
        private readonly int _health;
        public int Health => _health;
        
        public Event_HealthUpdate(int health)
        {
            _health = health;
        }
    }
}