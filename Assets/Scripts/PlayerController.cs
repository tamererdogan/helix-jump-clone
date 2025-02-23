using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    private bool _isFireMode = false;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Disc":
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                break;
            case "FinishDisc":
                Debug.Log("Oyun bitti panelini göster");
                break;
            case "ObstacleDisc":
                if (_isFireMode)
                {
                    DestroyDisc(other.gameObject);
                    _isFireMode = false;
                }
                else
                {
                    Debug.Log("Replay panelini göster");
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("HiddenDisc")) return;
        DestroyDisc(other.gameObject);
        Debug.Log("Puan arttı");
    }

    private void DestroyDisc(GameObject sliceObject)
    {
        sliceObject.transform.parent.gameObject.SetActive(false);
    }
}
