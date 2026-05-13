using System;
using System.Collections;
using System.Collections.Generic;
using _Main.MobSys.Sys.MobSys.Event;
using DG.Tweening;
using MessagePipe;
using TMPro;
using UnityEngine;

namespace _Main.MobSys.View.UI_Mob.Runtime
{
    public class UI_MobQuestionNumber : MonoBehaviour
    {
        [SerializeField] private TMP_Text questionNumberText;
        [SerializeField] private int currentQuestionNumber;
        
        [SerializeField] private List<int> modifyQueue = new();
        
        [SerializeField] private float impactAnimationDuration = 1f;
        [SerializeField] private Vector3 impactAnimationStrength = new(5f, 5f, 0f);
        private Coroutine animationCoroutine;

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

        public void UpdateNewQuestionNumber(int questionNumber,Action onFinish = null)
        {
            modifyQueue.Add(questionNumber);
            if (animationCoroutine != null)
            {
                return;
            }
            animationCoroutine = StartCoroutine(UpdateNewQuestionNumberAnimation(onFinish));
        }

        private IEnumerator UpdateNewQuestionNumberAnimation(Action onFinish = null)
        {
            currentQuestionNumber = modifyQueue[0];
            modifyQueue.RemoveAt(0);
            UpdateQuestionNumberText();
            questionNumberText.transform.DOShakePosition(impactAnimationDuration, impactAnimationStrength);
            yield return new WaitForSeconds(impactAnimationDuration);
            animationCoroutine = null;
            
            if (CheckModifyQueue())
            {
                StartCoroutine(UpdateNewQuestionNumberAnimation(onFinish));
            }
            else
            {
                if (currentQuestionNumber == 0)
                {
                    var calculateMobDefeated = new Calculate_MobDefeated();
                    GlobalMessagePipe.GetPublisher<Calculate_MobDefeated>().Publish(calculateMobDefeated);
                }
                else
                {
                    onFinish?.Invoke();
                }
            }
        }
        
        private bool CheckModifyQueue()
        {
            return modifyQueue.Count >= 1;
        }
        #endregion
    }
}