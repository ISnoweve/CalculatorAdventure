using _Main.UniqueItemSys.Data.Enum;
using UnityEngine;

namespace _Main.UniqueItemSys.Data.EffectData.Base
{
    public abstract class EffectBaseData : ScriptableObject
    {
        public abstract void ExecuteTrigger();
    }
}