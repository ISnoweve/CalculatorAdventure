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
            var bag = DisposableBag.CreateBuilder();
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
        
        // protected override void Initialize()
        // {
        //     SubscribeEvent();
        //     base.Initialize();
        // }
        //
        // private IDisposable _disposable;
        //
        // private void SubscribeEvent()
        // {
        //     _disposable?.Dispose();
        //     var bag = DisposableBag.CreateBuilder();
        //     _disposable = bag.Build();
        // }
        //
        // protected override void Release()
        // {
        //     _disposable?.Dispose();
        //     base.Release();
        // }
    }
}