using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.UniqueItemSys.Data
{
    [CreateAssetMenu(fileName = "UniqueItemDataSoList", menuName = "SoSetting/UniqueItem/UniqueItemDataSoList", order = 0)]
    public class UniqueItemDataSoList : ScriptableObject
    {
        [SerializeField] private List<UniqueItemData> uniqueItemDataList;
        public List<UniqueItemData> UniqueItemDataList => uniqueItemDataList;
        
        [SerializeField] private List<UniqueItemData> IncreaseCalculatorBox;
        public List<UniqueItemData> IncreaseCalculatorBoxList => IncreaseCalculatorBox;
    }
}