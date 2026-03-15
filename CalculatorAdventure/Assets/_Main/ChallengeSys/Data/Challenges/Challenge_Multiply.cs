using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.ChallengeSys.Data.Challenges
{
    [CreateAssetMenu(fileName = "Challenge_Multiply", menuName = "SoSetting/Challenge/Challenge_Multiply", order = 0)]
    public class Challenge_Multiply : ChallengeData
    {
        [Title("Challenge Info")] public int multiplier;

        public override bool CheckIsChallengePass<T>(T challengeTarget)
        {
            if (challengeTarget is int number) return CheckMultiply(number);
            return false;
        }

        private bool CheckMultiply(int number)
        {
            if (number % multiplier == 0)
                return true;
            return false;
        }
    }
}