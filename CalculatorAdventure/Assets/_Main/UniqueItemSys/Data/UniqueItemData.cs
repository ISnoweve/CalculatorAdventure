using _Main.UniqueItemSys.Data.Enum;
using UnityEngine;

namespace _Main.UniqueItemSys.Data
{
    //[CreateAssetMenu(fileName = "UniqueItemData", menuName = "SoSetting/UniqueItem/UniqueItemData", order = 0)]
    public abstract class UniqueItemData : ScriptableObject
    {
        [SerializeField] private UniqueItemTriggerType triggerType;
        public UniqueItemTriggerType TriggerType => triggerType;

        public abstract void ExecuteTrigger();
    }
}