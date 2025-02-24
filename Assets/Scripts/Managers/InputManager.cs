using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static UnityAction<float> OnInputTaken = delegate { };
        
        private bool _isRunning;

        void Update()
        {
            if (!_isRunning) return;
            if (Input.touchCount == 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) OnInputTaken.Invoke(touch.deltaPosition.x);
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += CheckIsRunning;
        }

        private void CheckIsRunning(GameStates prev, GameStates current)
        {
            _isRunning = current == GameStates.Running;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= CheckIsRunning;
        }
    }
}