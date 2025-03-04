using System.Collections.Generic;
using Controllers.UI;
using Events;
using Structs;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static UnityAction<int> OnLevelChanged = delegate { };
        public static UnityAction OnLevelReplayed = delegate { };
        public static UnityAction<Level> OnLevelLoaded = delegate { };

        private int _levelIndex;
        private List<Level> _levels;

        private void Awake()
        {
            _levelIndex = PlayerPrefs.GetInt("level", 0);
        }

        private void Start()
        {
            _levels = LoadLevelData();
            NotifyLevelLoaded();
        }

        private void NotifyLevelChanged()
        {
            OnLevelChanged?.Invoke(_levelIndex);
        }

        private void NotifyLevelLoaded()
        {
            OnLevelLoaded.Invoke(_levels[GetLevelIndex()]);
        }

        private void ReplayLevel()
        {
            NotifyLevelLoaded();
            OnLevelReplayed.Invoke();
        }

        private void NextLevel()
        {
            _levelIndex++;
            PlayerPrefs.SetInt("level", _levelIndex);
            NotifyLevelLoaded();
            OnLevelChanged.Invoke(_levelIndex);
        }

        private List<Level> LoadLevelData()
        {
            var levelDataJson = Resources.Load<TextAsset>("Data/LevelData").text;
            return JsonUtility.FromJson<LevelData>(levelDataJson).levels;
        }

        private int GetLevelIndex()
        {
            return _levelIndex > _levels.Count - 1 ? _levelIndex % _levels.Count : _levelIndex;
        }

        private void OnEnable()
        {
            NextLevelButtonEvent.OnButtonClicked += NextLevel;
            ReplayButtonEvent.OnButtonClicked += ReplayLevel;
            NextLevelPanelController.OnUpdateRequested += NotifyLevelChanged;
            ProgressPanelController.OnUpdateRequested += NotifyLevelChanged;
        }

        private void OnDisable()
        {
            NextLevelButtonEvent.OnButtonClicked -= NextLevel;
            ReplayButtonEvent.OnButtonClicked -= ReplayLevel;
            NextLevelPanelController.OnUpdateRequested -= NotifyLevelChanged;
            ProgressPanelController.OnUpdateRequested -= NotifyLevelChanged;
        }
    }
}