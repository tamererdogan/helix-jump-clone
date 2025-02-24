using Controllers;
using Events;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(LevelController))]
    public class LevelManager : MonoBehaviour
    {
        private LevelController _levelController;

        private void Awake()
        {
            _levelController = GetComponent<LevelController>();
        }

        private void OnEnable()
        {
            NextLevelButtonEvent.OnButtonClicked += NextLevel;
            ReplayButtonEvent.OnButtonClicked += ReplayLevel;
        }

        private void ReplayLevel()
        {
            _levelController.CreateCurrentLevel();
        }

        private void NextLevel()
        {
            _levelController.NextLevel();
        }

        private void OnDisable()
        {
            NextLevelButtonEvent.OnButtonClicked -= NextLevel;
            ReplayButtonEvent.OnButtonClicked -= ReplayLevel;
        }
    }
}