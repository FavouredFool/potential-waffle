using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _rotateSpeed = 10;

    Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Quaternion q = Quaternion.AngleAxis(_moveSpeed * Time.deltaTime, transform.forward);
        _rigidbody.MovePosition(q * _rigidbody.transform.position);

        _rigidbody.MoveRotation((_rotateSpeed * Time.time)%360);
    }
}
