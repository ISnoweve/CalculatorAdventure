using System;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Sys.Button;
using _Main.CalculatorSys.Sys.Button.Event;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.ChallengeSys.Data;
using _Main.ChallengeSys.Enum;
using _Main.ChallengeSys.Sys.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace _Main.ChallengeSys.Sys
{
    [Serializable]
    public class ChallengeSystem : Singleton<ChallengeSystem>
    {
        [SerializeField] private ChallengeDataSoList challengeAllData;
        [SerializeField] private ChallengeData currentChallengeData;
        [SerializeField] private int challengeToGoalCount;
        [SerializeField] private int currentChallengeIndex;

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
            GlobalMessagePipe.GetSubscriber<NotifySetMobBattle>().Subscribe(InitChallenge).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(ResetChallengeAfterBattle).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<ButtonClickSuccess>().Subscribe(CheckChallengePass).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<CalculateResultNotify>().Subscribe(CheckChallengePass).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #region Initial

        public void InitChallenge(NotifySetMobBattle data)
        {
            Instance.ResetChallenge();
            Instance.SetChallenge();
        }
        
        private void ResetChallenge()
        {
            Instance.currentChallengeData = null;
            Instance.challengeToGoalCount = 0;
            Instance.currentChallengeIndex = 0;
        }
        
        private void ResetChallengeAfterBattle(NotifyMobBattleNewState data)
        {
            if(data.NewState != MobBattleStateEnum.BattleResult)return;
            Instance.currentChallengeData = null;
            Instance.challengeToGoalCount = 0;
            Instance.currentChallengeIndex = 0;
        }

        #endregion

        #endregion

        #region Behaviour

        public void SetChallengeSoList(ChallengeDataSoList challengeDataSoList)
        {
            challengeAllData = challengeDataSoList;
        }

        

        [Button]
        public void SetChallenge()
        {
            NextChallenge();
            var challengeNew = new ChallengeNew(currentChallengeData);
            GlobalMessagePipe.GetPublisher<ChallengeNew>().Publish(challengeNew);
        }

        private void NextChallenge()
        {
            if (challengeAllData == null || challengeAllData.ChallengeDataList.Length <= 0) return;
            var random = new Random();
            var randomIndex = random.Next(0, challengeAllData.ChallengeDataList.Length);
            currentChallengeData = challengeAllData.ChallengeDataList[randomIndex];
            challengeToGoalCount = currentChallengeData.toGoalCount;
            currentChallengeIndex = 0;
        }

        private void CheckChallengePass(ButtonClickSuccess data)
        {
            // if (currentChallengeData == null) return;
            // var button = CalculatorButtonManager.GetButtonByIndex(data.ButtonIndex);
            // if(button.CalculatorButtonType != CalculatorButtonType.NumberActivate)return;
            // if (currentChallengeData.CheckIsChallengePass(button.CurrentValue))
            // {
            //     challengeToGoalCount++;
            //     if (challengeToGoalCount <= 0)
            //     {
            //         
            //     }
            // }
        }

        private void CheckChallengePass(CalculateResultNotify data)
        {
            Debug.Log("asd");
            
            if (currentChallengeData == null) return;

            if (!currentChallengeData.CheckIsChallengePass(data.Result)) return;

            currentChallengeIndex++;
            var challengeToGoalUpdate =
                new ChallengeToGoalUpdate(currentChallengeIndex, challengeToGoalCount);
            GlobalMessagePipe.GetPublisher<ChallengeToGoalUpdate>().Publish(challengeToGoalUpdate);

            if (currentChallengeIndex < challengeToGoalCount) return;

            var challengeSuccess = new ChallengeSuccess(currentChallengeData.challengeReward);
            GlobalMessagePipe.GetPublisher<ChallengeSuccess>().Publish(challengeSuccess);
            GiveChallengeReward();
            SetChallenge();
        }

        private void GiveChallengeReward()
        {
            if (currentChallengeData.challengeReward.HasFlag(ChallengeReward.Multiply))
                ButtonSystem.SetOperatorButtonClickAble(CalculatorOperator.Multiply);

            if (currentChallengeData.challengeReward.HasFlag(ChallengeReward.Divide))
                ButtonSystem.SetOperatorButtonClickAble(CalculatorOperator.Divide);
        }

        #endregion
    }
}