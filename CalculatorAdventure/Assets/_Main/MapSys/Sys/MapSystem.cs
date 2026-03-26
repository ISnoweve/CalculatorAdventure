using System;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine;

namespace _Main.MapSys.Sys
{
    public class MapSystem : Singleton<MapSystem>
    {
        #region Life Cycle

        
        protected override void Initialize()
        {
            base.Initialize();
            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            //GlobalMessagePipe.GetSubscriber<>().Subscribe().AddTo(bag);
            //GlobalMessagePipe.GetSubscriber<>().Subscribe().AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion

        public bool TryGetMapNote()
        {
            return false;
        }
    }
} 