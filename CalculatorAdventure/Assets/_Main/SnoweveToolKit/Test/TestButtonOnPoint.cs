using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Main.Test
{
    public class TestButtonOnPoint : MonoBehaviour, ISelectHandler , IDeselectHandler
    {
        public GameObject panel;
        public Button button;

        private void Awake()
        {
            panel.SetActive(false);
            button.onClick.AddListener(()=>Debug.Log("asdasd"));
        }

        public void OnSelect(BaseEventData eventData)
        {
            panel.SetActive(true);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            Debug.Log("asd");
            panel.SetActive(false);
        }
    }
}