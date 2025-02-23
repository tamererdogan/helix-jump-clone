using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static UnityAction<float> OnInputTaken = delegate { };

        void Update()
        {
            if (Input.touchCount == 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) OnInputTaken.Invoke(touch.deltaPosition.x);
        }
    }
}