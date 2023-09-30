using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _shootingForce = 3f;
    [SerializeField] float _timeTillKill = 0.35f;

    Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        Vector2 direction = (Vector2) transform.up;
        _rigidBody.AddForce(direction * _shootingForce, ForceMode2D.Impulse);

        // Scale down and then destroy the laser
        transform.DOScale(Vector3.zero, _timeTillKill).SetEase(Ease.InCubic).OnComplete(()=> Destroy(this.gameObject));
    }
}
