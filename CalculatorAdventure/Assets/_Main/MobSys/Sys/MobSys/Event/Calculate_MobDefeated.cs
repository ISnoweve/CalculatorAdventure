using EventSys.Interface;

namespace _Main.MobSys.Sys.MobSys.Event
{
    public readonly struct Calculate_MobDefeated : IEventData
    {
        private readonly int _questionNumber;
        public int QuestionNumber => _questionNumber;
        public Calculate_MobDefeated(int questionNumber)
        {
            _questionNumber = questionNumber;
        }
    }
}