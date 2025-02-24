using Controllers;
using Enums;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(CoreUIController))]
    public class UIManager : MonoBehaviour
    {
        private CoreUIController _coreUIController;

        private void Awake()
        {
            _coreUIController = GetComponent<CoreUIController>();
        }

        private void Start()
        {
            _coreUIController.OpenPanel(UIPanels.TapToPlay, 2);
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameStates before, GameStates current)
        {
            switch (current)
            {
                case GameStates.Running:
                    _coreUIController.ClosePanel(2);
                    break;
                case GameStates.Failed:
                    _coreUIController.OpenPanel(UIPanels.Replay, 2);
                    break;
                case GameStates.Finished:
                    _coreUIController.OpenPanel(UIPanels.NextLevel, 2);
                    break;
            }
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}
