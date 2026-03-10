using EventSys.Interface;

namespace _Main.MobBattleSys.Sys.MobSys.Event
{
    public readonly struct Calculate_UpdateMobQuestionNumber : IEventData
    {
        public int QuestionNumber { get; }

        public Calculate_UpdateMobQuestionNumber(int questionNumber)
        {
            QuestionNumber = questionNumber;
        }
    }
}