using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlanetBit : MonoBehaviour
{
    [SerializeField] float _timeTillKill = 15f;

    Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        Vector3 direction = Random.insideUnitCircle;
        float strength = Random.Range(0.1f, 0.5f);
        Vector3 force = direction * strength;

        _rigidBody.AddForce(force, ForceMode2D.Impulse);

        // Scale down and then destroy the laser
        transform.DOScale(Vector3.zero, _timeTillKill).SetEase(Ease.InCubic).OnComplete(()=> Destroy(this.gameObject));
    }
}
