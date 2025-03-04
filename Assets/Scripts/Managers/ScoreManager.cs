using Controllers;
using Controllers.UI;
using Structs;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static UnityAction<int> OnScoreUpdated = delegate { };

        private int _score;
        private int _previousScore;
        private int _levelMultiplier;
        private int _safeModeMultiplier;
        private bool _isSafeModeOpen;

        private void Awake()
        {
            _score = _previousScore = PlayerPrefs.GetInt("score", 0);
        }

        private void NotifyScoreUpdated()
        {
            OnScoreUpdated.Invoke(_score);
        }

        private void SaveMultipliers(Level currentLevel)
        {
            _levelMultiplier = currentLevel.levelMultiplier;
            _safeModeMultiplier = currentLevel.safeModeMultiplier;
        }

        private void IncreaseScore(Transform other)
        {
            _score += 1 * _levelMultiplier * (_isSafeModeOpen ? _safeModeMultiplier : 1);
            OnScoreUpdated.Invoke(_score);
        }

        private void RestorePrevScore()
        {
            _score = _previousScore;
        }

        private void SaveScore(int levelIndex)
        {
            _previousScore = _score;
            PlayerPrefs.SetInt("score", _score);
        }

        private void OpenSafeMode()
        {
            _isSafeModeOpen = true;
        }

        private void CloseSafeMode()
        {
            _isSafeModeOpen = false;
        }

        private void OnEnable()
        {
            LevelManager.OnLevelChanged += SaveScore;
            LevelManager.OnLevelReplayed += RestorePrevScore;
            LevelManager.OnLevelLoaded += SaveMultipliers;
            PlayerController.OnPlayerPassed += IncreaseScore;
            PlayerController.OnSafeModeEnter += OpenSafeMode;
            PlayerController.OnSafeModeExit += CloseSafeMode;
            ReplayPanelController.OnUpdateRequested += NotifyScoreUpdated;
            ProgressPanelController.OnUpdateRequested += NotifyScoreUpdated;
        }

       private void OnDisable()
        {
            LevelManager.OnLevelChanged -= SaveScore;
            LevelManager.OnLevelReplayed -= RestorePrevScore;
            LevelManager.OnLevelLoaded -= SaveMultipliers;
            PlayerController.OnPlayerPassed -= IncreaseScore;
            PlayerController.OnSafeModeEnter -= OpenSafeMode;
            PlayerController.OnSafeModeExit -= CloseSafeMode;
            ReplayPanelController.OnUpdateRequested -= NotifyScoreUpdated;
            ProgressPanelController.OnUpdateRequested -= NotifyScoreUpdated;
        }
    }
}