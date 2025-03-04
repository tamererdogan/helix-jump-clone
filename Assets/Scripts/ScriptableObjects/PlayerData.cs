using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int safeModeThreshold;
        public float jumpForce;
        public float playerMaxSpeed ;
    }
}