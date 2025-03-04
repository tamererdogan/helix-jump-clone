using Structs;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static UnityAction<Transform> OnPlayerCreated = delegate { };

        private const float PlayerYOffset = 0.5f;
        private const float PlayerZOffset = 0.75f;

        private Transform _player;
        private MeshRenderer _meshRenderer;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            var playerPrefab = Resources.Load<GameObject>("Prefabs/GameObject/Player");
            _player = Instantiate(playerPrefab, new Vector3(0f, 0.5f, 2f), Quaternion.identity).transform;
            _rigidbody = _player.GetComponent<Rigidbody>();
            _meshRenderer = _player.GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            OnPlayerCreated.Invoke(_player);
        }

        private void UpdatePlayer(Level currentLevel)
        {
            ColorUtility.TryParseHtmlString(currentLevel.playerColor, out var playerColor);
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            _meshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_BaseColor", playerColor);
            _meshRenderer.SetPropertyBlock(propertyBlock);

            var discCount = currentLevel.indices.Length;
            _rigidbody.MovePosition(new Vector3(0, discCount + PlayerYOffset, PlayerZOffset));
        }

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += UpdatePlayer;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= UpdatePlayer;
        }
    }
}