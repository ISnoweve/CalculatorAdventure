using _Main.MapSys.Data.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MapSys.Data.Base
{
    public abstract class MapNoteBaseData : ScriptableObject
    {
        [Title("ID")][SerializeField] private int id;
        [SerializeField] MapNoteType mapNoteType;

        public abstract void TriggerMapNote();
    }
}
