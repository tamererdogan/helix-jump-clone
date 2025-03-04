using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.UI
{
    public class ReplayPanelController : MonoBehaviour
    {
        public static UnityAction OnUpdateRequested = delegate { };

        [SerializeField]
        private TMP_Text scoreText;

        private void UpdateScoreUI(int score)
        {
            scoreText.text = "Score: " + score;
        }

        private void OnEnable()
        {
            ScoreManager.OnScoreUpdated += UpdateScoreUI;

            OnUpdateRequested.Invoke();
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreUpdated -= UpdateScoreUI;
        }
    }
}