using System;
using _Main.SnoweveToolKit.ToolKit;
using UnityEngine;

namespace _Main.MobSys.Sys.SelectSys
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