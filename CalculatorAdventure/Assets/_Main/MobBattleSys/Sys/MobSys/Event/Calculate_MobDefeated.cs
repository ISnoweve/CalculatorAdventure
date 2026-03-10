using EventSys.Interface;

namespace _Main.MobBattleSys.Sys.MobSys.Event
{
    public readonly struct Calculate_MobDefeated : IEventData
    {
        public int QuestionNumber { get; }

        public Calculate_MobDefeated(int questionNumber)
        {
            QuestionNumber = questionNumber;
        }
    }
}