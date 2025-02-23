using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class PlayerHitEvent : MonoBehaviour
    {
        public static UnityAction<Collision> OnBallCollision = delegate { };
        public static UnityAction<Collider> OnBallTrigger = delegate { };

        private void OnCollisionEnter(Collision other)
        {
            OnBallCollision?.Invoke(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnBallTrigger?.Invoke(other);
        }
    }
}