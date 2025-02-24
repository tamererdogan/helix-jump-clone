using Controllers;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerManager : MonoBehaviour
    {
        public static UnityAction OnPlayerCollisionFinish = delegate { };
        public static UnityAction<Collision, bool> OnPlayerCollisionObstacle = delegate { };

        private PlayerController _playerController;
        private int _passedDiscCount;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            PlayerHitEvent.OnPlayerCollision += OnPlayerCollision;
            PlayerHitEvent.OnPlayerTrigger += OnPlayerTrigger;
        }

        private void OnPlayerCollision(Transform player, Collision other)
        {
            var isHit = Physics.Raycast(player.position, -transform.up * 0.2f, out var hitInfo);
            var compareTag = isHit ? hitInfo.transform.tag : other.gameObject.tag;
            switch (compareTag)
            {
                case "Normal":
                    _playerController.Jump();
                    _passedDiscCount = 0;
                    break;
                case "Finish":
                    OnPlayerCollisionFinish.Invoke();
                    _passedDiscCount = 0;
                    break;
                case "Obstacle":
                    OnPlayerCollisionObstacle.Invoke(other, _passedDiscCount > 2);
                    if (_passedDiscCount > 2) _passedDiscCount = 0;
                    break;
            }
        }

        private void OnPlayerTrigger(Transform player, Collider other)
        {
            if (other.gameObject.CompareTag("Hidden"))
                _passedDiscCount++;
        }

        private void OnDisable()
        {
            PlayerHitEvent.OnPlayerCollision -= OnPlayerCollision;
            PlayerHitEvent.OnPlayerTrigger -= OnPlayerTrigger;
        }
    }
}