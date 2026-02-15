using System;
using MessagePipe;
using TMPro;
using UnityEngine;

namespace _Main.CalculatorSys.View
{
    public class CalculatorLabel : MonoBehaviour
    {
        [Header("UI 元件實例")]
        [SerializeField] private TMP_Text operatorText;
        [SerializeField] private TMP_Text numberText;

        [Header("設定")]
        [SerializeField] private string defaultNumber = "0";

        #region Life cycle

        private void Awake()
        {
            ClearDisplay();
        }

        private IDisposable _disposable;
        
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            
            DisposableBagBuilder bag = DisposableBag.CreateBuilder();
        }

        #endregion
        
        private void ClearDisplay()
        {
            if (operatorText != null) operatorText.text = "\u25a1";
            if (numberText != null) numberText.text = "\u25a1";
        }
    }
}