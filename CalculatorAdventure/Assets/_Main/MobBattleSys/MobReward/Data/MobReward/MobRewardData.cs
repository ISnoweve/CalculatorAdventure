using _Main.MobSys.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobBattleSys.MobReward.Data.MobReward
{
    [CreateAssetMenu(fileName = "MobRewardData", menuName = "SoSetting/Mob/Reward/MobRewardData", order = 1)]
    public class MobRewardData : ScriptableObject
    {
        [SerializeField] private MobType mobType;
        [SerializeField] private int mobRewardBlank;
        [SerializeField] private float mobRewardMoneyValue;
        [SerializeField] private float mobRewardUniqueItemValue;
        [SerializeField] private float mobRewardMoneyValueLimit;
        [SerializeField] private float mobRewardModifyValueAfterBlank;
        
        [Title("Value")]
        [SerializeField] private int moneyRewardValue;
        
        public MobType MobType => mobType;
        public int MobRewardBlank => mobRewardBlank;
        public float MobRewardMoneyValue => mobRewardMoneyValue;
        public float MobRewardUniqueItemValue => mobRewardUniqueItemValue;
        public float MobRewardMoneyValueLimit => mobRewardMoneyValueLimit;
        public float MobRewardModifyValueAfterBlank => mobRewardModifyValueAfterBlank;
        public int MoneyRewardValue => moneyRewardValue;
    }
}