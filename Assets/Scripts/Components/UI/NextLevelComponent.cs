using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Components.UI
{
    public class NextLevelComponent : MonoBehaviour
    {
        public static UnityAction OnUpdateRequested = delegate { };

        [SerializeField]
        private TMP_Text levelText;

        private void OnEnable()
        {
            LevelManager.OnLevelChanged += UpdateLevel;
            OnUpdateRequested.Invoke();
        }

        private void UpdateLevel(int level)
        {
            levelText.text = "Level " + level + " Completed!";
        }

        private void OnDisable()
        {
            LevelManager.OnLevelChanged -= UpdateLevel;
        }
    }
}