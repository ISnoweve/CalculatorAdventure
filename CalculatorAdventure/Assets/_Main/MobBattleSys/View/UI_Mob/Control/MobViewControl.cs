using System;
using System.Collections;
using _Main.MobBattleSys.Sys.MobSys.Event;
using _Main.MobBattleSys.View.UI_Mob.Runtime;
using _Main.MobBattleSys.View.UI_Mob.Runtime.Event;
using _Main.MobSys.Data.AttackSkills.Event;
using _Main.MobSys.Manager.Event;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using UnityEngine;

namespace _Main.MobBattleSys.View.UI_Mob.Control
{
    public class MobViewControl : SingletonMonoBehaviour<MobViewControl>
    {
        [SerializeField] private UI_MobView uiMobView;
        [SerializeField] private UI_MobQuestionNumber uiMobQuestionNumber;
        [SerializeField] private UI_MobExtraMission uiMobExtraMission;
        [SerializeField] private UI_MobAtkSkillCountDown uiMobAtkSkillCountDown;
        [SerializeField] private UI_MobAtkSkillDescription uiMobAtkSkillDescription;
        [SerializeField] private UI_MobGetDefeated uiMobGetDefeated;

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
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.QuestionNumber);
            var finishedUpdateNewNumber = new FinishedUpdateNewNumber();
            GlobalMessagePipe.GetPublisher<FinishedUpdateNewNumber>().Publish(finishedUpdateNewNumber);
        }

        #endregion

        #region Mob Defeated

        private void UpdateMobDefeated(Calculate_MobDefeated data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(0);
            uiMobGetDefeated.ShowDefeatedPanel();
        }

        #endregion

        #region Update By Mob Behaviour CountDown

        private void UpdateMobBehaviourCountDown(MobTurn_UpdateBehaviourNumber data)
        {
            uiMobAtkSkillCountDown.UpdateNewCountDown(data.MobAttackSkillCountDown);
            StartCoroutine(Stay());
        }

        #endregion

        private IEnumerator Stay()
        {
            yield return new WaitForSeconds(1f);
            var finishedUpdateBehaviourCountDown = new FinishedUpdateBehaviourCountDown();
            GlobalMessagePipe.GetPublisher<FinishedUpdateBehaviourCountDown>()
                .Publish(finishedUpdateBehaviourCountDown);
        }

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
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion

        #region Update Mob By AttackSkill

        private void SetNewMobBehaviour(MobTurn_SetMobNewBehaviour data)
        {
            uiMobAtkSkillCountDown.Initialize(data.AtkSData);
            uiMobAtkSkillDescription.SetDescription(data.AtkSData.Description);
            StartCoroutine(Stay());
        }

        private void RecoverByButtonsMultiply(Event_AtkS_Recover_TakeCalculatorButtonsMultiply data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber);
            Debug.Log("Skill_TakeCalculatorButtonsMultiply:" + data.TakeButtons[0].CurrentValue +
                      "/Right Now Mob Number:" + data.MobNewQuestionNumber +
                      "/Original Mob Number:" + data.MobNewQuestionNumber / data.TakeButtons[0].CurrentValue);
            StartCoroutine(Stay());
        }

        private void RecoverByButtonsAddOrSubtract(Event_AtkS_Recover_TakeCalculatorButtonsAddOrSubtract data)
        {
            var index = 0;
            foreach (var VARIABLE in data.TakeButtons) index += VARIABLE.CurrentValue;
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber);
            Debug.Log("Skill_TakeCalculatorButtonsAddOrSubtract:" + index +
                      "/Right Now Mob Number:" + data.MobNewQuestionNumber +
                      "/Original Mob Number:" + (data.MobNewQuestionNumber - index));
            StartCoroutine(Stay());
        }

        private void RecoverByMultiply(Event_AtkS_Recover_Multiply data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber);
            Debug.Log("Skill_Multiply:" + data.MultiplyValue +
                      "/Right Now Mob Number:" + data.MobNewQuestionNumber +
                      "/Original Mob Number:" + data.MobNewQuestionNumber / data.MultiplyValue);
            StartCoroutine(Stay());
        }

        private void RecoverByAddOrSubtract(Event_AtkS_Recover_AddOrSubtract data)
        {
            uiMobQuestionNumber.UpdateNewQuestionNumber(data.MobNewQuestionNumber);
            Debug.Log("Skill_AddOrSubtract" + data.AddOrSubtractValue +
                      "/Right Now Mob Number:" + data.MobNewQuestionNumber +
                      "/Original Mob Number:" + (data.MobNewQuestionNumber - data.AddOrSubtractValue));
            StartCoroutine(Stay());
        }

        #endregion
    }
}