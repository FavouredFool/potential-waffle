using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject _laserBlueprint; 
    [SerializeField] Transform _shootingPointTransform;
    [SerializeField] float _fireRatePerSecond = 1;
    [SerializeField] float _rotateSpeed = 2;

    float _timeLastShot = float.NegativeInfinity;

    Vector2 _gunLookDirection = Vector2.up;

    void Update()
    {
        ShootLaser();
    }

    void FixedUpdate()
    {
        _gunLookDirection = Quaternion.Euler(0, 0, -_rotateSpeed) * _gunLookDirection;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)_gunLookDirection);
    }

    void ShootLaser()
    {
        // If enemies are targeted
        if (Time.time - _timeLastShot > 1 / _fireRatePerSecond)
        {
            Instantiate(_laserBlueprint, _shootingPointTransform.position, _shootingPointTransform.rotation);
            _timeLastShot = Time.time;
        }
    }
}
