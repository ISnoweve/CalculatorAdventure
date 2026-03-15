using _Main.ChallengeSys.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.ChallengeSys.Data
{
    //[CreateAssetMenu(fileName = "ChallengeData", menuName = "SoSetting/Challenge/Test", order = 0)]
    public abstract class ChallengeData : ScriptableObject
    {
        [Title("Basic Info")] public string challengeName;

        [TextArea] public string challengeDescription;
        public ChallengeReward challengeReward;
        public int toGoalCount;

        public abstract bool CheckIsChallengePass<T>(T challengeTarget);
    }
}