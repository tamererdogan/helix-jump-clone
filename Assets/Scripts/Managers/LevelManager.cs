using System.Collections.Generic;
using Components.UI;
using Controllers;
using Events;
using Structs;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    [RequireComponent(typeof(LevelController))]
    public class LevelManager : MonoBehaviour
    {
        public static UnityAction<int> OnLevelChanged = delegate { };
        public static UnityAction OnLevelReplayed = delegate { };

        private LevelController _levelController;
        private int _levelIndex;
        private List<Level> _levels;

        private void Awake()
        {
            _levelIndex = PlayerPrefs.GetInt("level", 0);
            _levelController = GetComponent<LevelController>();
        }

        private void Start()
        {
            _levels = _levelController.LoadLevelData();
            _levelController.CreateLevel(_levels[GetLoadLevelIndex()]);
        }

        private void OnEnable()
        {
            NextLevelButtonEvent.OnButtonClicked += NextLevel;
            ReplayButtonEvent.OnButtonClicked += ReplayLevel;
            NextLevelComponent.OnUpdateRequested += NotifyLevelUpdated;
            ProgressComponent.OnUpdateRequested += NotifyLevelUpdated;
        }

        private void NotifyLevelUpdated()
        {
            OnLevelChanged?.Invoke(_levelIndex);
        }

        private void ReplayLevel()
        {
            var loadLevelIndex = GetLoadLevelIndex();
            _levelController.CreateLevel(_levels[loadLevelIndex]);
            OnLevelReplayed.Invoke();
        }

        private void NextLevel()
        {
            _levelIndex++;
            PlayerPrefs.SetInt("level", _levelIndex);
            var loadLevelIndex = GetLoadLevelIndex();
            _levelController.CreateLevel(_levels[loadLevelIndex]);
            OnLevelChanged.Invoke(_levelIndex);
        }

        private void OnDisable()
        {
            NextLevelButtonEvent.OnButtonClicked -= NextLevel;
            ReplayButtonEvent.OnButtonClicked -= ReplayLevel;
            NextLevelComponent.OnUpdateRequested -= NotifyLevelUpdated;
            ProgressComponent.OnUpdateRequested -= NotifyLevelUpdated;
        }

        private int GetLoadLevelIndex()
        {
            return _levelIndex > _levels.Count - 1 ? _levelIndex % _levels.Count : _levelIndex;
        }
    }
}
