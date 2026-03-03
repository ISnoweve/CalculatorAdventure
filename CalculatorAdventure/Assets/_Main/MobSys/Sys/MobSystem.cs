using System;
using _Main.MobSys.Manager;
using _Main.MobSys.Sys.Event;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;

namespace _Main.MobSys.Sys
{
    public class MobSystem : Singleton<MobSystem>
    {
        #region life cycle

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        
        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            _disposable = bag.Build();
        }
        

        #endregion

        #region SubScribe Event

        private void ModifyMobQuestionNumber()
        {
            //MobManager.CurrentsMob.ModifyQuestionNumber();
        }

        #endregion

        #region Mob Round Behaviour

        private void MobBehaviorTurn()
        {
            DecreaseMobBehaviorCountDown();
            if (DetectMobBehaviorCountDown()==false)
            {
                MobTurn_DereaseNumber mobTurnDecreaseNumber = 
                    new MobTurn_DereaseNumber(MobManager.CurrentsMob.AttackSkillCountDown);
                GlobalMessagePipe.GetPublisher<MobTurn_DereaseNumber>().Publish(mobTurnDecreaseNumber);
            }
            MobExecuteBehaviour();
            RandomNewMobBehavior();
        }

        private void DecreaseMobBehaviorCountDown()
        {
            MobManager.CurrentsMob.DecreaseAttackSkillCountDown();
        }
        
        private bool DetectMobBehaviorCountDown()
        {
            return MobManager.CurrentsMob.AttackSkillCountDown <= 0;
        }
        
        private void MobExecuteBehaviour()
        {
            MobManager.CurrentsMob.ExecuteNextAttackSkill();
        }
        
        private void RandomNewMobBehavior()
        {
            MobManager.CurrentsMob.RandomNextAttackSkill();
        }

        #endregion
    }
}