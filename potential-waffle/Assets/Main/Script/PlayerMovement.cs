using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] ResourceManager _resourceManager;
    [SerializeField] GameObject _laserBlueprint; 
    [SerializeField] float _thrusterForce = 1;
    [SerializeField] ParticleSystem _particles;
    [SerializeField] Transform _leftLaserSpawn;
    [SerializeField] Transform _rightLaserSpawn;

    Rigidbody2D _rigidBody;
    Vector2 _velocity = Vector2.zero;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ShootLaser();
    }

    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
    }

    void ShootLaser()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_laserBlueprint, _leftLaserSpawn.position, _leftLaserSpawn.rotation);
            Instantiate(_laserBlueprint, _rightLaserSpawn.position, _rightLaserSpawn.rotation);
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
        
        // Maybe i should set the rotation through the Rigidbody but eh
        transform.rotation = rotation;

        // rotate around ANGLE!
        //_rigidBody.SetRotation(Quaternion.Angle(rotation, transform.rotation));
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("triggered");
        ResourceCollectable resource = collider.gameObject.GetComponent<ResourceCollectable>();
    
        if (resource != null) 
        {
            Destroy(resource.gameObject);

            _resourceManager.AddResource(resource.GetResourceType());
        }

    }
}
