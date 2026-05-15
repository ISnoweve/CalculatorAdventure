using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.ChallengeSys.Data.Challenges
{
    [CreateAssetMenu(fileName = "Challenge_ResultLastNumber",
        menuName = "SoSetting/Challenge/Challenge_ResultLastNumber", order = 0)]
    public class Challenge_ResultLastNumber : ChallengeData
    {
        [Title("Challenge Info")] public int lastNumber;

        public override bool CheckIsChallengePass<T>(T challengeTarget)
        {
            if (challengeTarget is int number) return CheckLastNumber(number);
            return false;
        }

        private bool CheckLastNumber(int number)
        {
            return number % 10 == lastNumber || number % 10 == -lastNumber;
        }
    }
}