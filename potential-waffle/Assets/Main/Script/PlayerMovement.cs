using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _thrusterForce = 1;
    [SerializeField] ParticleSystem _particles;

    Rigidbody2D _rigidBody;
    Vector2 _velocity = Vector2.zero;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
    }


    void MovePlayer()
    {
        if (Input.GetMouseButton(1))
        {
            _rigidBody.AddForce(transform.up * _thrusterForce);
            _particles.Play();
        }
    }

    void RotatePlayer()
    {
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosMouse.z = 0;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, (worldPosMouse - transform.position).normalized);
        
        // Maybe i should set the rotation through the Rigidbody but eh
        transform.rotation = rotation;

        // rotate around ANGLE!
        //_rigidBody.SetRotation(Quaternion.Angle(rotation, transform.rotation));
    }
}
