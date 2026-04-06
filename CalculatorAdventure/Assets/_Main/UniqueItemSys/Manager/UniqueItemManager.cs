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
        [SerializeField] private List<UniqueItem> uniqueItems = new();
        
        [Button]
        private void SpawnUniqueItem(UniqueItemData data)
        {
            UniqueItem newItem = new UniqueItem(data);
            uniqueItems.Add(newItem);
        }
        
        public static List<UniqueItem> GetAllUniqueItems()
        {
            return Instance.uniqueItems;
        }
        
        public static List<UniqueItem> GetUniqueItemsByType(UniqueItemType type)
        {
            return Instance.uniqueItems.FindAll(item => item.Type == type);
        }
    }
}