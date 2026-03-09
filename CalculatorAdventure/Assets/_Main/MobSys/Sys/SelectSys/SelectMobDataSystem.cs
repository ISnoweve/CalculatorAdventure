using System;
using System.Linq;
using _Main.MobSys.Data;
using _Main.SnoweveToolKit.ToolKit;
using UnityEngine;

namespace _Main.MobSys.Sys.SelectSys
{
    [Serializable]
    public class SelectMobDataSystem : Singleton<SelectMobDataSystem>
    {
        [SerializeField] private MobDataSoList mobDataSoList;
        public MobDataSoList MobDataSoList => mobDataSoList;
        [SerializeField] private int currentSelectMobDataId;
        public static int CurrentSelectMobDataId => Instance.currentSelectMobDataId;
        
        public static void SelectMobData(int data)
        {
            Instance.currentSelectMobDataId = data;
        }
        
        public static MobData GetMobDataById(int id)
        {
            return Instance.mobDataSoList.Mobs.First(x => x.Id == id);
        }
        
        public static void SetMobDataSoList(MobDataSoList mobDataSoList)
        {
            Instance.mobDataSoList = mobDataSoList;
        }
    }
}