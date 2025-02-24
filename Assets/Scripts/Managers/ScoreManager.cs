using Components.UI;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static UnityAction<int> OnScoreUpdated = delegate { };

        private int _score;
        private int _prevScore;

        private void OnEnable()
        {
            PlayerHitEvent.OnPlayerTrigger += OnPlayerTrigger;
            PlayerManager.OnPlayerCollisionObstacle += OnPlayerCollisionObstacle;
            ReplayComponent.OnUpdateRequested += NotifyScoreUpdated;
            ProgressComponent.OnUpdateRequested += NotifyScoreUpdated;
            LevelManager.OnLevelChanged += OnLevelChanged;
            LevelManager.OnLevelReplayed += OnLevelReplayed;
        }

        private void OnLevelReplayed()
        {
            _score = _prevScore;
        }

        private void OnLevelChanged(int levelIndex)
        {
            _prevScore = _score;
        }

        private void NotifyScoreUpdated()
        {
            OnScoreUpdated.Invoke(_score);
        }

        private void OnPlayerCollisionObstacle(Collision other, bool isSafeMode)
        {
            if (isSafeMode) IncScore();
        }

        private void OnPlayerTrigger(Collider other)
        {
            IncScore();
        }

        private void OnDisable()
        {
            PlayerHitEvent.OnPlayerTrigger -= OnPlayerTrigger;
            PlayerManager.OnPlayerCollisionObstacle -= OnPlayerCollisionObstacle;
            ReplayComponent.OnUpdateRequested -= NotifyScoreUpdated;
            ProgressComponent.OnUpdateRequested -= NotifyScoreUpdated;
            LevelManager.OnLevelChanged -= OnLevelChanged;
            LevelManager.OnLevelReplayed -= OnLevelReplayed;
        }

        private void IncScore()
        {
            _score += 10;
            OnScoreUpdated.Invoke(_score);
        }
    }
}