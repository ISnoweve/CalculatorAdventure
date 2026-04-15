using _Main.UniqueItemSys.Data.EffectData.Base;
using _Main.UniqueItemSys.Data.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.UniqueItemSys.Data
{
    [CreateAssetMenu(fileName = "UniqueItemData", menuName = "SoSetting/UniqueItem/UniqueItemData", order = 0)]
    public class UniqueItemData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string dataName;
        [SerializeField] private string description;
        [SerializeField] private EffectBaseData effectData;
        [SerializeField] private UniqueItemType type;
        [SerializeField] private bool increaseCalculatorBox;
        
        [Title("View")]
        [SerializeField] private Sprite icon;
        
        public int Id => id;
        public string DataName => dataName;
        public string Description => description;
        public EffectBaseData EffectData => effectData;
        public UniqueItemType Type => type;
        public bool IncreaseCalculatorBox => increaseCalculatorBox;
        public Sprite Icon => icon;
    }
}