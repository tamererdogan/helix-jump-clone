using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;
        private CameraData _cameraData;

        private void Awake()
        {
            _cameraData = Resources.Load<CameraData>("Data/ScriptableObjects/Settings/CameraData");
        }

        void LateUpdate()
        {
            var targetPosition = _target.position + _cameraData.offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _cameraData.speed * Time.deltaTime);
        }

        private void SetTarget(Transform playerTransform)
        {
            _target = playerTransform;
        }

        private void OnEnable()
        {
            PlayerManager.OnPlayerCreated += SetTarget;
        }

        private void OnDisable()
        {
            PlayerManager.OnPlayerCreated -= SetTarget;
        }
    }
}
