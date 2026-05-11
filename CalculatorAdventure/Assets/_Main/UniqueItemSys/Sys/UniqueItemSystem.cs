using System;
using System.Collections.Generic;
using System.Linq;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data;
using _Main.UniqueItemSys.Data.Enum;
using _Main.UniqueItemSys.Manager;
using _Main.UniqueItemSys.Manager.Runtime;
using _Main.UniqueItemSys.Sys.Event;
using _Main.UtilityFeature;
using MessagePipe;
using Sirenix.OdinInspector;
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
            GlobalMessagePipe.GetSubscriber<ChooseUniqueReward>().Subscribe(TryTriggerUniqueItemByGetItem).AddTo(bag);
            _disposable = bag.Build();
        }
        
        public static void SpawnAllUniqueItemInSoList()
        {
            foreach (var uniqueItemData in Instance.uniqueItemDataSoList.UniqueItemDataList)
            {
                UniqueItemManager.SpawnUniqueItem(uniqueItemData);
            }
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }


        #endregion
        
        #region Trigger Unique Item Effect

        private void TryTriggerUniqueItemByGetItem(ChooseUniqueReward data)
        {
            UniqueItem item = UniqueItemManager.GetUniqueItemById(data.RewardIndex);
            UniqueItemManager.AddUniqueItemToPlayerInventory(item);
            
            Event_NewUniqueItemToPlayer trigger = new Event_NewUniqueItemToPlayer(item);
            GlobalMessagePipe.GetPublisher<Event_NewUniqueItemToPlayer>().Publish(trigger);
            
            if (item.Type == UniqueItemType.Internal)
            {
                item.ExecuteEffect();
            }
        }

        private void TriggerItemBeforePlayerTurn(NotifyMobBattleNewState data)
        {
            if (data.NewState == MobBattleStateEnum.BeforePlayerTurn)
            {
                TryTriggerUniqueItemEffectByType(UniqueItemType.BeforePlayerTurn);
            }
        }

        private void TriggerItemWhenArrivalEveryMapSpot()
        {
            TryTriggerUniqueItemEffectByType(UniqueItemType.WhenArrivalEveryMapSpot);
        }
        private void TryTriggerUniqueItemEffectByType(UniqueItemType type)
        {
            List<UniqueItem> items = UniqueItemManager.GetUniqueItemsInPlayerInventoryByType(type);
            if (items.Count == 0)
            {
                NoUniqueItemTrigger trigger = new();
                GlobalMessagePipe.GetPublisher<NoUniqueItemTrigger>().Publish(trigger);
                return;
            }
            
            foreach (var item in items.Where(item => item.Type == UniqueItemType.BeforePlayerTurn))
            {
                item.ExecuteEffect();
            }
        }

        #endregion

        #region Random UniqueItem For Battle Reward
        public List<UniqueItem> TryGetNewUniqueItemIdForBattleReward()
        {
            //return list
            List<int> newUniqueItemIds = new List<int>();
            
            //manager data
            List<UniqueItem> allUniqueItemNotInPlayerInventory = UniqueItemManager.GetAllUniqueItemsNotInPlayerInventory();
            allUniqueItemNotInPlayerInventory.ShuffleList();
            
            int index = 0;
            bool haveIncreaseCalculatorBoxItem = false;
            
            foreach (var uniqueItem in allUniqueItemNotInPlayerInventory)
            {
                if (uniqueItem.IncreaseCalculatorBox)
                {
                    if (haveIncreaseCalculatorBoxItem)
                    {
                        continue;
                    }
                    UniqueItemData newData = DetectIncreaseCalculatorBox();
                    if(newData ==null)continue;
                    newUniqueItemIds.Add(newData.Id);
                    haveIncreaseCalculatorBoxItem = true;
                }
                else
                {
                    newUniqueItemIds.Add(uniqueItem.Id);
                }
                
                index++;
                if(index==2|| allUniqueItemNotInPlayerInventory.Count<=1)break;
            }
            
            return GetUniqueItemsByIds(newUniqueItemIds);
        }

        private UniqueItemData DetectIncreaseCalculatorBox()
        {
            List<UniqueItemData> increaseCalculatorBox = uniqueItemDataSoList.IncreaseCalculatorBoxList;
            if (increaseCalculatorBox.Count == 0) return null;

            List<UniqueItem> allUniqueItemInPlayerInventory = UniqueItemManager.GetAllUniqueItemsInPlayerInventory();

            foreach (var uniqueItemData in increaseCalculatorBox)
            {
                if(allUniqueItemInPlayerInventory.Exists(item => item.Id == uniqueItemData.Id)) continue;
                return uniqueItemData;
            }
            return null;
        }
        
        private List<UniqueItem> GetUniqueItemsByIds(List<int> uniqueItemIds)
        {
            List<UniqueItem> allUniqueItemInManager = UniqueItemManager.GetAllUniqueItems();
            return allUniqueItemInManager.Where(item => uniqueItemIds.Contains(item.Id)).ToList();
        }
        
        #endregion
    }
}