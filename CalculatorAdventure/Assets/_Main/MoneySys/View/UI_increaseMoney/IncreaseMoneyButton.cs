using System;
using _Main.MoneySys.Sys;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Main.MobBattleSys.MobReward.View.UI_increaseMoney
{
    public class IncreaseMoneyButton : MonoBehaviour
    {
        [SerializeField] private int giveMoneyValue; 
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(IncreaseMoney);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        private void IncreaseMoney()
        {
            MoneySystem.Instance.GiveMoney(giveMoneyValue);
        }
    }
}