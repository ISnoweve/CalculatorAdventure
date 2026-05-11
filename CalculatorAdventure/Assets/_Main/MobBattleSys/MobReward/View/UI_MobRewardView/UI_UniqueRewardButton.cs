using System.Collections;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using _Main.UniqueItemSys.Manager;
using MessagePipe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Main.MobBattleSys.MobReward.View.UI_MobRewardView
{
    public class UI_UniqueRewardButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Button rewardButton;
        [SerializeField] private int uniqueItemRewardId;
        
        [SerializeField] private float longPressSeconds = 0.5f;
        private bool isPointerDown;
        private Coroutine longPressCoroutine;
        private bool longPressTriggered;

        private void Awake()
        {
            if (rewardButton == null) rewardButton = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            longPressTriggered = false;

            if (longPressCoroutine != null) StopCoroutine(longPressCoroutine);
            longPressCoroutine = StartCoroutine(LongPressRoutine());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (longPressTriggered)
            {
                UI_UniqueInfoPanelControl.Instance.OnPointLeftUniqueItem();
            }

            isPointerDown = false;
            if (longPressCoroutine != null)
            {
                StopCoroutine(longPressCoroutine);
                longPressCoroutine = null;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isPointerDown && longPressTriggered)
            {
                UI_UniqueInfoPanelControl.Instance.OnPointLeftUniqueItem();
            }

            isPointerDown = false;
            if (longPressCoroutine != null)
            {
                StopCoroutine(longPressCoroutine);
                longPressCoroutine = null;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (longPressTriggered)
            {
                longPressTriggered = false; 
                return;
            }

            ExecuteClickLogic();
        }

        private void ExecuteClickLogic()
        {
            UI_MobRewardView.Instance.CloseRewardPanel();
            var data = new ChooseUniqueReward(uniqueItemRewardId);
            GlobalMessagePipe.GetPublisher<ChooseUniqueReward>().Publish(data);
        }

        private IEnumerator LongPressRoutine()
        {
            yield return new WaitForSeconds(longPressSeconds);

            if (!isPointerDown) yield break;

            longPressTriggered = true;
            UI_UniqueInfoPanelControl.Instance.OnPointEnterUniqueItem(uniqueItemRewardId);
        }

        public void SetUniqueItemReward(int uniqueItemId = 0)
        {
            uniqueItemRewardId = uniqueItemId;
            if (uniqueItemId == 0)
            {
                rewardButton.interactable = false;
                return;
            }
            var item = UniqueItemManager.GetUniqueItemById(uniqueItemRewardId);
            if (item != null)rewardButton.image.sprite = item.Icon;
        }
    }
}