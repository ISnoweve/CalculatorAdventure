using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Main.Test
{
    public class TestAnimation : MonoBehaviour
    {
        public TMP_Text text;
        public List<int> list = new();
        public float animationDuration = 1f;
        public bool isAnimating = false;
        [SerializeField] private Vector3 impactAnimationStrength = new Vector3(10f, 10f, 0f);
        
        private Coroutine animationCoroutine;
        
        [Button]
        public void AddList(int num)
        {
            list.Add(num);
            UpdateAnimation();
        }

        private void Awake()
        {
            AddList(6);
            AddList(5);
            AddList(4);
            AddList(3);
            AddList(2);
        }

        private void UpdateAnimation()
        {
            if(animationCoroutine != null)return;
            animationCoroutine = StartCoroutine(AnimationUpdateText());
            Debug.Log("Start Animation");
        }

        IEnumerator AnimationUpdateText()
        {
            text.text = list[0].ToString();
            text.transform.DOShakePosition(animationDuration, impactAnimationStrength);
            list.RemoveAt(0);
            yield return new WaitForSeconds(animationDuration);
            animationCoroutine = null;
            if (CheckAnimationZero())
            {
                StartCoroutine(AnimationUpdateText());
            }
            else
            {
                text.text="NONE";
            }
        }

        private bool CheckAnimationZero()
        {
            return list.Count >= 1;
        } 
    }
}