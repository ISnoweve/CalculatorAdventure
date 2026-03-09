using _Main.StateSys.GameStateMachineSys.Enum;
using _Main.StateSys.GameStateMachineSys.View.Event;
using MessagePipe;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.StateSys.GameStateMachineSys.View.UI_SwitchGameState
{
    public class SwitchGameStateView : MonoBehaviour
    {
        [SerializeField] private GameState _gameStateToSwitch;
        [SerializeField] private Button _button;

        #region Life Cycle

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _button.onClick.AddListener(UpdateNewGameState);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(UpdateNewGameState);
        }

        #endregion
        

        private void UpdateNewGameState()
        {
            SetNewGameState setNewGameState = new SetNewGameState(_gameStateToSwitch);
            GlobalMessagePipe.GetPublisher<SetNewGameState>().Publish(setNewGameState);
        }
    }
}