using _Main.Domain.InstanceSys.SoData.Abstract;
using _Main.Domain.InstanceSys.SoData.Interface;
using UnityEngine;

namespace _Main.Domain.InstanceSys.SoData.Map.Abstract
{
//    [CreateAssetMenu(fileName = "MapData", menuName = "000_Map", order = 0)]
    public class BaseMap<TMapData, TSoDataId, TSoDataBase> : ScriptableObject
        where TMapData : MapData<TSoDataId, TSoDataBase>
        where TSoDataId : ISoDataId
        where TSoDataBase : SoDataBase
    {
        public TMapData[] mapData;

        public bool TryGetDataById(TSoDataId id, out TSoDataBase dataBase)
        {
            foreach (var data in mapData)
            {
                if (!data.dataId.Equals(id)) continue;
                dataBase = data.dataBase;
                return true;
            }

            dataBase = null;
            return false;
        }
    }
}