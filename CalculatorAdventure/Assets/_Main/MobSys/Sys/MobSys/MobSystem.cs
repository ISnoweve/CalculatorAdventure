using System;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobSys.Manager;
using _Main.MobSys.Sys.MobSys.Event;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data.EffectData.Event;
using MessagePipe;
using Sirenix.OdinInspector;

namespace _Main.MobSys.Sys.MobSys
{
    [Serializable]
    public class MobSystem : Singleton<MobSystem>
    {
        #region life cycle

        protected override void Initialize()
        {
            SubscribeEvent();
            base.Initialize();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<CalculateResultNotify>().Subscribe(CalculatePlayerSendResult).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(MobBehaviorTurn).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_ExecuteCalculateToMobBeforePlayerRound>().Subscribe(UniqueItemModifyQuestionNumber).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion

        #region Mob Round Behaviour

        private void MobBehaviorTurn(NotifyMobBattleNewState data)
        {
            if (data.NewState != MobBattleStateEnum.MobTurn) return;
            DecreaseMobBehaviorCountDown();
            if (DetectMobBehaviorCountDown() == false) return;
            MobExecuteBehaviour();
            RandomMobNewBehavior();
        }

        private void DecreaseMobBehaviorCountDown()
        {
            MobManager.CurrentsMob.DecreaseAttackSkillCountDown();
        }

        private bool DetectMobBehaviorCountDown()
        {
            if (MobManager.CurrentsMob.AttackSkillCountDown <= 0) return true;

            var mobTurnDecreaseNumber = new MobTurn_UpdateBehaviourNumber(MobManager.CurrentsMob.AttackSkillCountDown);
            GlobalMessagePipe.GetPublisher<MobTurn_UpdateBehaviourNumber>().Publish(mobTurnDecreaseNumber);
            return false;
        }

        private void MobExecuteBehaviour()
        {
            MobManager.CurrentsMob.ExecuteNextAttackSkill();
        }

        [Button]
        private void RandomMobNewBehavior()
        {
            MobManager.CurrentsMob.RandomNextAttackSkill();
            var mobTurnSetMobNewBehaviour = new
                MobTurn_SetMobNewBehaviour(MobManager.CurrentsMob.NextAttackSkill,
                    MobManager.CurrentsMob.AttackSkillCountDown);
            GlobalMessagePipe.GetPublisher<MobTurn_SetMobNewBehaviour>().Publish(mobTurnSetMobNewBehaviour);
        }

        #endregion

        #region Modify Mob Question Number By other System

        private void UniqueItemModifyQuestionNumber(Event_ExecuteCalculateToMobBeforePlayerRound data)
        {
            MobManager.CurrentsMob.ModifyQuestionNumber(data.ModifyNumber, data.CalculatorOperator);
            var calculateUpdateMobNumber = new UniqueItem_UpdateMobQuestionNumber(
                    MobManager.CurrentsMob.CurrentQuestionNumber, data.ModifyNumber, data.CalculatorOperator);
            GlobalMessagePipe.GetPublisher<UniqueItem_UpdateMobQuestionNumber>().Publish(calculateUpdateMobNumber);
        }
        
        private void CalculatePlayerSendResult(CalculateResultNotify data)
        {
            MobManager.CurrentsMob.ModifyQuestionNumber(data.Result, data.FirstOperator);
            var calculateUpdateMobNumber = new Calculate_UpdateMobQuestionNumber(
                MobManager.CurrentsMob.CurrentQuestionNumber, data.Result, data.FirstOperator);
            GlobalMessagePipe.GetPublisher<Calculate_UpdateMobQuestionNumber>().Publish(calculateUpdateMobNumber);
        }

        #endregion
    }
}