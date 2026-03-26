using System;
using _Main.ToolKit.SingletonFeature;
using UnityEngine;

namespace _Main.MobBattleSys.Sys.SelectSys
{
    [Serializable]
    public class SelectMobDataSystem : Singleton<SelectMobDataSystem>
    {
        [SerializeField] private int currentSelectMobDataId;
        public static int CurrentSelectMobDataId => Instance.currentSelectMobDataId;

        public static void SelectMobData(int data)
        {
            Instance.currentSelectMobDataId = data;
        }
    }
}