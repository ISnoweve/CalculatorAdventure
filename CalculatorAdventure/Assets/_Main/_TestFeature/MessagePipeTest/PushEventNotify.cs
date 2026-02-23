using _Main.MessagePipeTest.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.MessagePipeTest
{
    public class PushEventNotify : MonoBehaviour
    {
        public void PushEvent()
        {
            var pushTest = new PushEvent();
            GlobalMessagePipe.GetPublisher<PushEvent>().Publish(pushTest);
        }
    }
}