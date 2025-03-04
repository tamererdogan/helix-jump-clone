using Managers;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] 
        private Vector3 offset;
        [SerializeField] 
        private float speed;

        private Transform _target;

        void LateUpdate()
        {
            var targetPosition = _target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }

        private void SetTarget(Transform playerTransform)
        {
            _target = playerTransform;
        }

        private void RemoveTarget()
        {
            _target = null;
        }

        private void OnEnable()
        {
            PlayerManager.OnPlayerCreated += SetTarget;
            PlayerManager.OnPlayerRemoved += RemoveTarget;
        }

        private void OnDisable()
        {
            PlayerManager.OnPlayerCreated -= SetTarget;
            PlayerManager.OnPlayerRemoved -= RemoveTarget;
        }
    }
}
