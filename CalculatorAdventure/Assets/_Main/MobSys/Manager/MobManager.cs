using System;
using System.Linq;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobSys.Data;
using _Main.MobSys.Data.Mob;
using _Main.MobSys.Manager.Event;
using _Main.MobSys.Manager.RunTime;
using _Main.MobSys.Sys.SelectSys;
using _Main.ToolKit.SingletonFeature;
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

        protected override void Initialize()
        {
            base.Initialize();
            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<NotifySetMobBattle>().Subscribe(SpawnMob).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }
        
        public static void SpawnMob(NotifySetMobBattle data)
        {
            int dataIndex = SelectMobDataSystem.CurrentSelectMobDataId;
            
            var newMob = new Mob(Instance.mobDataSoList.Mobs.First(x => x.Id == dataIndex));
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