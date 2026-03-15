using System;
using _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView;
using _Main.GameSceneSys.Sys.Event;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using UnityEngine;

namespace _Main.GameSceneSys.LoadPanelSys.View.UI_Control
{
    public class GameSceneControl : SingletonMonoBehaviour<GameSceneControl>
    {
        [SerializeField] private ScenePanelView scenePanelView;
        protected override bool IsDontDestroyOnLoad => true;

        #region Life Cycle

        protected override void Awake()
        {
            base.Awake();

            if (Instance != this) return;

            SubscribeEvent();
        }

        private IDisposable _disposable;

        private void SubscribeEvent()
        {
            _disposable?.Dispose();
            var bag = DisposableBag.CreateBuilder();
            GlobalMessagePipe.GetSubscriber<BeforeSceneChange>().Subscribe(BeforeSwitchScene).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<AfterSceneChange>().Subscribe(AfterSwitchScene).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion

        #region Behaviour

        private void BeforeSwitchScene(BeforeSceneChange data)
        {
            scenePanelView.PanelFadeInAnimation();
        }

        private void AfterSwitchScene(AfterSceneChange data)
        {
            scenePanelView.PanelFadeOutAnimation();
        }

        #endregion
    }
}