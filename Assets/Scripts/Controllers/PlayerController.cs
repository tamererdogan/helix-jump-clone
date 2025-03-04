using Abstracts;
using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static UnityAction<Transform> OnPlayerPassed = delegate { };
        public static UnityAction OnPlayerFailed = delegate { };
        public static UnityAction OnPlayerFinished = delegate { };
        public static UnityAction OnSafeModeEnter = delegate { };
        public static UnityAction OnSafeModeExit = delegate { };

        private const int SafeModeThreshold = 2; //TODO:SO'dan al
        
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float raycastDistance;

        private Rigidbody _rigidbody;
        private TrailRenderer _trailRenderer;
        private int _sequentialPassedDiscCount;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void SetTrailState(bool isOpen)
        {
            _trailRenderer.enabled = isOpen;
        }

        private void OnCollisionEnter(Collision other)
        {
            var isHit = Physics.Raycast(transform.position, -transform.up * raycastDistance, out var hitInfo);
            var hitTransform = isHit ? hitInfo.transform : other.transform;
            hitTransform.TryGetComponent(out IGround ground);
            if (ground == null) return;
            var groundType = ground.GetGroundType();
            switch (groundType)
            {
                case GroundType.Normal:
                    Jump();
                    break;
                case GroundType.Finish:
                    OnPlayerFinished.Invoke();
                    break;
                case GroundType.Obstacle:
                    if (_sequentialPassedDiscCount > SafeModeThreshold) OnPlayerPassed.Invoke(hitTransform);
                    else OnPlayerFailed.Invoke();
                    break;
            }

            _sequentialPassedDiscCount = 0;
            OnSafeModeExit.Invoke();
            SetTrailState(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerPassed.Invoke(other.transform);
            _sequentialPassedDiscCount++;
            if (_sequentialPassedDiscCount > SafeModeThreshold)
            {
                OnSafeModeEnter.Invoke();
                SetTrailState(true);
            }
        }
    }
}