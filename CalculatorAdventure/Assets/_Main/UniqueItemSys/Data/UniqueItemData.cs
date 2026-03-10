using _Main.UniqueItemSys.Data.EffectData.Base;
using _Main.UniqueItemSys.Data.Enum;
using UnityEngine;

namespace _Main.UniqueItemSys.Data
{
    [CreateAssetMenu(fileName = "UniqueItemData", menuName = "SoSetting/UniqueItem/UniqueItemData", order = 0)]
    public class UniqueItemData : ScriptableObject
    {
        [SerializeField] private UniqueItemTriggerType triggerType;
        [SerializeField] private UniqueItemEffectBase effectBase;

        public UniqueItemTriggerType TriggerType => triggerType;
        public UniqueItemEffectBase EffectBase => effectBase;

        public void ExecuteTrigger()
        {
            effectBase.ExecuteEffect();
        }
    }
}