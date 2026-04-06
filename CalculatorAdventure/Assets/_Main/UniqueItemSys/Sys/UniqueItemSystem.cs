using System;
using System.Collections.Generic;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data;
using _Main.UniqueItemSys.Data.Enum;
using _Main.UniqueItemSys.Manager;
using _Main.UniqueItemSys.Manager.Runtime;
using _Main.UniqueItemSys.Sys.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.UniqueItemSys.Sys
{
    [Serializable]
    public class UniqueItemSystem : Singleton<UniqueItemSystem>
    {
        [SerializeField] private UniqueItemDataSoList uniqueItemDataSoList;
        
        public void SetUniqueItemDataSoList(UniqueItemDataSoList soList)
        {
            Instance.uniqueItemDataSoList = soList;
        }

        #region Life Cycle
        
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
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(TriggerItemBeforePlayerTurn).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }


        #endregion
        
        #region Trigger Unique Item Effect

        private void TriggerItemBeforePlayerTurn(NotifyMobBattleNewState data)
        {
            if (data.NewState == MobBattleStateEnum.BeforePlayerTurn)
            {
                TryTriggerUniqueItemEffectByType(UniqueItemType.BeforePlayerTurn);
            }
        }

        private void TriggerInternalItem()
        {
            TryTriggerUniqueItemEffectByType(UniqueItemType.Internal);
        }

        private void TriggerItemWhenArrivalEveryMapSpot()
        {
            TryTriggerUniqueItemEffectByType(UniqueItemType.WhenArrivalEveryMapSpot);
        }
        private void TryTriggerUniqueItemEffectByType(UniqueItemType type)
        {
            List<UniqueItem> items = UniqueItemManager.GetUniqueItemsByType(type);
            if (items.Count == 0)
            {
                NoUniqueItemTrigger trigger = new();
                GlobalMessagePipe.GetPublisher<NoUniqueItemTrigger>().Publish(trigger);
                return;
            }
            
            foreach (var item in items)
            {
                item.ExecuteEffect();
            }
        }

        #endregion

        #region Random UniqueItem For Battle Reward

        

        #endregion
    }
}