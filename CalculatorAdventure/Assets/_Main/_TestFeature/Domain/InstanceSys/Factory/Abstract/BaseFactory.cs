using _Main._TestFeature.Domain.InstanceSys.SoData.Abstract;
using _Main._TestFeature.Domain.InstanceSys.SoData.Interface;
using _Main._TestFeature.Domain.InstanceSys.SoData.Map.Abstract;

namespace _Main._TestFeature.Domain.InstanceSys.Factory.Abstract
{
    public abstract class BaseFactory<TRunTime, TBaseMap, TMapData, TSoDataId, TSoDataBase>
        where TRunTime : RunTimeData
        where TBaseMap : BaseMap<TMapData, TSoDataId, TSoDataBase>
        where TMapData : MapData<TSoDataId, TSoDataBase>
        where TSoDataId : ISoDataId
        where TSoDataBase : SoDataBase
    {
        protected TBaseMap _mapData;

        protected abstract void Initialize();

        public abstract TRunTime CreateInstance(TSoDataId dataId);
    }
}