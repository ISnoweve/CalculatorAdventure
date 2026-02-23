using System;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Calculator.Event
{
    [Serializable]
    public readonly struct CalculatorNotifyIsLastNumberAfterRecover : IEventData
    {
    }
}