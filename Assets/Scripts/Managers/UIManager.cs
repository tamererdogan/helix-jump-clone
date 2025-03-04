using Enums;
using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> layers;

        private void Start()
        {
            OpenPanel(UIPanel.Progress, 0);
            OpenPanel(UIPanel.TapToPlay, 2);
        }

        private void OpenPanel(UIPanel uiPanel, byte layerIndex)
        {
            ClosePanel(layerIndex);
            Instantiate(Resources.Load<GameObject>($"Prefabs/UI/{uiPanel}"), layers[layerIndex]);
        }

        private void ClosePanel(byte layerIndex)
        {
            var childCount = layers[layerIndex].childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(layers[layerIndex].GetChild(i).gameObject);
        }

        private void ShowStateSpecificUIPanel(GameState state)
        {
            switch (state)
            {
                case GameState.Running:
                    ClosePanel(2);
                    break;
                case GameState.Failed:
                    OpenPanel(UIPanel.Replay, 2);
                    break;
                case GameState.Finished:
                    OpenPanel(UIPanel.NextLevel, 2);
                    break;
            }
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += ShowStateSpecificUIPanel;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= ShowStateSpecificUIPanel;
        }
    }
}
