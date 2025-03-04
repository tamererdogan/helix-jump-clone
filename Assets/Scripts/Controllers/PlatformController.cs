using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlatformController : MonoBehaviour
    {
        private float _rotateSpeed = 18; //TODO: SO'dan al bunu

        private void RotatePlatform(float value)
        {
            transform.Rotate(Vector3.up, -value * _rotateSpeed * Time.deltaTime);
        }

        private void OnEnable()
        {
            InputManager.OnInputTaken += RotatePlatform;
        }

        private void OnDisable()
        {
            InputManager.OnInputTaken -= RotatePlatform;
        }
    }
}