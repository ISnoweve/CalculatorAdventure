using System;
using System.Linq;
using _Main.MobSys.Data;
using _Main.MobSys.Manager.Event;
using _Main.MobSys.Manager.RunTime;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using UnityEngine;

namespace _Main.MobSys.Manager
{
    [Serializable]
    public class MobManager : Singleton<MobManager>
    {
        [SerializeField] private MobDataSoList mobDataSoList;
        [SerializeField] private Mob currentsMob;
        public MobDataSoList MobDataSoList => mobDataSoList;
        public static Mob CurrentsMob => Instance.currentsMob;

        public static void SpawnMob(int index)
        {
            var newMob = new Mob(Instance.mobDataSoList.Mobs.First(x => x.Id == index));
            Instance.currentsMob = newMob;

            var spawnMobEvent = new SpawnMobEvent(newMob);
            GlobalMessagePipe.GetPublisher<SpawnMobEvent>().Publish(spawnMobEvent);
        }

        public static void SetMobDataSoList(MobDataSoList mobDataSoList)
        {
            Instance.mobDataSoList = mobDataSoList;
        }
    }
}