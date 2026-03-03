using System;
using _Main.SnoweveToolKit.SceneSys.Sys;
using _Main.SnoweveToolKit.ToolKit;
using _Main.StateSys.GameStateMachine.Enum;
using _Main.StateSys.GameStateMachine.Sys.Event;
using MessagePipe;

namespace _Main.GameSceneSys.Sys
{
    public sealed class GameSceneSystem : Singleton<GameSceneSystem>
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
            //GlobalMessagePipe.GetSubscriber<GameStateMachineChangeState>().Subscribe(SwitchSceneWithGameState).AddTo(bag);
            _disposable = bag.Build();
        }
        
        protected override void Release()
        {
            _disposable?.Dispose();
            base.Release();
        }


        #endregion
        
        private async void SwitchSceneWithGameState(GameStateMachineChangeState data)
        {
            try
            {
                switch (data.NewGameState)
                {
                    case GameState.Menu:
                        await SceneSystem.LoadScene("");
                        break;
                    case GameState.Option:
                        break;
                    case GameState.InMap:
                        break;
                    case GameState.None:
                        break;
                    case GameState.InStoreSpot:
                        break;
                    case GameState.InQuestionSpot:
                        break;
                    case GameState.InMobBattle:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                throw; // TODO handle exception
            }
        }
    }
}