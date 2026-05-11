using System;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine;

namespace _Main.UniqueItemSys.View.UI_UniqueEffectCoverPanel
{
    public class UI_UniqueEffectCoverPanel : SingletonMonoBehaviour<UI_UniqueEffectCoverPanel>
    {
        [SerializeField] private GameObject coverPanel;

        #region Life Cycle

        protected override void Awake()
        {
            SubscribeEvent();
            base.Awake();
        }
        
        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion

        #region Behaviour

        private void ShowCoverPanel()
        {
            coverPanel.SetActive(true);
        }
        
        private void HideCoverPanel()
        {
            coverPanel.SetActive(false);
        }

        #endregion
    }
}