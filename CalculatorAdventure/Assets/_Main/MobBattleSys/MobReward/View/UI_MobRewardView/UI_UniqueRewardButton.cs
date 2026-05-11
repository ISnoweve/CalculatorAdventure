using System.Collections;
using System.Collections.Generic;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using _Main.UniqueItemSys.Manager;
using MessagePipe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView
{
    public class UI_UniqueRewardButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private Button rewardButton;
        [SerializeField] private int uniqueItemRewardId;
        [SerializeField] private float longPressSeconds = 0.5f;
        private bool isPointerDown;

        private Coroutine longPressCoroutine;
        private bool longPressTriggered;
        private bool suppressNextClick;

        private void Awake()
        {
            rewardButton = GetComponent<Button>();
            rewardButton.onClick.AddListener(OnButtonClick);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            longPressTriggered = false;

            if (longPressCoroutine != null)
                StopCoroutine(longPressCoroutine);

            longPressCoroutine = StartCoroutine(LongPressRoutine());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerDown = false;

            if (longPressCoroutine != null)
            {
                StopCoroutine(longPressCoroutine);
                longPressCoroutine = null;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;

            if (longPressCoroutine != null)
            {
                StopCoroutine(longPressCoroutine);
                longPressCoroutine = null;
            }

            if (longPressTriggered)
            {
                suppressNextClick = true;
                UI_MobRewardView.Instance.OnPointLeftUniqueItem();
            }
        }

        public void SetUniqueItemReward(int uniqueItemId)
        {
            uniqueItemRewardId = uniqueItemId;
            rewardButton.image.sprite = UniqueItemManager.GetUniqueItemById(uniqueItemRewardId).Icon;
        }

        private IEnumerator LongPressRoutine()
        {
            yield return new WaitForSeconds(longPressSeconds);

            if (!isPointerDown) yield break;

            longPressTriggered = true;
            suppressNextClick = true;
            UI_MobRewardView.Instance.OnPointEnterUniqueItem(uniqueItemRewardId);
        }

        private void OnButtonClick()
        {
            if (suppressNextClick)
            {
                suppressNextClick = false;
                return;
            }

            UI_MobRewardView.Instance.CloseRewardPanel();
            var data = new ChooseUniqueReward(uniqueItemRewardId);
            GlobalMessagePipe.GetPublisher<ChooseUniqueReward>().Publish(data);
        }
    }
}