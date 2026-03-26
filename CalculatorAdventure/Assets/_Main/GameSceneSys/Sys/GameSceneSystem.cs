using System;
using _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView.Event;
using _Main.GameSceneSys.Sys.Event;
using _Main.SceneSys.Sys;
using _Main.StateSys.GameStateMachineSys.Enum;
using _Main.StateSys.GameStateMachineSys.Sys.Event;
using _Main.ToolKit.SingletonFeature;
using MessagePipe;
using UnityEngine;

namespace _Main.GameSceneSys.Sys
{
    [Serializable]
    public sealed class GameSceneSystem : Singleton<GameSceneSystem>
    {
        [SerializeField] private GameState recordChangeSceneState = GameState.None;

        private void OnGameStateChange(GameStateMachineChangeState data)
        {
            recordChangeSceneState = data.NewGameState;
            var beforeSceneChange = new BeforeSceneChange();
            GlobalMessagePipe.GetPublisher<BeforeSceneChange>().Publish(beforeSceneChange);
        }

        private async void SwitchSceneWithGameState(Event_FadeInAnimationEnd data)
        {
            switch (recordChangeSceneState)
            {
                case GameState.Menu:
                    await SceneSystem.LoadScene(GameState.Menu.ToString());
                    break;
                case GameState.Option:
                    await SceneSystem.LoadScene(GameState.Option.ToString());
                    break;
                case GameState.InMap:
                    await SceneSystem.LoadScene(GameState.InMap.ToString());
                    break;
                case GameState.InStoreSpot:
                    await SceneSystem.LoadScene(GameState.InStoreSpot.ToString());
                    break;
                case GameState.InQuestionSpot:
                    await SceneSystem.LoadScene(GameState.InQuestionSpot.ToString());
                    break;
                case GameState.InMobBattle:
                    await SceneSystem.LoadScene(GameState.InMobBattle.ToString());
                    break;
                case GameState.None:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CallAfterChanceScene();
        }

        private void CallAfterChanceScene()
        {
            var changeSceneSuccess = new AfterSceneChange(recordChangeSceneState);
            GlobalMessagePipe.GetPublisher<AfterSceneChange>().Publish(changeSceneSuccess);
        }

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
            GlobalMessagePipe.GetSubscriber<GameStateMachineChangeState>().Subscribe(OnGameStateChange).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_FadeInAnimationEnd>().Subscribe(SwitchSceneWithGameState).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }

        #endregion
    }
}