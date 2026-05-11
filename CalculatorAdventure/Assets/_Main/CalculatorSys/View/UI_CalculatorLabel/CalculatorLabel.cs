using System;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.CalculatorSys.Sys.Calculator.Enum;
using _Main.CalculatorSys.Sys.Calculator.Event;
using _Main.MobBattleSys.MobBattleState.Enum;
using _Main.MobBattleSys.MobBattleState.Event;
using MessagePipe;
using TMPro;
using UnityEngine;

namespace _Main.CalculatorSys.View.UI_CalculatorLabel
{
    public class CalculatorLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private TMP_Text warningText;
        [SerializeField] private TMP_Text previewResultText;
        [SerializeField] private int currentCalculatorOperationAndValueCount;

        [SerializeField] private string warningTextForEmptyNumber = "No number in box for delete.";
        [SerializeField] private string warningTextForEmptyOperator = "No operator in box for delete.";
        [SerializeField] private string warningNotBoxNotFill = "Box is not filled.";

        #region API Feature

        private void ResetLabel(NotifyMobBattleNewState data)
        {
            if (data.NewState != MobBattleStateEnum.PlayerTurn) return;

            ClearDisplay();
            ClearWarning();
            previewResultText.text = "0";

            for (var i = 0; i < currentCalculatorOperationAndValueCount; i++)
            {
                labelText.text += "\u25a1";
                if (i <= 0) labelText.text += "(";
                labelText.text += 0;
                if(i >= currentCalculatorOperationAndValueCount-1) labelText.text += ")";
            }
        }

        #endregion

        #region Life cycle

        private void Awake()
        {
            InitLabelText();
            SubscribeEvent();
        }

        private void InitLabelText()
        {
            ClearDisplay();
            ClearWarning();
            previewResultText.text = "0";

            currentCalculatorOperationAndValueCount = CalculatorSystem.Instance.CurrentCalculatorOperationAndValueCount;

            for (var i = 0; i < currentCalculatorOperationAndValueCount; i++)
            {
                labelText.text += "\u25a1";
                if (i <= 0) labelText.text += "(";
                labelText.text += 0;
                if(i >= currentCalculatorOperationAndValueCount-1) labelText.text += ")";
            }
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
                labelText.text += GetOperatorString(data.CurrentOperators[i]);

                if (i <= 0) labelText.text += "(";
                labelText.text += data.NumbersInBox[i];
                if(i >= data.IndexCount-1) labelText.text += ")";
            }

            if (CalculatorSystem.Instance.GetEqual() != 0)
            {
                previewResultText.text = GetOperatorString(data.CurrentOperators[0])+CalculatorSystem.Instance.GetEqual();
            }
            else
            {
                previewResultText.text = "0";
            }
        }

        private string GetOperatorString(CalculatorOperator data)
        {
            switch (data)
            {
                case CalculatorOperator.Add:
                    return "+";
                case CalculatorOperator.Subtract:
                    return "-";
                case CalculatorOperator.Multiply:
                    return "x";
                case CalculatorOperator.Divide:
                    return "/";
                case CalculatorOperator.None:
                    return "\u25a1";
                default:
                    throw new ArgumentOutOfRangeException();
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
    }
}