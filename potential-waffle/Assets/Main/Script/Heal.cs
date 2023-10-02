using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] float _speed = 5;

    Rigidbody2D _rigidbody;
    ShipMovement _ship;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ship = GameObject.FindGameObjectWithTag("Ship")?.GetComponent<ShipMovement>();
    }

    void FixedUpdate()
    {
        if (_ship == null)
        {
            return;
        }

        Vector2 direction = (_ship.transform.position - transform.position).normalized;
        _rigidbody.AddForce(direction * _speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _ship.gameObject)
        {
            _ship.Heal();
            Destroy(gameObject);
        }
        
    }
}
