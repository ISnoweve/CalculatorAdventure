using _Main.MobSys.Manager.RunTime;
using TMPro;
using UnityEngine;

namespace _Main.MobSys.View.UI_Mob.Runtime
{
    public class UI_MobView : MonoBehaviour
    {
        [SerializeField] private Transform mobSpawnPoint;
        [SerializeField] private GameObject mobBehaviour;

        public void Initialize(Mob mob)
        {
            mobBehaviour = Instantiate(mob.MobPrefab, mobSpawnPoint);
        }
    }
}