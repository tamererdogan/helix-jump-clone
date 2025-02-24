using System.Collections.Generic;
using System.Linq;
using Structs;
using UnityEngine;

namespace Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private Transform levelHolder;
        private int _levelIndex;
        private List<Level> _levels;

        private void Start()
        {
            LoadLevelData();
            CreateCurrentLevel();
        }

        private void ResetLevel()
        {
            player.transform.position = new Vector3(0, 9.35f, 0.75f);
            foreach (Transform child in levelHolder.transform)
                Destroy(child.gameObject);
        }
        
        public void NextLevel()
        {
            _levelIndex++;
            CreateCurrentLevel();
        }

        public void CreateCurrentLevel()
        {
            ResetLevel();

            var loadLevelIndex = GetLoadLevelIndex();
            var currentLevel = _levels[loadLevelIndex];

            ColorUtility.TryParseHtmlString(currentLevel.platformColor, out var platformColor);
            ColorUtility.TryParseHtmlString(currentLevel.normalColor, out var normalColor);
            ColorUtility.TryParseHtmlString(currentLevel.obstacleColor, out var obstacleColor);
            ColorUtility.TryParseHtmlString(currentLevel.playerColor, out var playerColor);

            player.transform.GetComponent<MeshRenderer>().material.color = playerColor;

            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.SetParent(levelHolder);
            cylinder.transform.position = new Vector3(0, 5, 0);
            cylinder.transform.localScale = new Vector3(0.7f, 5, 0.7f);
            cylinder.GetComponent<MeshRenderer>().material.color = platformColor;

            CreateFinishDisc(playerColor);
            for (int i = 0; i < currentLevel.indices.Length; i++)
                CreateDisc(i, currentLevel.indices[i], normalColor, obstacleColor);
        }

        private void CreateDisc(int discIndex, LevelIndices levelIndices, Color normalColor, Color obstacleColor)
        {
            var disc = new GameObject { name = "Disc" + discIndex };
            disc.transform.SetParent(levelHolder);
            disc.transform.localPosition = new Vector3(0, discIndex + 1, 0);

            var sliceModel = Resources.Load<GameObject>("Prefabs/GameObject/Slice");
            for (int i = 0; i < 12; i++)
            {
                var sliceObject = Instantiate(sliceModel, disc.transform);
                sliceObject.transform.localPosition = new Vector3(0, 0, 0);
                sliceObject.transform.Rotate(Vector3.up, 30 * i);

                var meshRenderer = sliceObject.GetComponent<MeshRenderer>();
                if (levelIndices.hiddenDiscIndices.Contains(i))
                {
                    sliceObject.tag = "Hidden";
                    sliceObject.GetComponent<MeshCollider>().isTrigger = true;
                    meshRenderer.enabled = false;
                }
                else
                {
                    meshRenderer.material.color = levelIndices.obstacleDiscIndices.Contains(i) ? obstacleColor : normalColor;
                    sliceObject.tag = levelIndices.obstacleDiscIndices.Contains(i) ? "Obstacle" : "Normal";
                }
            }
        }

        private void CreateFinishDisc(Color color)
        {
            var disc = new GameObject { name = "FinishDisc" };
            disc.transform.SetParent(levelHolder);

            var sliceModel = Resources.Load<GameObject>("Prefabs/GameObject/Slice");
            for (int i = 0; i < 12; i++)
            {
                var sliceObject = Instantiate(sliceModel, disc.transform);
                sliceObject.transform.localPosition = new Vector3(0, 0, 0);
                sliceObject.transform.Rotate(Vector3.up, 30 * i);
                var meshRenderer = sliceObject.GetComponent<MeshRenderer>();
                sliceObject.tag = "Finish";
                meshRenderer.material.color = color;
            }
        }
        
        private int GetLoadLevelIndex()
        {
            return _levelIndex > _levels.Count - 1 ? _levelIndex % _levels.Count : _levelIndex;
        }

        private void LoadLevelData()
        {
            var levelDataJson = Resources.Load<TextAsset>("Data/LevelData").text;
            _levels = JsonUtility.FromJson<LevelData>(levelDataJson).levels;
        }
    }
}