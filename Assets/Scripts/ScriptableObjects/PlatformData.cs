using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlatformData", menuName = "Game/PlatformData")]
    public class PlatformData : ScriptableObject
    {
        public float rotateSpeed;
    }
}
