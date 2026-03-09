using System;
using _Main.MobSys.Manager.RunTime;
using _Main.MobSys.Sys;
using _Main.MobSys.Sys.SelectSys;
using _Main.MobSys.Sys.SelectSys.Event;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using UnityEngine;

namespace _Main.MobSys.Manager
{
    [Serializable]
    public class MobManager : Singleton<MobManager>
    {
        [SerializeField] private Mob currentsMob;
        public static Mob CurrentsMob => Instance.currentsMob;

        public static void SpawnMob()
        {
            Mob newMob = new Mob(SelectMobDataSystem.GetMobDataById(SelectMobDataSystem.CurrentSelectMobDataId));
            Instance.currentsMob = newMob;
            
            SpawnMobEvent spawnMobEvent = new SpawnMobEvent(newMob);
            GlobalMessagePipe.GetPublisher<SpawnMobEvent>().Publish(spawnMobEvent);
        }
    }
}