using System.Collections.Generic;
using UnityEngine;

namespace _Main.MobBattleSys.MobReward.Data.MobReward
{
    [CreateAssetMenu(fileName = "AllMobRewardData", menuName = "SoSetting/Mob/Reward/MobRewardDataSoList", order = 1)]
    public class MobRewardDataSoList : ScriptableObject
    {
        [SerializeField] private List<MobRewardData> mobRewardDataList;
        public List<MobRewardData> MobRewardDataList => mobRewardDataList;
    }
}