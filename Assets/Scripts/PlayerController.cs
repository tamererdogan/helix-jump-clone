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
            case "HiddenDisc":
                DestroyDisc(other.gameObject);
                Debug.Log("Puan arttı");
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

    private void DestroyDisc(GameObject sliceObject)
    {
        Debug.Log("Disk yokedildi.");
        //sliceObject'in üstündeki diski uçur
    }
}
