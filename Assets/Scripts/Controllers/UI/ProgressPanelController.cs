using Managers;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class ProgressPanelController : MonoBehaviour
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

        private void ResetUI()
        {
            OnUpdateRequested.Invoke();
        }

        private void UpdateScoreUI(int score)
        {
            scoreText.text = score.ToString();
        }

        private void UpdateProgressUI(int level)
        {
            slider.value = 0;
            leftSquareText.text = level.ToString();
            rightSquareText.text = (level + 1).ToString();
        }

        private void IncreaseProgressUI(Transform other)
        {
            slider.value++;
        }

        private void SetSliderMaxValue(Level currentLevel)
        {
            slider.maxValue = currentLevel.indices.Length;
        }

        private void OnEnable()
        {
            ScoreManager.OnScoreUpdated += UpdateScoreUI;
            LevelManager.OnLevelChanged += UpdateProgressUI;
            LevelManager.OnLevelReplayed += ResetUI;
            LevelManager.OnLevelLoaded += SetSliderMaxValue;
            PlayerController.OnPlayerPassed += IncreaseProgressUI;

            OnUpdateRequested.Invoke();
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreUpdated -= UpdateScoreUI;
            LevelManager.OnLevelChanged -= UpdateProgressUI;
            LevelManager.OnLevelReplayed -= ResetUI;
            LevelManager.OnLevelLoaded -= SetSliderMaxValue;
            PlayerController.OnPlayerPassed -= IncreaseProgressUI;
        }
    }
}