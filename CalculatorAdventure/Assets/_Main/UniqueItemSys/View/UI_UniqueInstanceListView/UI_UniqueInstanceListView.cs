using System;
using _Main.GameSceneSys.Sys.Event;
using _Main.StateSys.GameStateMachineSys.Enum;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Manager;
using _Main.UniqueItemSys.Sys.Event;
using _Main.UniqueItemSys.View.UI_UniqueInstanceView;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.UniqueItemSys.View.UI_UniqueInstanceListView
{
    public class UI_UniqueInstanceListView : SingletonMonoBehaviour<UI_UniqueInstanceListView>
    {
        [SerializeField] private GameObject scrollView,content;
        [SerializeField] private Vector2 menuPosition,battlePosition;
        [SerializeField] private GameObject instanceViewPrefab;
        
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
            GlobalMessagePipe.GetSubscriber<AfterSceneChange>().Subscribe(UpdateAllPlayerUniqueItem).AddTo(bag);
            GlobalMessagePipe.GetSubscriber<Event_NewUniqueItemToPlayer>().Subscribe(SetNewUniqueInstanceView).AddTo(bag);
            _disposable = bag.Build();
        }

        protected override void OnDestroy()
        {
            _disposable?.Dispose();
            base.OnDestroy();
        }

        #endregion

        #region Set Position By SceneLoad

        [Button]
        private void GetMenuPosition()
        {
            menuPosition = scrollView.transform.localPosition;
        }

        [Button]
        private void GetBattlePosition()
        {
            battlePosition = scrollView.transform.localPosition;
        }
        
        private void SetMenuPosition()
        {
            scrollView.transform.localPosition = menuPosition;
        }
        
        [Button]
        private void SetBattlePosition()
        {
            scrollView.transform.localPosition = battlePosition;
        }
        
        private void SetPositionBySceneLoad(AfterSceneChange sceneState)
        {
            switch (sceneState.CurrentGameState)
            {
                case GameState.InMobBattle:
                    SetBattlePosition();
                    break;
            }
        }

        #endregion

        #region Set New Unique Instance View

        private void UpdateAllPlayerUniqueItem(AfterSceneChange sceneState)
        {
            foreach (var uniqueItem in UniqueItemManager.GetAllUniqueItemsInPlayerInventory())
            {
                GameObject instanceView = Instantiate(instanceViewPrefab, content.transform);
                UI_UniqueIstanceView view = instanceView.GetComponentInChildren<UI_UniqueIstanceView>();
                view.SetView(uniqueItem.Id);
            }
        }

        private void SetNewUniqueInstanceView(Event_NewUniqueItemToPlayer data)
        {
            GameObject instanceView = Instantiate(instanceViewPrefab, content.transform);
            UI_UniqueIstanceView view = instanceView.GetComponentInChildren<UI_UniqueIstanceView>();
            view.SetView(data.Item.Id);
        }

        #endregion
    }
}