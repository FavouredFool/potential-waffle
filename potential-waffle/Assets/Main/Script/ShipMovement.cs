using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipMovement : MonoBehaviour
{
    
    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _rotateSpeed = 10;

    void Start()
    {
        
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, Time.deltaTime * _moveSpeed);
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * _rotateSpeed));
    }
}
