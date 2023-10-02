using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] ResourceManager _resourceManager;
    [SerializeField] Laser _laserBlueprint; 
    [SerializeField] float _thrusterForce = 1;
    [SerializeField] float _fireRatePerSecond = 2;
    [SerializeField] ParticleSystem _particles;
    [SerializeField] Transform _leftLaserSpawn;
    [SerializeField] Transform _rightLaserSpawn;


    float _timeLastShot = float.NegativeInfinity;
    int _timeTillKillMultiplicator = 1;

    Rigidbody2D _rigidBody;
    Vector2 _velocity = Vector2.zero;

    ShipMovement _ship;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipMovement>();
    }

    void Update()
    {
        if (_ship == null)
        {
            return;
        }
        ShootLaser();
    }

    void FixedUpdate()
    {
        if (_ship == null)
        {
            return;
        }
        RotatePlayer();
        MovePlayer();
    }

    void ShootLaser()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - _timeLastShot > 1 / _fireRatePerSecond)
            {
                Laser laser1 = Instantiate(_laserBlueprint, _leftLaserSpawn.position, _leftLaserSpawn.rotation);
                Laser laser2 = Instantiate(_laserBlueprint, _rightLaserSpawn.position, _rightLaserSpawn.rotation);
                
                laser1.SetTimeTillKill(_timeTillKillMultiplicator);
                laser2.SetTimeTillKill(_timeTillKillMultiplicator);
                
                _timeLastShot = Time.time;
            }
        }
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

        _rigidBody.SetRotation(rotation);

        // rotate around ANGLE!
        //_rigidBody.SetRotation(Quaternion.Angle(rotation, transform.rotation));
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        ResourceCollectable resource = collider.gameObject.GetComponent<ResourceCollectable>();
    
        if (resource != null) 
        {
            Destroy(resource.gameObject);

            _resourceManager.AddResource(resource.GetResourceType());
        }

    }

    public void UpgradeFireRate()
    {
        _fireRatePerSecond *= 2;
    }

    public void UpgradeTimeTillKillMultiplicator()
    {
        _timeTillKillMultiplicator *= 2;
    }
}
