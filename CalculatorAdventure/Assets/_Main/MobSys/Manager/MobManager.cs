using System;
using _Main.MobSys.Manager.RunTime;
using _Main.SnoweveToolKit.ToolKit;
using UnityEngine;

namespace _Main.MobSys.Manager
{
    [Serializable]
    public class MobManager : Singleton<MobManager>
    {
        [SerializeField] private Mob currentsMob;
        public static Mob CurrentsMob => Instance.currentsMob;
    }
}