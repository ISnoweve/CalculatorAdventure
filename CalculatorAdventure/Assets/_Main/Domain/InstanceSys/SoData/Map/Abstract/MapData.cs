using _Main.Domain.InstanceSys.SoData.Abstract;
using _Main.Domain.InstanceSys.SoData.Interface;
using Sirenix.OdinInspector;

namespace _Main.Domain.InstanceSys.SoData.Map.Abstract
{
    //[Serializable]
    public abstract class MapData<TDataId, TDataBase>
        where TDataId : ISoDataId
        where TDataBase : SoDataBase
    {
        [HorizontalGroup(0.3f),HideLabel,LabelWidth(20)]
        public TDataId dataId;

        [HorizontalGroup(0.7f),LabelText("Data"),LabelWidth(35)]
        public TDataBase dataBase;
    }
}