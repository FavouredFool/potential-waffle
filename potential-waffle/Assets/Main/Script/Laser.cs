using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _shootingForce = 3f;
    [SerializeField] float _timeTillKill = 0.35f;

    Rigidbody2D _rigidBody;

    Tween _scaleTween;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        Vector2 direction = (Vector2) transform.up;
        _rigidBody.AddForce(direction * _shootingForce, ForceMode2D.Impulse);

        // Scale down and then destroy the laser
        _scaleTween = transform.DOScale(Vector3.zero, _timeTillKill).SetEase(Ease.InCubic).OnComplete(()=> Destroy(this.gameObject));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Planet planet = collision.gameObject.GetComponent<Planet>();

        if (planet != null)
        {
            planet.ReduceHP(1);
        }

        if (_scaleTween != null)
        {
            _scaleTween.Kill();
        }
        
        Destroy(this.gameObject);
    }
}
