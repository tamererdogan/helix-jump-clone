using Events;
using UnityEngine;

namespace Managers
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject platformObject;
        [SerializeField]
        private float rotateSpeed;

        private void OnEnable()
        {
            InputManager.OnInputTaken += RotatePlatform;
            PlayerHitEvent.OnBallTrigger += OnBallTrigger;
            PlayerManager.OnPlayerCollisionObstacle += OnPlayerCollisionObstacle;
        }

        private void OnPlayerCollisionObstacle(Collision other, bool isSafeMode)
        {
            if (isSafeMode)
                HideDisc(other.gameObject.transform.parent);
        }

        private void RotatePlatform(float value)
        {
            platformObject.transform.Rotate(Vector3.up, -value * rotateSpeed * Time.deltaTime);
        }

        private void OnBallTrigger(Collider other)
        {
            if (other.gameObject.CompareTag("Hidden"))
                HideDisc(other.gameObject.transform.parent);
        }

        private void OnDisable()
        {
            InputManager.OnInputTaken -= RotatePlatform;
            PlayerHitEvent.OnBallTrigger -= OnBallTrigger;
            PlayerManager.OnPlayerCollisionObstacle -= OnPlayerCollisionObstacle;
        }

        private void HideDisc(Transform disc)
        {
            disc.gameObject.SetActive(false);
        }
    }
}