using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{

    [SerializeField] float _speed = 5;
    [SerializeField] float _rotateSpeed = 0.5f;

    Rigidbody2D _rigidbody;
    Transform _ship;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ship = GameObject.FindGameObjectWithTag("Ship").transform;
    }

    void FixedUpdate()
    {
        // Aufrecht bleiben relativ zur Erde
        // immer in Richtung des Schiffs b-linen 

        Vector2 direction = (_ship.position - transform.position).normalized;
        _rigidbody.AddForce(direction * _speed);

        float goalRotation = Vector2.SignedAngle(Vector2.up, transform.position);

        int sign = AngleDifference(_rigidbody.rotation + Random.Range(-10f, 10f), goalRotation) > 0 ? 1 : -1;

        _rigidbody.AddTorque(_rotateSpeed * sign);
    }

    

    public static float AngleDifference( float angle1, float angle2 )
    {
        float diff = ( angle2 - angle1 + 180 ) % 360 - 180;
        return diff < -180 ? diff + 360 : diff;
    }


}
