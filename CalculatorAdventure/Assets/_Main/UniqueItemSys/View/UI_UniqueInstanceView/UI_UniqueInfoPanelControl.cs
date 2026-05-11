using _Main.ToolKit.SingletonFeature;
using UnityEngine;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event
{
    public class UI_UniqueInfoPanelControl : SingletonMonoBehaviour<UI_UniqueInfoPanelControl>
    {
        protected override bool IsDontDestroyOnLoad => true;
        
        [SerializeField] private UI_UniqueInfoPanel uniqueInfoPanel;
        public void OnPointEnterUniqueItem(int uniqueItemId)
        {
            uniqueInfoPanel.ShowUniqueRewardInfo(uniqueItemId);
        }
        
        public void OnPointLeftUniqueItem()
        {
            uniqueInfoPanel.HideUniqueRewardInfo();
        }
    }
}