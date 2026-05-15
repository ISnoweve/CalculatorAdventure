using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.HealthSys.Sys.Event;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.HealthSys.Sys
{
    [Serializable]
    public class HealthSystem : Singleton<HealthSystem>
    {
        [SerializeField] private int maxHealth=150;
        [SerializeField,ReadOnly] private int currentHealth;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        #region Life Cycle

        protected override void Initialize()
        {
            base.Initialize();
            SubscribeEvent();
        }

        private IDisposable _disposable;
        
        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<NotifySetMobBattle>().Subscribe(SetHealth).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion

        #region Initialization

        private void SetHealth(NotifySetMobBattle eventData)
        {
            currentHealth = maxHealth;
            Event_InitialHealth data = new Event_InitialHealth(maxHealth, currentHealth);
            GlobalMessagePipe.GetPublisher<Event_InitialHealth>().Publish(data);
        }

        #endregion

        #region Event Behaviour

        public void ModifyPlayerHealthByMobAttack(List<CalculatorButton> buttons)
        {
            if(buttons.Count <= 0)return;

            int modifyValue = 0;
            
            foreach (CalculatorButton calculatorButton in buttons)
            {
                if(calculatorButton.IsClick)continue;
                modifyValue += calculatorButton.CurrentValue;
            }
            
            ModifyHealth(-modifyValue);
        }

        #endregion

        #region Modify

        private void ModifyHealth(int value)
        {
            currentHealth += value;
            if(DetectHealthEmpty())
            {
                HealthEmpty();
                return;
            }
            HealthUpdate();
           
        }

        private bool DetectHealthEmpty()
        {
            return currentHealth <= 0;
        }

        private void HealthEmpty()
        {
            Event_HealthEmpty eventData = new Event_HealthEmpty();
            GlobalMessagePipe.GetPublisher<Event_HealthEmpty>().Publish(eventData);
        }
        
        private void HealthUpdate()
        {
            Event_HealthUpdate eventData = new Event_HealthUpdate(currentHealth);
            GlobalMessagePipe.GetPublisher<Event_HealthUpdate>().Publish(eventData);
        }

        public void SetHealth(int value)
        {
           currentHealth = value;
        }

        #endregion
    }
}