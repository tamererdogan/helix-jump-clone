using UnityEngine;

namespace Controllers
{
    public class PlatformController : MonoBehaviour
    {        
        [SerializeField]
        private GameObject levelHolder;
        [SerializeField]
        private float rotateSpeed;

        public void RotatePlatform(float value)
        {
            levelHolder.transform.Rotate(Vector3.up, -value * rotateSpeed * Time.deltaTime);
        }

        public void HideDisc(Transform disc)
        {
            disc.gameObject.SetActive(false);
        }
    }
}