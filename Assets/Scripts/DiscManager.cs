using System.Collections.Generic;
using UnityEngine;

public class DiscManager : MonoBehaviour
{
    [SerializeField] bool isFinishDisc;
    [SerializeField] List<int> hideDiscIndices;
    [SerializeField] List<int> obstacleDiscIndices;
    [SerializeField] GameObject discModel;
    [SerializeField] Color discColor;
    [SerializeField] Color obstacleColor;

    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            var discObject = Instantiate(discModel, transform);

            discObject.transform.localPosition = new Vector3(0, 0, 0);
            discObject.transform.Rotate(Vector3.up, 30 * i);
            
            var meshRenderer = discObject.GetComponent<MeshRenderer>();
            if (hideDiscIndices.Contains(i))
            {
                discObject.tag = "Hidden";
                discObject.GetComponent<MeshCollider>().isTrigger = true;
                meshRenderer.enabled = false;
            }
            else
            {
                meshRenderer.material.color =  isFinishDisc ? new Color(0,0,0) :
                    obstacleDiscIndices.Contains(i) ? obstacleColor : discColor;
                discObject.tag = isFinishDisc ? "Finish" : obstacleDiscIndices.Contains(i) ? "Obstacle" : "Normal";
            }
        }
    }
}
