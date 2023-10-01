using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject _laserBlueprint; 
    [SerializeField] Transform _shootingPointTransform;
    [SerializeField] float _fireRatePerSecond = 1;

    float _timeLastShot = float.NegativeInfinity;

    void Update()
    {
        ShootLaser();
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
