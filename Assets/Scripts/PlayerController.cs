using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private int _passedDiscCount;
    private int _score;
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
                _passedDiscCount = 0;
                break;
            case "FinishDisc":
                Debug.Log("Oyun bitti puanınız: " + _score);
                break;
            case "ObstacleDisc":
                if (_passedDiscCount > 2)
                {
                    DestroyDisc(other.gameObject);
                    _passedDiscCount = 0;
                    _score += 10;
                    Debug.Log("Fire modu sayesinde kurtuldun!");
                }
                else
                {
                    Time.timeScale = 0f;
                    Debug.Log("Replay: " + _score);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HiddenDisc"))
        {
            DestroyDisc(other.gameObject);
            _passedDiscCount++;
            _score += 10;
        }
    }

    private void DestroyDisc(GameObject sliceObject)
    {
        sliceObject.transform.parent.gameObject.SetActive(false);
    }
}
