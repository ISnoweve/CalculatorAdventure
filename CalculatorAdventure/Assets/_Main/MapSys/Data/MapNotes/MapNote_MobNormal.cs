using _Main.MobSys.Data;
using UnityEngine;

namespace _Main.MapSys.Data.MapNotes
{
    [CreateAssetMenu(fileName = "MapNote_MobNormal",
        menuName = "SoSetting/Map/AdjustCalculatorButton_Random", order = 1)]
    public class MapNote_MobNormal : MapNoteBaseData
    {
        [SerializeField] private MobData _mobData;
        
        public override void TriggerMapNote()
        {
            throw new System.NotImplementedException();
        }
    }
}