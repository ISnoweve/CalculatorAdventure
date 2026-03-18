using _Main.MobSys.Data;
using UnityEngine;

namespace _Main.MapSys.Data.MapNotes
{
    public class MapNote_MobElite : MapNoteBaseData
    {
        [SerializeField] private MobData _mobData;
        
        public override void TriggerMapNote()
        {
            throw new System.NotImplementedException();
        }
    }
}