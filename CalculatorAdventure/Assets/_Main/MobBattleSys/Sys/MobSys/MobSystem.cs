using System;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobBattleSys.Sys.MobSys.Event;
using _Main.MobSys.Manager;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;

namespace _Main.MobBattleSys.Sys.MobSys
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

        private void RandomMobNewBehavior()
        {
            MobManager.CurrentsMob.RandomNextAttackSkill();
            var mobTurnSetMobNewBehaviour = new
                MobTurn_SetMobNewBehaviour(MobManager.CurrentsMob.NextAttackSkill,
                    MobManager.CurrentsMob.AttackSkillCountDown);
            GlobalMessagePipe.GetPublisher<MobTurn_SetMobNewBehaviour>().Publish(mobTurnSetMobNewBehaviour);
        }

        #endregion

        #region Calculate Player Send Result

        private void CalculatePlayerSendResult(CalculateResultNotify data)
        {
            MobManager.CurrentsMob.ModifyQuestionNumber(data.Result, data.FirstOperator);
            DetectMobDefeated();
        }

        private void DetectMobDefeated()
        {
            if (MobManager.CurrentsMob.CurrentQuestionNumber == 0)
            {
                var calculateMobDefeated = new Calculate_MobDefeated(MobManager.CurrentsMob.CurrentQuestionNumber);
                GlobalMessagePipe.GetPublisher<Calculate_MobDefeated>().Publish(calculateMobDefeated);
            }
            else
            {
                var calculateUpdateMobQuestionNumber =
                    new Calculate_UpdateMobQuestionNumber(MobManager.CurrentsMob.CurrentQuestionNumber);
                GlobalMessagePipe.GetPublisher<Calculate_UpdateMobQuestionNumber>()
                    .Publish(calculateUpdateMobQuestionNumber);
            }
        }

        #endregion
    }
}