using UnityEngine;

namespace _Main.ChallengeSys.Data
{
    [CreateAssetMenu(fileName = "ChallengeSoList", menuName = "SoSetting/Challenge/ChallengeSoList", order = 0)]
    public class ChallengeDataSoList : ScriptableObject
    {
        [SerializeField] private ChallengeData[] challengeDataList;
        public ChallengeData[] ChallengeDataList => challengeDataList;
    }
}