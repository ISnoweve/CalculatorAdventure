using System;
using _Main.MoneySys.Sys;
using _Main.MoneySys.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.PickerWheelUI.GameScripts
{
    public class ButtonToSpin : MonoBehaviour
    {
        [SerializeField] private int spinCost;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text warningNotEnoughMoney;
        [SerializeField] private string warningText;
        [SerializeField] private PickerWheel _pickerWheel;
        [SerializeField] private MoneyView view;

        private void Awake()
        {
            _button.onClick.AddListener(() => SpinWheel());
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void SpinWheel()
        {
            if (TryGetMoney() == false)
            {
                ShowWarningText();
                return;
            }

            _button.interactable = false;
            _pickerWheel.OnSpinEnd(_ => _button.interactable = true);
            _pickerWheel.Spin();
            view.UpdateMoneyValue(MoneySystem.Instance.MoneyValue);
        }

        private bool TryGetMoney()
        {
            return MoneySystem.Instance.TryGetMoney(spinCost);
        }

        private void ShowWarningText()
        {
            warningNotEnoughMoney.text = warningText;
            warningNotEnoughMoney.color = Color.red;
        }
    }
}