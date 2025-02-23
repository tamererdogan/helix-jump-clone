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
            PlayerHitEvent.OnBallCollision += OnBallCollision;
            PlayerHitEvent.OnBallTrigger += OnBallTrigger;
        }

        private void OnBallCollision(Collision other)
        {
            switch (other.gameObject.tag)
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

        private void OnBallTrigger(Collider other)
        {
            if (other.gameObject.CompareTag("Hidden"))
                _passedDiscCount++;
        }

        private void OnDisable()
        {
            PlayerHitEvent.OnBallCollision -= OnBallCollision;
            PlayerHitEvent.OnBallTrigger -= OnBallTrigger;
        }
    }
}