using System;
using EventSys.Interface;

namespace _Main.MessagePipeTest.Event
{
    [Serializable]
    public readonly struct PushEvent : IEventData
    {
    }
}