using System;
using System.Collections.Generic;
using System.Linq;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data;
using _Main.UniqueItemSys.Data.Enum;
using _Main.UniqueItemSys.Manager;
using _Main.UniqueItemSys.Manager.Runtime;
using _Main.UniqueItemSys.Sys.Event;
using DG.DemiEditor;
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
        public List<UniqueItem> TryGetNewUniqueItemIdForBattleReward()
        {
            List<UniqueItemData> allUniqueItemData = uniqueItemDataSoList.UniqueItemDataList;
            allUniqueItemData.Shuffle();
            if (allUniqueItemData.Count == 0) return null;
            
            List<int> newUniqueItemIds = new List<int>();
            
            List<UniqueItem> allUniqueItemInManager = UniqueItemManager.GetAllUniqueItems();
            if (allUniqueItemInManager.Count <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    UniqueItemData data = allUniqueItemData[i];
                    if (data.IncreaseCalculatorBox) DetectIncreaseCalculatorBox(data);
                    UniqueItemManager.SpawnUniqueItem(data);
                    newUniqueItemIds.Add(data.Id);
                }
            }
            else
            {
                newUniqueItemIds = SpawnNewUniqueItemWithDetect(allUniqueItemData, allUniqueItemInManager);
            }

            return GetUniqueItemsByIds(newUniqueItemIds);
        }

        private List<int> SpawnNewUniqueItemWithDetect(List<UniqueItemData> allUniqueItemData, List<UniqueItem> allUniqueItemInManager)
        {
            int index = 0;
            List<int> newUniqueItemIds = new List<int>();
            foreach (var uniqueItemData in allUniqueItemData)
            {
                if(index==2)break;
                bool alreadyHave = allUniqueItemInManager.Exists(item => item.Id == uniqueItemData.Id);
                if (alreadyHave) continue;
                if (uniqueItemData.IncreaseCalculatorBox) DetectIncreaseCalculatorBox(uniqueItemData);
                UniqueItemManager.SpawnUniqueItem(uniqueItemData);
                index++;
                newUniqueItemIds.Add(uniqueItemData.Id);
            }

            return newUniqueItemIds;
        }

        private UniqueItemData DetectIncreaseCalculatorBox(UniqueItemData data)
        {
            List<UniqueItemData> increaseCalculatorBox = uniqueItemDataSoList.IncreaseCalculatorBoxList;
            if (increaseCalculatorBox.Count == 0) return null;

            List<UniqueItem> allUniqueItemInManager = UniqueItemManager.GetAllUniqueItems();

            return increaseCalculatorBox.
                FirstOrDefault(uniqueItemData => !allUniqueItemInManager.
                    Exists(item => item.Id == uniqueItemData.Id));
        }
        
        private List<UniqueItem> GetUniqueItemsByIds(List<int> uniqueItemIds)
        {
            List<UniqueItem> allUniqueItemInManager = UniqueItemManager.GetAllUniqueItems();
            return allUniqueItemInManager.Where(item => uniqueItemIds.Contains(item.Id)).ToList();
        }
        
        #endregion
    }
}