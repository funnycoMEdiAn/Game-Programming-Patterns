using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(5.0f * transform.up, ForceMode.Impulse);
        }

    }
}
