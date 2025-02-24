using Enums;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager: MonoBehaviour
    {
        public static UnityAction<GameStates, GameStates> OnGameStateChanged = delegate { };

        private GameStates _gameState = GameStates.PreGame;

        void Start()
        {
            Time.timeScale = 0f;
        }
 
        private void OnEnable()
        {
            TapToStartButtonEvent.OnButtonClicked += StartGame;
            ReplayButtonEvent.OnButtonClicked += StartGame;
            NextLevelButtonEvent.OnButtonClicked += StartGame;
            PlayerManager.OnPlayerCollisionObstacle += OnPlayerCollisionObstacle;
            PlayerManager.OnPlayerCollisionFinish += OnPlayerCollisionFinish;
        }

        private void StartGame()
        {
            ChangeState(GameStates.Running);
            Time.timeScale = 1f;
        }

        private void OnPlayerCollisionObstacle(Collision other, bool isSafeMode)
        {
            if (isSafeMode) return;
            ChangeState(GameStates.Failed);
            Time.timeScale = 0f;
        }

        private void OnPlayerCollisionFinish()
        {
            ChangeState(GameStates.Finished);
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            TapToStartButtonEvent.OnButtonClicked -= StartGame;
            ReplayButtonEvent.OnButtonClicked -= StartGame;
            NextLevelButtonEvent.OnButtonClicked -= StartGame;
            PlayerManager.OnPlayerCollisionObstacle -= OnPlayerCollisionObstacle;
            PlayerManager.OnPlayerCollisionFinish -= OnPlayerCollisionFinish;
        }

        private void ChangeState(GameStates currentState)
        {
            var beforeState = _gameState;
            _gameState = currentState;
            OnGameStateChanged(beforeState, currentState);
        }
    }
}
