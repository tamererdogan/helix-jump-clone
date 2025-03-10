﻿using Abstracts;
using Enums;
using ScriptableObjects;
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

        private const float RaycastDistance = 0.2f;

        private Rigidbody _rigidbody;
        private TrailRenderer _trailRenderer;
        private int _sequentialPassedDiscCount;
        private PlayerData _playerData;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _playerData = Resources.Load<PlayerData>("Data/ScriptableObjects/Settings/PlayerData");
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude > _playerData.playerMaxSpeed)
                _rigidbody.velocity = _rigidbody.velocity.normalized * _playerData.playerMaxSpeed;
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _playerData.jumpForce, ForceMode.Impulse);
        }

        private void SetTrailState(bool isOpen)
        {
            _trailRenderer.enabled = isOpen;
        }

        private void OnCollisionEnter(Collision other)
        {
            var isHit = Physics.Raycast(transform.position, -transform.up * RaycastDistance, out var hitInfo);
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
                    if (_sequentialPassedDiscCount > _playerData.safeModeThreshold) OnPlayerPassed.Invoke(hitTransform);
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
            if (_sequentialPassedDiscCount > _playerData.safeModeThreshold)
            {
                OnSafeModeEnter.Invoke();
                SetTrailState(true);
            }
        }
    }
}