using _Main.Domain.InstanceSys.Factory.Abstract;
using _Main.Domain.InstanceSys.SoData.Abstract;
using _Main.Domain.InstanceSys.SoData.Interface;
using _Main.Domain.InstanceSys.SoData.Map.Abstract;

namespace _Main.Domain.InstanceSys.Manager.Abstract
{
    public abstract class BaseManger<TFactory,TRunTime,TBaseMap,TMapData, TSoDataId, TSoDataBase>
        where TFactory : BaseFactory<TRunTime,TBaseMap,TMapData, TSoDataId, TSoDataBase>
        where TRunTime : RunTimeData
        where TBaseMap : BaseMap<TMapData, TSoDataId, TSoDataBase>
        where TMapData : MapData<TSoDataId, TSoDataBase>
        where TSoDataId : ISoDataId
        where TSoDataBase : SoDataBase
    {
        
    }
}