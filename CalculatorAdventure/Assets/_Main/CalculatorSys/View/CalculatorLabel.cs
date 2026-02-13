using TMPro;
using UnityEngine;

namespace _Main.CalculatorSys.View
{
    public class CalculatorLabel : MonoBehaviour
    {
        [Header("UI 元件實例")]
        [SerializeField] private TMP_Text[] operatorText;
        [SerializeField] private TMP_Text[] numberText;

        [Header("設定")]
        [SerializeField] private string defaultNumber = "0";

        private void Awake()
        {
            ClearDisplay();
        }
        public void SetOperator(string newOperator)
        {
            if (operatorText == null) return;
        }
        
        public void AppendNumber(string digit)
        {
            if (numberText == null) return;
        }

        public void ClearDisplay()
        {
            //if (operatorText != null) operatorText.text = "\u25a1";
            //if (numberText != null) numberText.text = "\u25a1";
        }
    }
}