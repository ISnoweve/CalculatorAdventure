using EventSys.Interface;

namespace _Main.MobSys.Sys.MobSys.Event
{
    public readonly struct Calculate_UpdateMobQuestionNumber : IEventData
    {
        private readonly int _questionNumber;
        public int QuestionNumber => _questionNumber;

        public Calculate_UpdateMobQuestionNumber(int questionNumber)
        {
            _questionNumber = questionNumber;
        }
    }
}