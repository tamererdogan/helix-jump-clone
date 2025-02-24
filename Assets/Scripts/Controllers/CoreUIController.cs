using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class CoreUIController : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> layers;

        public void OpenPanel(UIPanels uiPanel, byte layerIndex)
        {
            ClosePanel(layerIndex);
            Instantiate(Resources.Load<GameObject>($"Screens/{uiPanel}"), layers[layerIndex]);
        }

        public void ClosePanel(byte layerIndex)
        {
            var childCount = layers[layerIndex].childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(layers[layerIndex].GetChild(i).gameObject);
        }
    }
}