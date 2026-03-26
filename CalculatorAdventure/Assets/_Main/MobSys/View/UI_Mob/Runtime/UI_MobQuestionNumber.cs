using TMPro;
using UnityEngine;

namespace _Main.MobBattleSys.View.UI_Mob.Runtime
{
    public class UI_MobQuestionNumber : MonoBehaviour
    {
        [SerializeField] private TMP_Text questionNumberText;
        [SerializeField] private int currentQuestionNumber;

        #region Life Cycle

        public void Initialize(int questionNumber)
        {
            InitializeCurrentQuestionNumber(questionNumber);
        }

        #endregion

        #region Behaviour

        private void UpdateQuestionNumberText()
        {
            questionNumberText.text = currentQuestionNumber.ToString();
        }

        private void InitializeCurrentQuestionNumber(int questionNumber)
        {
            currentQuestionNumber = questionNumber;
            UpdateQuestionNumberText();
        }

        public void UpdateNewQuestionNumber(int questionNumber)
        {
            InitializeCurrentQuestionNumber(questionNumber);
        }

        #endregion
    }
}