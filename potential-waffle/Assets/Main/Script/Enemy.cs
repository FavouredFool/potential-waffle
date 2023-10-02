using UnityEngine;
using UnityEngine.Timeline;
using DG.Tweening;
using System.Reflection.Emit;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] float _rotateSpeed = 0.5f;
    [SerializeField] float _slowAmount = 5;
    [SerializeField] float _slowDuration = 1;
    Rigidbody2D _rigidbody;
    Transform _ship;

    Tween _slowTween;

    float _currentSpeed;
    float _slowMultiplicator = 1;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ship = GameObject.FindGameObjectWithTag("Ship")?.transform;
        _currentSpeed = _speed;
    }

    void FixedUpdate()
    {
        // Aufrecht bleiben relativ zur Erde
        // immer in Richtung des Schiffs b-linen 

        if (_ship == null)
        {
            return;
        }

        Vector2 direction = (_ship.position - transform.position).normalized;
        _rigidbody.AddForce(direction * _currentSpeed);

        float goalRotation = Vector2.SignedAngle(Vector2.up, transform.position);

        int sign = AngleDifference(_rigidbody.rotation + Random.Range(-10f, 10f), goalRotation) > 0 ? 1 : -1;

        _rigidbody.AddTorque(_rotateSpeed * sign);
    }

    public static float AngleDifference( float angle1, float angle2 )
    {
        float diff = ( angle2 - angle1 + 180 ) % 360 - 180;
        return diff < -180 ? diff + 360 : diff;
    }


    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (_ship == null)
        {
            return;
        }
        if (collider.gameObject == _ship.gameObject)
        {
            ShipMovement ship = _ship.GetComponent<ShipMovement>();
            ship.ReduceHP(1);
            GetComponent<Hittable>().ReduceHP(10000);
        }
    }

    public void SlowEnemy()
    {
        if (_slowTween != null) _slowTween.Kill();

        _slowTween = DOTween.To(x => _currentSpeed = x, _speed / (_slowAmount * _slowMultiplicator), _speed, _slowDuration * _slowMultiplicator).SetEase(Ease.OutCubic);
    }

    public void SetSlowMultiplicator(int multiplicator)
    {
        _slowMultiplicator = multiplicator;
    }

}
