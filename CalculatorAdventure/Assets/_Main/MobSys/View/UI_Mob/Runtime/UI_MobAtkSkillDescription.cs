using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.MobBattleSys.View.UI_Mob.Runtime
{
    public class UI_MobAtkSkillDescription : MonoBehaviour
    {
        [SerializeField] private GameObject descriptionPanel;
        [SerializeField] private Image descriptionImage;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button buttonShowPanel;
        [SerializeField] private Button buttonHidePanel;

        private void Awake()
        {
            descriptionPanel.SetActive(false);
            buttonShowPanel.onClick.AddListener(ShowDescription);
            buttonHidePanel.onClick.AddListener(HideDescription);
        }

        private void OnDestroy()
        {
            buttonShowPanel.onClick.RemoveListener(ShowDescription);
            buttonHidePanel.onClick.RemoveListener(HideDescription);
        }

        public void SetDescription(string description)
        {
            descriptionText.text = description;
        }

        private void ShowDescription()
        {
            descriptionPanel.SetActive(true);
        }

        private void HideDescription()
        {
            descriptionPanel.SetActive(false);
        }
    }
}