using EventSys.Interface;

namespace _Main.HealthSys.Sys.Event
{
    public readonly struct Event_InitialHealth : IEventData
    {
        private readonly int _maxHealth;
        private readonly int _health;
        public int MaxHealth => _maxHealth;
        public int Health => _health;

        public Event_InitialHealth(int maxHealth, int health)
        {
            _maxHealth = maxHealth;
            _health = health;
        }
    }
}