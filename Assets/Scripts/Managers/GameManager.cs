using Controllers;
using Enums;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static UnityAction<GameState> OnGameStateChanged = delegate { };

        private GameState _gameState = GameState.PreGame;

        private void StartGame()
        {
            ChangeState(GameState.Running);
            Time.timeScale = 1f;
        }

        private void FailGame()
        {
            ChangeState(GameState.Failed);
            Time.timeScale = 0f;
        }

        private void FinishGame()
        {
            ChangeState(GameState.Finished);
            Time.timeScale = 0f;
        }

        private void ChangeState(GameState state)
        {
            _gameState = state;
            OnGameStateChanged.Invoke(_gameState);
        }

        private void OnEnable()
        {
            TapToStartButtonEvent.OnButtonClicked += StartGame;
            ReplayButtonEvent.OnButtonClicked += StartGame;
            NextLevelButtonEvent.OnButtonClicked += StartGame;
            PlayerController.OnPlayerFailed += FailGame;
            PlayerController.OnPlayerFinished += FinishGame;
        }

        private void OnDisable()
        {
            TapToStartButtonEvent.OnButtonClicked -= StartGame;
            ReplayButtonEvent.OnButtonClicked -= StartGame;
            NextLevelButtonEvent.OnButtonClicked -= StartGame;
            PlayerController.OnPlayerFailed -= FailGame;
            PlayerController.OnPlayerFinished -= FinishGame;
        }
    }
}