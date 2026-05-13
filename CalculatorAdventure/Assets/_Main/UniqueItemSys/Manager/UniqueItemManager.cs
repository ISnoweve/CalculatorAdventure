using System;
using System.Collections.Generic;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data;
using _Main.UniqueItemSys.Data.Enum;
using _Main.UniqueItemSys.Manager.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.UniqueItemSys.Manager
{
    [Serializable]
    public class UniqueItemManager : Singleton<UniqueItemManager>
    {
        [SerializeField] private List<UniqueItem> uniqueItemInPlayerInventory = new();
        [SerializeField] private List<UniqueItem> notInPlayerInventoryUniqueItems = new();
        [SerializeField] private List<UniqueItem> uniqueItems = new();

        [Button]
        private void TestUniqueItem(UniqueItemData data)
        {
            Instance.uniqueItemInPlayerInventory.Add(GetUniqueItemById(data.Id));
            Instance.notInPlayerInventoryUniqueItems.Remove(GetUniqueItemById(data.Id));
        }
        
        public static void SpawnUniqueItem(UniqueItemData data)
        {
            UniqueItem newItem = new UniqueItem(data);
            Instance.uniqueItems.Add(newItem);
            Instance.notInPlayerInventoryUniqueItems.Add(newItem);
        }
        
        public static void AddUniqueItemToPlayerInventory(UniqueItem item)
        {
            Instance.uniqueItemInPlayerInventory.Add(item);
            Instance.notInPlayerInventoryUniqueItems.Remove(item);
        }
        
        public static void RemoveUniqueItemFromPlayerInventory(UniqueItem item)
        {
            Instance.uniqueItemInPlayerInventory.Remove(item);
            Instance.notInPlayerInventoryUniqueItems.Add(item);
        }
        
        public static List<UniqueItem> GetAllUniqueItems()
        {
            return Instance.uniqueItems;
        }
        
        public static List<UniqueItem> GetAllUniqueItemsInPlayerInventory()
        {
            return Instance.uniqueItemInPlayerInventory;
        }
        
        public static List<UniqueItem> GetAllUniqueItemsNotInPlayerInventory()
        {
            return Instance.notInPlayerInventoryUniqueItems;
        }
        
        public static List<UniqueItem> GetUniqueItemsByType(UniqueItemType type)
        {
            return Instance.uniqueItems.FindAll(item => item.Type == type);
        }
        
        public static UniqueItem GetUniqueItemById(int id)
        {
            return Instance.uniqueItems.Find(item => item.Id == id);
        }
        
        public static List<UniqueItem> GetUniqueItemsInPlayerInventoryByType(UniqueItemType type)
        {
            return Instance.uniqueItemInPlayerInventory.FindAll(item => item.Type == type);
        }
    }
}