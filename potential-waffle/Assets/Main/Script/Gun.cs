using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject _laserBlueprint; 
    [SerializeField] Transform _shootingPointTransform;
    [SerializeField] float _fireRatePerSecond = 1;
    [SerializeField] float _rotateSpeed = 2;
    [SerializeField] float _searchRadius = 4;

    float _timeLastShot = float.NegativeInfinity;

    Vector2 _gunLookDirection = Vector2.up;
    Enemy _activeEnemy = null;

    float _closestDistance = float.PositiveInfinity;

    void Update()
    {
        if (_activeEnemy != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _closestDistance, LayerMask.GetMask("Enemy"));

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    ShootLaser();
                }
            }
        }
    }

    void FixedUpdate()
    {
        Enemy [] enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(e => e.GetComponent<Enemy>()).ToArray();

        _closestDistance = float.PositiveInfinity;

        foreach(Enemy enemy in enemies)
        {
            float dist = (enemy.transform.position - transform.position).magnitude;
            if (dist <= _searchRadius)
            {
                if (dist < _closestDistance)
                {
                    _closestDistance = dist;
                    _activeEnemy = enemy;
                }
            }
        }

        if (_activeEnemy == null)
        {
            _gunLookDirection = Quaternion.Euler(0, 0, -_rotateSpeed) * _gunLookDirection;
            Quaternion goalQuat = Quaternion.LookRotation(Vector3.forward, (Vector3)_gunLookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalQuat, _rotateSpeed);
        }
        else
        {
            _gunLookDirection = (_activeEnemy.transform.position - transform.position).normalized;
            Quaternion goalQuat = Quaternion.LookRotation(Vector3.forward, (Vector3)_gunLookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goalQuat, _rotateSpeed*10);
        }
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
