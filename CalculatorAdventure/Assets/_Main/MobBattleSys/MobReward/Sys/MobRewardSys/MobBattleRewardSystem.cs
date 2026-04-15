using System;
using System.Collections.Generic;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using _Main.MobBattleSys.MobReward.Data.MobReward;
using _Main.MobBattleSys.MobReward.Data.MobReward.Enum;
using _Main.MobBattleSys.MobReward.Sys.MobRewardSys.Event;
using _Main.MobSys.Enum;
using _Main.MobSys.Manager.Event;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data;
using _Main.UniqueItemSys.Manager.Runtime;
using _Main.UniqueItemSys.Sys;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.MobBattleSys.MobReward.Sys.MobRewardSys
{
    [Serializable]
    public class MobBattleRewardSystem : Singleton<MobBattleRewardSystem>
    {
        [SerializeField] private MobRewardDataSoList mobRewardDataSoList;
        
        [Title("Current Mob Info")]
        [SerializeField] private MobType mobType;
        [SerializeField] private int mobRewardBlank;
        [SerializeField] private float mobRewardMoneyValueLimit;
        [SerializeField] private float mobRewardModifyValueAfterBlank;
        
        [Title("Out Put Value")]
        [SerializeField] private float originalMobRewardMoneyValue;
        [SerializeField] private float originalMobRewardUniqueItemValue;
        [SerializeField] private float currentMobRewardMoneyValue;
        [SerializeField] private float currentMobRewardUniqueItemValue;
        [SerializeField] private int currentRound=1;
        [SerializeField] private int moneyRewardValue;
        
        #region Life Cycle

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
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(UpdateCurrentRound).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(OutputRewardValue).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<SpawnMobEvent>().Subscribe(SetMobRewardByType).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion

        #region Set RewardSoList Data
        
        public void SetMobRewardDataSoList(MobRewardDataSoList mobRewardDataSoList)
        {
            Instance.mobRewardDataSoList = mobRewardDataSoList;
        }

        #endregion

        #region SetReward

        private void SetMobRewardByType(SpawnMobEvent data)
        {
            MobRewardData mobRewardData = GetMobRewardDataByType(data.Mob.MobType);
            
            switch (data.Mob.MobType)
            {
                case MobType.Normal:
                    SetMobSetting(mobRewardData);
                    break;
                case MobType.Elite:
                    SetMobSetting(mobRewardData);
                    break;
                case MobType.Boss:
                    SetMobSetting(mobRewardData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private MobRewardData GetMobRewardDataByType(MobType mobType)
        {
            return mobRewardDataSoList.MobRewardDataList.Find(x => x.MobType == mobType);
        }

        private void SetMobSetting(MobRewardData mobRewardData)
        {
            mobType = mobRewardData.MobType;
            mobRewardBlank = mobRewardData.MobRewardBlank;
            currentRound = mobRewardBlank;
            mobRewardMoneyValueLimit = mobRewardData.MobRewardMoneyValueLimit;
            mobRewardModifyValueAfterBlank = mobRewardData.MobRewardModifyValueAfterBlank;
            moneyRewardValue = mobRewardData.MoneyRewardValue;
            originalMobRewardMoneyValue = mobRewardData.MobRewardMoneyValue;
            originalMobRewardUniqueItemValue = mobRewardData.MobRewardUniqueItemValue;
            currentMobRewardMoneyValue = originalMobRewardMoneyValue;
            currentMobRewardUniqueItemValue = originalMobRewardUniqueItemValue;

            
            if (DetectMobType(mobType))
            {
                UpdateRewardRoundAndValue updateRewardRoundAndValue = new UpdateRewardRoundAndValue
                    (currentRound,currentMobRewardMoneyValue,currentMobRewardUniqueItemValue,true);
                GlobalMessagePipe.GetPublisher<UpdateRewardRoundAndValue>().Publish(updateRewardRoundAndValue);
            }
            else
            {
                UpdateRewardRoundAndValue updateRewardRoundAndValue = new UpdateRewardRoundAndValue
                    (currentRound,currentMobRewardMoneyValue,currentMobRewardUniqueItemValue,false);
                GlobalMessagePipe.GetPublisher<UpdateRewardRoundAndValue>().Publish(updateRewardRoundAndValue);
            }
        }

        private bool DetectMobType(MobType type)
        {
            return type==MobType.Boss;
        }
        
        #endregion

        #region Update Reward Value
        
        private void UpdateCurrentRound(NotifyMobBattleNewState data)
        {
            if (data.NewState != MobBattleStateEnum.BeforePlayerTurn)return;
            UpdateByOutBlankLimit();
        }

        private void UpdateByOutBlankLimit()
        {
            if(DetectMobType(mobType))return;
            
            currentRound--;
            
            if (currentRound <= 0)
            {
                currentRound = mobRewardBlank;
                ModifyRewardValueAfterBlank();
                UpdateRewardRoundAndValue updateRewardRoundAndValue = new UpdateRewardRoundAndValue
                    (currentRound,currentMobRewardMoneyValue,currentMobRewardUniqueItemValue,false);
                GlobalMessagePipe.GetPublisher<UpdateRewardRoundAndValue>().Publish(updateRewardRoundAndValue);
            }
            else
            {
                UpdateRewardRound updateRewardRound = new UpdateRewardRound(currentRound);
                GlobalMessagePipe.GetPublisher<UpdateRewardRound>().Publish(updateRewardRound);
            }
        }

        private void ModifyRewardValueAfterBlank()
        {
            if(currentMobRewardMoneyValue >= mobRewardMoneyValueLimit)return;
            currentMobRewardMoneyValue += mobRewardModifyValueAfterBlank;
            currentMobRewardUniqueItemValue -= mobRewardModifyValueAfterBlank;
        }

        #endregion

        #region Output Reward Value When Battle Win

        private void OutputRewardValue(NotifyMobBattleNewState data)
        {
            if (data.NewState != MobBattleStateEnum.BattleResult) return;
            OutputRewardValueByMobType();
        }
        
        private void OutputRewardValueByMobType()
        {
            float randomRewardValue = Random.Range(0f,1f);;
            if(randomRewardValue > currentMobRewardMoneyValue)
            {
                List<UniqueItem> uniqueItems = UniqueItemSystem.Instance.TryGetNewUniqueItemIdForBattleReward();
                OutPutUniqueItemReward outPutUniqueItemReward = new OutPutUniqueItemReward(uniqueItems);
                GlobalMessagePipe.GetPublisher<OutPutUniqueItemReward>().Publish(outPutUniqueItemReward);
            }
            else
            {
                OutPutMoneyReward outPutMoneyReward = new OutPutMoneyReward(moneyRewardValue);
                GlobalMessagePipe.GetPublisher<OutPutMoneyReward>().Publish(outPutMoneyReward);
            }
        }
        #endregion
    }
}