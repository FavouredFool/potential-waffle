using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _planet1Blueprint;
    [SerializeField] GameObject _planet2Blueprint;
    [SerializeField] GameObject _planet3Blueprint;

    [SerializeField] AnimationCurve _planet1Curve;

    [SerializeField] int _planet1Amount = 100;

    [SerializeField] float _planet1Offset = 2;

    float _orbitRadius = 4.5f;

    



    void Start()
    {
        for(int i = 0; i < _planet1Amount; i++)
        {
            // 1. Have the planet be in the general area of the orbit + offset.
            // 2. Have the planet far away from the other planets

            Vector2 randomOnOrbit = Random.insideUnitCircle.normalized * _orbitRadius;

            float weightedRandomNegativePositiveOne = _planet1Curve.Evaluate(Random.Range(-1f, 1f));
            
            Vector2 offsetAdjustedPosition = randomOnOrbit + randomOnOrbit.normalized * _planet1Offset * weightedRandomNegativePositiveOne;

            Instantiate(_planet1Blueprint, offsetAdjustedPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
}
