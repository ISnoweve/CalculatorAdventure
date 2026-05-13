using System;
using _Main.MobBattleSys.MobReward.View.UI_MobRewardView.Event;
using _Main.MoneySys.Sys;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Sys;
using MessagePipe;
using UnityEngine;
using UnityEngine.Events;

namespace EasyUI.PickerWheelUI.GameScripts
{
    public class SpinResultService : MonoBehaviour
    {
        [SerializeField] private PickerWheel _pickerWheel;

        private void Awake()
        {
            _pickerWheel.OnSpinEnd(SetResult);
        }

        private void SetResult(WheelPiece wheelPiece)
        {
            if (wheelPiece.index == 1)
            {
                MoneySystem.Instance.GiveMoney(6);
            }

            if (wheelPiece.index == 2)
            {
                var item = UniqueItemSystem.Instance.GetOneNewItem();
                
                var data = new ChooseUniqueReward(item.Id);
                GlobalMessagePipe.GetPublisher<ChooseUniqueReward>().Publish(data);
            }

            if (wheelPiece.index == 4)
            {
                MoneySystem.Instance.GiveMoney(3);
            }

            if (wheelPiece.index == 6)
            {
                MoneySystem.Instance.GiveMoney(9);
            }

            if (wheelPiece.index == 7)
            {
                MoneySystem.Instance.GiveMoney(50);
            }
        }
    }
}