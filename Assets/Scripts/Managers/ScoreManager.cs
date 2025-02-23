using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static UnityAction<int> OnScoreUpdated = delegate { };

        private int _score;

        private void OnEnable()
        {
            PlayerHitEvent.OnBallTrigger += OnBallTrigger;
            PlayerManager.OnPlayerCollisionObstacle += OnPlayerCollisionObstacle;
        }

        private void OnPlayerCollisionObstacle(Collision other, bool isSafeMode)
        {
            if (isSafeMode) IncScore();
        }

        private void OnBallTrigger(Collider other)
        {
            IncScore();
        }

        private void OnDisable()
        {
            PlayerHitEvent.OnBallTrigger -= OnBallTrigger;
        }

        private void IncScore()
        {
            _score += 10;
            OnScoreUpdated.Invoke(_score);
        }
    }
}