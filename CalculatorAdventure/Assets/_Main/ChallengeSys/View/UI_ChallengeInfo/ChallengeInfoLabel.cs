using System;
using _Main.ChallengeSys.Enum;
using _Main.ChallengeSys.Sys.Event;
using MessagePipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.ChallengeSys.View.UI_ChallengeInfo
{
    public class ChallengeInfoLabel : MonoBehaviour
    {
        [SerializeField] private GameObject descriptionPanel;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text toGoalTextInside;
        [SerializeField] private TMP_Text toGoalTextOutside;
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] private Button buttonShowPanel;
        [SerializeField] private Button buttonHidePanel;
        [SerializeField] [TextArea] private string divideRewardText;
        [SerializeField] [TextArea] private string multiplyRewardText;
        [SerializeField] [TextArea] private string divideAndUultiplyRewardText;

        #region Life Cycle

        private void Awake()
        {
            descriptionPanel.SetActive(false);
            buttonShowPanel.onClick.AddListener(ShowDescriptionPanel);
            buttonHidePanel.onClick.AddListener(HideDescriptionPanel);
            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<ChallengeToGoalUpdate>().Subscribe(UpdateToGoalCount).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ChallengeSuccess>().Subscribe(UpdateChallengeSuccess).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ChallengeNew>().Subscribe(UpdateNewChallenge).AddTo(bag);
            _disposable = bag.Build();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
            buttonShowPanel.onClick.RemoveListener(ShowDescriptionPanel);
            buttonHidePanel.onClick.RemoveListener(HideDescriptionPanel);
        }

        #endregion

        #region Behaviour

        private void ShowDescriptionPanel()
        {
            descriptionPanel.SetActive(true);
        }

        private void HideDescriptionPanel()
        {
            descriptionPanel.SetActive(false);
        }

        private void UpdateToGoalCount(ChallengeToGoalUpdate data)
        {
            toGoalTextInside.text = data.CurrentToGoalCount + "/" + data.CurrentGoalCount;
            toGoalTextOutside.text = data.CurrentToGoalCount + "/" + data.CurrentGoalCount;
        }

        private void UpdateNewChallenge(ChallengeNew data)
        {
            toGoalTextInside.text = 0 + "/" + data.ChallengeData.toGoalCount;
            toGoalTextOutside.text = 0 + "/" + data.ChallengeData.toGoalCount;
            descriptionText.text = data.ChallengeData.challengeDescription;
            UpdateRewardText(data.ChallengeData.challengeReward);
        }

        private void UpdateRewardText(ChallengeReward challengeReward)
        {
            if (challengeReward.HasFlag(ChallengeReward.Divide) && challengeReward.HasFlag(ChallengeReward.Multiply))
                rewardText.text = divideAndUultiplyRewardText;

            if (challengeReward.HasFlag(ChallengeReward.Multiply)) rewardText.text = multiplyRewardText;

            if (challengeReward.HasFlag(ChallengeReward.Divide)) rewardText.text = divideRewardText;
        }

        private void UpdateChallengeSuccess(ChallengeSuccess data)
        {
        }

        #endregion
    }
}