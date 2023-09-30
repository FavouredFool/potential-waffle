using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBit : MonoBehaviour
{
    Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        Vector3 direction = Random.insideUnitCircle;
        float strength = Random.Range(0.1f, 0.5f);
        Vector3 force = direction * strength;

        _rigidBody.AddForce(force, ForceMode2D.Impulse);
    }
}
