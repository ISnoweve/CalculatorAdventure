using System;
using _Main.HealthSys.Sys;
using _Main.HealthSys.Sys.Event;
using _Main.HealthSys.View.UI_HealthView.Event;
using DG.Tweening;
using MessagePipe;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.HealthSys.View.UI_HealthView
{
    public class HealthBarView : MonoBehaviour
    {
        [Header("UI 元件")]
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private Button testButton;
        [SerializeField] private Slider healthSlider;

        [Header("動畫設定")]
        [SerializeField] private float duration = 0.5f; 
        [SerializeField] private Ease decreaseEase = Ease.OutQuad;

        private Tween _currentTween;

        #region Life Cycle

        private void Awake()
        {
            testButton.onClick.AddListener(SetHealth);
            SubscribeEvent();
        }
        
        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<Event_HealthUpdate>().Subscribe(UpdateHealth).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_HealthEmpty>().Subscribe(UpdateHealth).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_InitialHealth>().Subscribe(Initialize).AddTo(bag);
            _disposable = bag.Build();
        }
        
        private void Initialize(Event_InitialHealth eventData)
        {
            healthText.text = eventData.Health.ToString();
            healthSlider.maxValue = eventData.MaxHealth;
            healthSlider.value = eventData.Health;
        }

        private void OnDestroy()
        {
            testButton.onClick.RemoveListener(SetHealth);
            _disposable?.Dispose();
        }

        #endregion
        
        
        #region Event

        [Button]
        private void SetHealth(int currentHealth,int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        private void UpdateHealth(Event_HealthUpdate eventData)
        {
            UpdateHealth(eventData.Health);
        }

        private void UpdateHealth(Event_HealthEmpty eventData)
        {
            UpdateHealthEmpty();
        }

        #endregion

        #region Logic

        [Button]
        private void UpdateHealth(int newValue)
        {
            _currentTween?.Kill();
            
            _currentTween = DOTween.To(() => healthSlider.value, x => healthSlider.value = x, newValue, duration)
                .SetEase(decreaseEase)
                .OnComplete(() =>
                {
                    _currentTween = null;
                    UpdateText();
            
                    Event_UpdateHealthFinished eventData =  new Event_UpdateHealthFinished();
                    GlobalMessagePipe.GetPublisher<Event_UpdateHealthFinished>().Publish(eventData);
                });
        }

        private void UpdateHealthEmpty()
        {
            _currentTween?.Kill();
            
            _currentTween = DOTween.To(() => healthSlider.value, x => healthSlider.value = x, 0, duration)
                .SetEase(decreaseEase)
                .OnComplete(() =>
                {
                    _currentTween = null;
                    UpdateText();
            
                    Event_UpdateHealthEmpty eventData = new Event_UpdateHealthEmpty();
                    GlobalMessagePipe.GetPublisher<Event_UpdateHealthEmpty>().Publish(eventData);
                });
        }
        
        private void UpdateText()
        {
            healthText.text = ((int)healthSlider.value).ToString();
        }

        #endregion

        #region  Button

        private void SetHealth()
        {
            HealthSystem.Instance.SetHealth((int)healthSlider.maxValue);
            healthSlider.value = HealthSystem.Instance.CurrentHealth;
            UpdateText();
        }

        #endregion
    }
}