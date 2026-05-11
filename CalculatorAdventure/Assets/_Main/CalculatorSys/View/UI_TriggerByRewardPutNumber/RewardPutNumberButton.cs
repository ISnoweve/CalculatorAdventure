using _Main.CalculatorSys.View.UI_TriggerByRewardPutNumber.Event;
using MessagePipe;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.CalculatorSys.View.UI_TriggerByRewardPutNumber
{
    public class RewardPutNumberButton : MonoBehaviour
    {
        public byte index;
        [SerializeField] private Button button;
        private void Awake()
        {
            SubscribeEvent();
        }
        
        private void SubscribeEvent()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            RemoveEvent();
        }
        
        private void RemoveEvent()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
        
        private void OnButtonClick()
        {
            Event_PutNumber eventPutNumber = new Event_PutNumber(index);
            GlobalMessagePipe.GetPublisher<Event_PutNumber>().Publish(eventPutNumber);
        }
    }
}