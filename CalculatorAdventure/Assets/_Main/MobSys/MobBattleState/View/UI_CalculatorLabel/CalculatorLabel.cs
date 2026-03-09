using System;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.CalculatorSys.Sys.Calculator.Enum;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.MobSys.MobBattleState.Enum;
using _Main.MobSys.MobBattleState.Event;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using TMPro;
using UnityEngine;

namespace _Main.CalculatorSys.View.UI_CalculatorLabel
{
    public class CalculatorLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private TMP_Text warningText;
        [SerializeField] private int currentCalculatorOperationAndValueCount;
        
        [SerializeField] private string warningTextForEmptyNumber = "No number in box for delete.";
        [SerializeField] private string warningTextForEmptyOperator = "No operator in box for delete.";
        [SerializeField] private string warningNotBoxNotFill = "Box is not filled.";


        #region Life cycle

        private void Awake()
        {
            ClearDisplay();
            ClearWarning();
            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<CalculatorNotify>().Subscribe(Notify).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<CalculateResultNotify>().Subscribe(Result).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<CalculatorWarning>().Subscribe(ShowWarningText).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<NotifyMobBattleNewState>().Subscribe(ResetLabel).AddTo(bag);
            _disposable = bag.Build();
        }
        
        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        #endregion

        #region Behaviour
        
        private void ShowWarningText(CalculatorWarning data)
        {
            switch (data.Warning)
            {
                case CalculatorWarningEnum.OperatorIsEmpty:
                    warningText.text = warningTextForEmptyOperator;
                    break;
                case CalculatorWarningEnum.NumberIsEmpty:
                    warningText.text = warningTextForEmptyNumber;
                    break;
                case CalculatorWarningEnum.CantGiveResult:
                    warningText.text = warningNotBoxNotFill;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Notify(CalculatorNotify data)
        {
            currentCalculatorOperationAndValueCount = data.IndexCount;
            ClearWarning();
            ClearDisplay();

            for (var i = 0; i < data.IndexCount; i++)
            {
                switch (data.CurrentOperators[i])
                {
                    case CalculatorOperator.Add:
                        labelText.text += "+";
                        break;
                    case CalculatorOperator.Subtract:
                        labelText.text += "-";
                        break;
                    case CalculatorOperator.Multiply:
                        labelText.text += "x";
                        break;
                    case CalculatorOperator.Divide:
                        labelText.text += "/";
                        break;
                    case CalculatorOperator.None:
                        labelText.text += "\u25a1";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                labelText.text += data.NumbersInBox[i];
            }
        }

        private void Result(CalculateResultNotify data)
        {
            ClearWarning();
            ClearDisplay();
            
            switch (data.FirstOperator)
            {
                case CalculatorOperator.Add:
                    labelText.text = "+";
                    break;
                case CalculatorOperator.Subtract:
                    labelText.text = "-";
                    break;
                case CalculatorOperator.Multiply:
                    labelText.text = "x";
                    break;
                case CalculatorOperator.Divide:
                    labelText.text = "/";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            labelText.text += data.Result;
        }
        
        private void ClearDisplay()
        {
            labelText.text = "";
        }
        
        private void ClearWarning()
        {
            warningText.text = "";
        }

        #endregion

        #region API Feature

        private void ResetLabel(NotifyMobBattleNewState data)
        {
            if(data.NewState != MobBattleStateEnum.PlayerTurn)return;

            ClearDisplay();
            ClearWarning();
                
            for (var i = 0; i < currentCalculatorOperationAndValueCount; i++)
            {
                labelText.text += "\u25a1";
                labelText.text += 0;
            }
        }

        #endregion
    }
}