using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Components.UI
{
    public class ReplayComponent : MonoBehaviour
    {
        public static UnityAction OnUpdateRequested = delegate { };

        [SerializeField]
        private TMP_Text scoreText;

        private void OnEnable()
        {
            ScoreManager.OnScoreUpdated += UpdateScore;
            OnUpdateRequested.Invoke();
        }

        private void UpdateScore(int score)
        {
            scoreText.text = "Score: " + score;
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreUpdated -= UpdateScore;
        }
    }
}