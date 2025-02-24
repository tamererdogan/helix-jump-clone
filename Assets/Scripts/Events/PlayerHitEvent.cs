using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class PlayerHitEvent : MonoBehaviour
    {
        public static UnityAction<Transform, Collision> OnPlayerCollision = delegate { };
        public static UnityAction<Transform, Collider> OnPlayerTrigger = delegate { };

        private void OnCollisionEnter(Collision other)
        {
            OnPlayerCollision?.Invoke(transform, other);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerTrigger?.Invoke(transform, other);
        }
    }
}