using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class PlayerHitEvent : MonoBehaviour
    {
        public static UnityAction<Collision> OnPlayerCollision = delegate { };
        public static UnityAction<Collider> OnPlayerTrigger = delegate { };

        private void OnCollisionEnter(Collision other)
        {
            OnPlayerCollision?.Invoke(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerTrigger?.Invoke(other);
        }
    }
}