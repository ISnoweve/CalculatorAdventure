using UnityEngine;

namespace _Main.ChallengeSys.Data.Challenges
{
    [CreateAssetMenu(fileName = "Challenge_Prime", menuName = "SoSetting/Challenge/Challenge_Prime", order = 0)]
    public class Challenge_Prime : ChallengeData
    {
        public override bool CheckIsChallengePass<T>(T challengeTarget)
        {
            if (challengeTarget is int number) return IsPrime(number);
            return false;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = Mathf.FloorToInt(Mathf.Sqrt(number));
            for (var i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}