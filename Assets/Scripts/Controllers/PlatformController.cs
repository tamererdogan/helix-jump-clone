using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class PlatformController : MonoBehaviour
    {
        private PlatformData _platformData;

        private void Awake()
        {
            _platformData = Resources.Load<PlatformData>("Data/ScriptableObjects/Settings/PlatformData");
        }

        private void RotatePlatform(float value)
        {
            transform.Rotate(Vector3.up, -value * _platformData.rotateSpeed * Time.deltaTime);
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