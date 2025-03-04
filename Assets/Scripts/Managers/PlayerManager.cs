using Structs;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private const string ShaderName = "Universal Render Pipeline/Lit";
        private const float PlayerYOffset = 0.5f;
        private const float PlayerZOffset = 0.75f;

        public static UnityAction<Transform> OnPlayerCreated = delegate { };
        public static UnityAction OnPlayerRemoved = delegate { };

        private Transform _player;

        private void RemovePlayer()
        {
            if (_player != null) Destroy(_player.gameObject);
            OnPlayerRemoved.Invoke();
        }

        private void CreatePlayer(Level currentLevel)
        {
            RemovePlayer();

            ColorUtility.TryParseHtmlString(currentLevel.playerColor, out var playerColor);
            var shader = Shader.Find(ShaderName);
            Material playerMaterial = new Material(shader) { color = playerColor };
            var playerPrefab = Resources.Load<GameObject>("Prefabs/GameObject/Player");
            var player = Instantiate(playerPrefab);
            var discCount = currentLevel.indices.Length;
            player.transform.position = new Vector3(0, discCount + PlayerYOffset, PlayerZOffset);
            player.GetComponent<MeshRenderer>().material = playerMaterial;
            _player = player.transform;
            OnPlayerCreated.Invoke(_player);
        }

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += CreatePlayer;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= CreatePlayer;
        }
    }
}