using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.UI
{
    public class NextLevelPanelController : MonoBehaviour
    {
        public static UnityAction OnUpdateRequested = delegate { };

        [SerializeField]
        private TMP_Text levelText;

        private void UpdateLevelUI(int level)
        {
            levelText.text = "Level " + level + " Completed!";
        }

        private void OnEnable()
        {
            LevelManager.OnLevelChanged += UpdateLevelUI;

            OnUpdateRequested.Invoke();
        }

        private void OnDisable()
        {
            LevelManager.OnLevelChanged -= UpdateLevelUI;
        }
    }
}