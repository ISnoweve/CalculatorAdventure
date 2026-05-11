using System;
using System.Collections;
using _Main.MobSys.Data.Mob.AttackSkills.Event;
using _Main.MobSys.Manager.Event;
using _Main.MobSys.Sys.MobSys.Event;
using _Main.MobSys.View.UI_Mob.Runtime;
using _Main.MobSys.View.UI_Mob.Runtime.Enum;
using _Main.MobSys.View.UI_Mob.Runtime.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine;

namespace _Main.MobSys.View.UI_Mob.Control
{
    public class MobViewControl : SingletonMonoBehaviour<MobViewControl>
    {
        [SerializeField] private UI_MobView uiMobView;
        [SerializeField] private UI_MobQuestionNumber uiMobQuestionNumber;
        [SerializeField] private UI_MobExtraMission uiMobExtraMission;
        [SerializeField] private UI_MobAtkSkillCountDown uiMobAtkSkillCountDown;
        [SerializeField] private UI_MobAtkSkillDescription uiMobAtkSkillDescription;
        [SerializeField] private UI_MobGetDefeated uiMobGetDefeated;
        
        #region Life cycle

        protected override void Awake()
        {
            base.Awake();
            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<SpawnMobEvent>().Subscribe(UpdateNewMobView).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Calculate_UpdateMobQuestionNumber>().Subscribe(UpdateMobQuestionNumber)
                .AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Calculate_MobDefeated>().Subscribe(UpdateMobDefeated).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<MobTurn_UpdateBehaviourNumber>().Subscribe(UpdateMobBehaviourCountDown)
                .AddTo(bag);
            GlobalMessagePipe.GetSubscriber<MobTurn_SetMobNewBehaviour>().Subscribe(SetNewMobBehaviour).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_AtkS_Recover_TakeCalculatorButtonsMultiply>()
                .Subscribe(RecoverByButtonsMultiply).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_AtkS_Recover_TakeCalculatorButtonsAddOrSubtract>()
                .Subscribe(RecoverByButtonsAddOrSubtract).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_AtkS_Recover_Multiply>().Subscribe(RecoverByMultiply).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_AtkS_Recover_AddOrSubtract>().Subscribe(RecoverByAddOrSubtract)
                .AddTo(bag);
            GlobalMessagePipe.GetSubscriber<UniqueItem_UpdateMobQuestionNumber>().Subscribe(UpdateMobQuestionNumber).
                AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion
        
        #region InitialView

        private void UpdateNewMobView(SpawnMobEvent data)
        {
            uiMobView.Initialize(data.Mob);
            uiMobQuestionNumber.Initialize(data.Mob.CurrentQuestionNumber);
            uiMobAtkSkillCountDown.Initialize(data.Mob.NextAttackSkill);
            uiMobAtkSkillDescription.SetDescription(data.Mob.NextAttackSkill.Description);
        }

        #endregion

        #region Update By MobQuestionNumber Calculate Result

        private void UpdateMobQuestionNumber(Calculate_UpdateMobQuestionNumber data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.QuestionNumber,FinishUpdateNumberByCalculateResult);
        }

        private void UpdateMobQuestionNumber(UniqueItem_UpdateMobQuestionNumber data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.QuestionNumber,FinishUpdateNumberByUniqueItem);
        }
        
        private void FinishUpdateNumberByUniqueItem()
        {
            var finishedUpdateNewNumber = new FinishedUpdateNewNumber(FinishedUpdateNewNumberType.ByUniqueItem);
            GlobalMessagePipe.GetPublisher<FinishedUpdateNewNumber>().Publish(finishedUpdateNewNumber);
        }
        
        private void FinishUpdateNumberByCalculateResult()
        {
            var finishedUpdateNewNumber = new FinishedUpdateNewNumber(FinishedUpdateNewNumberType.ByCalculateResult);
            GlobalMessagePipe.GetPublisher<FinishedUpdateNewNumber>().Publish(finishedUpdateNewNumber);
        }

        #endregion

        #region Update Mob Behaviour 

        private void SetNewMobBehaviour(MobTurn_SetMobNewBehaviour data)
        {
            uiMobAtkSkillCountDown.Initialize(data.AtkSData);
            uiMobAtkSkillDescription.SetDescription(data.AtkSData.Description);
            StartCoroutine(Stay());
        }
        
        private void UpdateMobBehaviourCountDown(MobTurn_UpdateBehaviourNumber data)
        {
            uiMobAtkSkillCountDown.UpdateNewCountDown(data.MobAttackSkillCountDown);
            StartCoroutine(Stay());
        }
        
        private IEnumerator Stay()
        {
            yield return new WaitForSeconds(1f);
            var finishedUpdateBehaviourCountDown = new FinishedUpdateBehaviourCountDown();
            GlobalMessagePipe.GetPublisher<FinishedUpdateBehaviourCountDown>()
                .Publish(finishedUpdateBehaviourCountDown);
        }

        #endregion
        
        #region Update Mob By AttackSkill

        private void RecoverByButtonsMultiply(Event_AtkS_Recover_TakeCalculatorButtonsMultiply data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber,FinishUpdateNumberByAttackSkillRecover);
        }

        private void RecoverByButtonsAddOrSubtract(Event_AtkS_Recover_TakeCalculatorButtonsAddOrSubtract data)
        {
            var index = 0;
            foreach (var variable in data.TakeButtons) index += variable.CurrentValue;
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber,FinishUpdateNumberByAttackSkillRecover);
        }

        private void RecoverByMultiply(Event_AtkS_Recover_Multiply data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber,FinishUpdateNumberByAttackSkillRecover);
        }

        private void RecoverByAddOrSubtract(Event_AtkS_Recover_AddOrSubtract data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber,FinishUpdateNumberByAttackSkillRecover);
        }
        
        private void FinishUpdateNumberByAttackSkillRecover()
        {
            var finishedUpdateNewNumber = new FinishedUpdateNewNumber(FinishedUpdateNewNumberType.ByAttackSkillRecover);
            GlobalMessagePipe.GetPublisher<FinishedUpdateNewNumber>().Publish(finishedUpdateNewNumber);
        }

        #endregion
        
        #region Mob Defeated

        private void UpdateMobDefeated(Calculate_MobDefeated data)
        {
            // 更新  mob 被打倒的圖片
        }

        #endregion
    }
}