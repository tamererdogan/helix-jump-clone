using Controllers;
using Enums;
using Events;
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
            TapToStartButtonEvent.OnButtonClicked += () => _coreUIController.ClosePanel(2);
        }

        private void OnDisable()
        {
            TapToStartButtonEvent.OnButtonClicked -= () => _coreUIController.ClosePanel(2);
        }
    }
}
