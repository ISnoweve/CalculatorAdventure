using UnityEngine;

namespace _Main.MobSys.Data
{
    [CreateAssetMenu(fileName = "AllMobData", menuName = "SoSetting/Mob/AllMobData", order = 1)]
    public class MobDataSoList : ScriptableObject
    {
        [SerializeField] private MobData[] mobs;
        public MobData[] Mobs => mobs;
    }
}