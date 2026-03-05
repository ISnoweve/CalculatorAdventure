using _Main.StateSys.GameStateMachine.Enum;
using _Main.StateSys.GameStateMachine.View.Event;
using MessagePipe;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.StateSys.GameStateMachine.View.UI_SwitchGameState
{
    public class SwitchGameStateView : MonoBehaviour
    {
        [SerializeField] private GameState _gameStateToSwitch;
        [SerializeField] private Button _button;

        #region Life Cycle

        private void OnEnable()
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