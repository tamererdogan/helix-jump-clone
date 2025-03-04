using System.Linq;
using Abstracts;
using Controllers;
using Enums;
using Structs;
using UnityEngine;

namespace Managers
{
    public class PlatformManager : MonoBehaviour
    {
        private const string ShaderName = "Universal Render Pipeline/Lit";
        private const string PlatformHolderName = "PlatformHolder";
        private const string FinishDiscName = "FinishDisc";
        private const int SliceAngle = 30;
        private const float DiscSpaceSize = 0.5f;
        private const float CylinderScale = 0.7f;

        private Transform _platformHolder;

        private void Awake()
        {
            ResetPlatform();
        }

        private void ResetPlatform()
        {
            if (_platformHolder != null) Destroy(_platformHolder.gameObject);
            _platformHolder = new GameObject(PlatformHolderName).transform;
            _platformHolder.gameObject.AddComponent<PlatformController>();
        }

        private void CreatePlatform(Level currentLevel)
        {
            ResetPlatform();

            ColorUtility.TryParseHtmlString(currentLevel.platformColor, out var platformColor);
            ColorUtility.TryParseHtmlString(currentLevel.normalColor, out var normalColor);
            ColorUtility.TryParseHtmlString(currentLevel.obstacleColor, out var obstacleColor);
            ColorUtility.TryParseHtmlString(currentLevel.playerColor, out var playerColor);

            var shader = Shader.Find(ShaderName);
            Material platformMaterial = new Material(shader) { color = platformColor };
            Material normalDiscMaterial = new Material(shader) { color = normalColor };
            Material obstacleDiscMaterial = new Material(shader) { color = obstacleColor };
            Material finishDiscMaterial = new Material(shader) { color = playerColor };

            var discCount = currentLevel.indices.Length;
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            var cylinderSize = (discCount + 1) * DiscSpaceSize;
            cylinder.transform.SetParent(_platformHolder);
            cylinder.transform.position = Vector3.up * cylinderSize;
            cylinder.transform.localScale = new Vector3(CylinderScale, cylinderSize, CylinderScale);
            cylinder.GetComponent<MeshRenderer>().material = platformMaterial;

            currentLevel.indices = currentLevel.indices.Reverse().ToArray();

            var finishDisc = CreateFinishDisc(finishDiscMaterial);
            finishDisc.transform.SetParent(_platformHolder);
            for (int i = 0; i < discCount; i++)
            {
                var discName = "Disc" + i;
                var indices = currentLevel.indices[i];
                var disc = CreateDisc(
                    discName, 
                    indices.hiddenDiscIndices, 
                    indices.obstacleDiscIndices, 
                    normalDiscMaterial, 
                    obstacleDiscMaterial
                );
                disc.transform.SetParent(_platformHolder);
                disc.transform.localPosition = Vector3.up * (i + 1);
            }
        }

        private GameObject CreateDisc(
            string discName,
            int[] hiddenDiscIndices,
            int[] obstacleDiscIndices,
            Material normalDiscMaterial,
            Material obstacleDiscMaterial
        ) {
            var disc = new GameObject(discName);
            var sliceModel = Resources.Load<GameObject>("Prefabs/GameObject/Slice");
            for (int i = 0; i < 12; i++)
            {
                var sliceObject = Instantiate(sliceModel, disc.transform);
                sliceObject.transform.localPosition = Vector3.zero;
                sliceObject.transform.Rotate(Vector3.up, SliceAngle * i);

                var meshRenderer = sliceObject.GetComponent<MeshRenderer>();
                sliceObject.TryGetComponent(out IGround ground);
                if (ground == null) continue;
                if (hiddenDiscIndices.Contains(i))
                {
                    ground.SetGroundType(GroundType.Hidden);
                    sliceObject.GetComponent<MeshCollider>().isTrigger = true;
                    meshRenderer.enabled = false;
                } else if (obstacleDiscIndices.Contains(i))
                {
                    ground.SetGroundType(GroundType.Obstacle);
                    meshRenderer.material = obstacleDiscMaterial;
                }
                else
                {
                    ground.SetGroundType(GroundType.Normal);
                    meshRenderer.material = normalDiscMaterial;
                }
            }

            return disc;
        }

        private GameObject CreateFinishDisc(Material finishDiscMaterial)
        {
            var disc = new GameObject(FinishDiscName);
            var sliceModel = Resources.Load<GameObject>("Prefabs/GameObject/Slice");
            for (int i = 0; i < 12; i++)
            {
                var sliceObject = Instantiate(sliceModel, disc.transform);
                sliceObject.transform.localPosition = Vector3.zero;
                sliceObject.transform.Rotate(Vector3.up, SliceAngle * i);
                var meshRenderer = sliceObject.GetComponent<MeshRenderer>();
                meshRenderer.material = finishDiscMaterial;
                sliceObject.TryGetComponent(out IGround ground);
                if (ground == null) continue;
                ground.SetGroundType(GroundType.Finish);
            }

            return disc;
        }

        private void HideDisc(Transform disc)
        {
            disc.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += CreatePlatform;
            PlayerController.OnPlayerPassed += HideDisc;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= CreatePlatform;
            PlayerController.OnPlayerPassed -= HideDisc;
        }
    }
}