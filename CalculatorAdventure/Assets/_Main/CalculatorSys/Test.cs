using System;
using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Sys.Calculator.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys
{
    public class Test : MonoBehaviour
    {
        private void Notify(CalculatorNotify data)
        {
            var result = "";

            for (var i = 0; i < data.IndexCount; i++)
            {
                switch (data.CurrentOperators[i])
                {
                    case CalculatorOperator.Add:
                        result += "+";
                        break;
                    case CalculatorOperator.Subtract:
                        result += "-";
                        break;
                    case CalculatorOperator.Multiply:
                        result += "x";
                        break;
                    case CalculatorOperator.Divide:
                        result += "/";
                        break;
                    case CalculatorOperator.None:
                        result += "\u25a1";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                result += data.NumbersInBox[i];
            }


            Debug.Log(result);
        }

        private void Result(CalculatorResultNotify data)
        {
            var result = "";

            switch (data.FirstOperator)
            {
                case CalculatorOperator.Add:
                    result = "+";
                    break;
                case CalculatorOperator.Subtract:
                    result = "-";
                    break;
                case CalculatorOperator.Multiply:
                    result = "x";
                    break;
                case CalculatorOperator.Divide:
                    result = "/";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log(result + data.Result);
        }

        #region Life cycle

        private void Start()
        {
            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<CalculatorNotify>().Subscribe(Notify).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<CalculatorResultNotify>().Subscribe(Result).AddTo(bag);
            _disposable = bag.Build();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        #endregion


        // making Excel 
        // 做excel 
    }
}