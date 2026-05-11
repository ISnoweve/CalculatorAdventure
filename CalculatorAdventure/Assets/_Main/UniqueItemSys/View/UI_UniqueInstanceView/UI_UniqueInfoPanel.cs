using System;
using _Main.UniqueItemSys.Manager;
using _Main.UniqueItemSys.Manager.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView
{
    public class UI_UniqueInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image uniqueRewardIcon;
        [SerializeField] private GameObject uniqueRewardBoard;
        [SerializeField] private TMP_Text uniqueRewardDescriptionText;

        private void Awake()
        {
            uniqueRewardBoard.SetActive(false);
            uniqueRewardDescriptionText.gameObject.SetActive(false);
        }

        public void ShowUniqueRewardInfo(int uniqueItemId)
        {
            uniqueRewardDescriptionText.gameObject.SetActive(true);
            uniqueRewardBoard.SetActive(true);
            UniqueItem data = UniqueItemManager.GetUniqueItemById(uniqueItemId);
            uniqueRewardDescriptionText.text = data.Description;
            uniqueRewardIcon.sprite = data.Icon;
        }
        
        public void HideUniqueRewardInfo()
        {
            uniqueRewardDescriptionText.gameObject.SetActive(false);
            uniqueRewardBoard.SetActive(false);
        }
    }
}