using System;
using UnityEngine;

namespace _Main.UniqueItemSys.Data
{
    [CreateAssetMenu(fileName = "UniqueItemDataSoList", menuName = "SoSetting/UniqueItem/UniqueItemDataSoList", order = 0)]
    public class UniqueItemDataSoList : ScriptableObject
    {
        [SerializeField] private UniqueItemData[] uniqueItemDataArray;
        public UniqueItemData[] UniqueItemDataArray => uniqueItemDataArray;
    }
}