using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "Game/CameraData")]
    public class CameraData : ScriptableObject
    {
        public Vector3 offset;
        public float speed;
    }
}