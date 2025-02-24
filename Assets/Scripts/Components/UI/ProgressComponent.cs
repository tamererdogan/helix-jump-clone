using Events;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Components.UI
{
    public class ProgressComponent : MonoBehaviour
    {
        public static UnityAction OnUpdateRequested = delegate { };

        [SerializeField]
        private TMP_Text scoreText;
        [SerializeField]
        private TMP_Text leftSquareText;
        [SerializeField]
        private TMP_Text rightSquareText;
        [SerializeField]
        private Slider slider;

        private void OnEnable()
        {
            ScoreManager.OnScoreUpdated += UpdateScore;
            LevelManager.OnLevelChanged += UpdateLevel;
            LevelManager.OnLevelReplayed += ReplayLevel;
            PlayerHitEvent.OnPlayerTrigger += OnPlayerTrigger;
            OnUpdateRequested.Invoke();
        }

        private void ReplayLevel()
        {
            slider.value = 0;
            OnUpdateRequested.Invoke();
        }

        private void OnPlayerTrigger(Collider other)
        {
            if (other.gameObject.CompareTag("Hidden"))
                slider.value++;
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

        private void UpdateLevel(int level)
        {
            slider.value = 0;
            leftSquareText.text = level.ToString();
            rightSquareText.text = (level + 1).ToString();
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreUpdated -= UpdateScore;
            LevelManager.OnLevelChanged -= UpdateLevel;
            LevelManager.OnLevelReplayed -= ReplayLevel;
            PlayerHitEvent.OnPlayerTrigger -= OnPlayerTrigger;
        }
    }
}