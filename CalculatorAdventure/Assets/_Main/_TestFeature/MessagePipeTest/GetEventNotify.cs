using System;
using _Main.MessagePipeTest.Event;
using MessagePipe;
using UnityEngine;

namespace _Main.MessagePipeTest
{
    public class GetEventNotify : MonoBehaviour
    {
        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _disposable?.Dispose();
            DisposableBagBuilder bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<PushEvent>().Subscribe(GetNotify).AddTo(bag);
            _disposable = bag.Build();
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void GetNotify(PushEvent data)
        {
            Debug.Log("Get Notify");
        }
    }
}