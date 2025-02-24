using Controllers;
using Events;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(PlatformController))]
    public class PlatformManager : MonoBehaviour
    {
        private PlatformController _platformController;
        
        private void Awake()
        {
            _platformController = GetComponent<PlatformController>();
        }

        private void OnEnable()
        {
            InputManager.OnInputTaken += _platformController.RotatePlatform;
            PlayerHitEvent.OnPlayerTrigger += OnPlayerTrigger;
            PlayerManager.OnPlayerCollisionObstacle += OnPlayerCollisionObstacle;
        }

        private void OnPlayerCollisionObstacle(Collision other, bool isSafeMode)
        {
            if (isSafeMode)
                _platformController.HideDisc(other.gameObject.transform.parent);
        }

        private void OnPlayerTrigger(Collider other)
        {
            if (other.gameObject.CompareTag("Hidden"))
                _platformController.HideDisc(other.gameObject.transform.parent);
        }

        private void OnDisable()
        {
            InputManager.OnInputTaken -= _platformController.RotatePlatform;
            PlayerHitEvent.OnPlayerTrigger -= OnPlayerTrigger;
            PlayerManager.OnPlayerCollisionObstacle -= OnPlayerCollisionObstacle;
        }
    }
}