using System.Collections;
using System.Net.NetworkInformation;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using _Main.UniqueItemSys.Manager;
using _Main.UniqueItemSys.Manager.Runtime;
using MessagePipe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Main.UniqueItemSys.View.UI_UniqueInstanceView
{
    public class UI_UniqueIstanceView : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private int id;
        [SerializeField] private Image icon;
        [SerializeField] private Button button;
        
        [SerializeField] private float longPressSeconds = 0.5f;
        private bool isPointerDown;
        private Coroutine longPressCoroutine;
        private bool longPressTriggered;
        
        public void SetView(int index)
        {
            id = index;
            ChangeIcon(id);
        }
        
        private void ChangeIcon(int index)
        {
            UniqueItem item = UniqueItemManager.GetUniqueItemById(index);
            if(item == null || item.Icon == null)return;
            icon.sprite = item.Icon;
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
        
        private IEnumerator LongPressRoutine()
        {
            yield return new WaitForSeconds(longPressSeconds);

            if (!isPointerDown) yield break;

            longPressTriggered = true;
            UI_UniqueInfoPanelControl.Instance.OnPointEnterUniqueItem(id);
        }
    }
}