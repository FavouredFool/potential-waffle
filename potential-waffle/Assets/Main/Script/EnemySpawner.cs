using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyBlueprint;
    [SerializeField] float _spawnRingRadius = 8;

    [SerializeField] float _timeBetweenWaves = 12;
    
    float _timeLastWave = float.NegativeInfinity;

    void Update()
    {
        NewWave();
    }

    void NewWave()
    {
        if (Time.time - _timeLastWave > _timeBetweenWaves)
        {

            Debug.Log("NEW WAVE");

            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                Vector2 randomOnOrbit = Random.insideUnitCircle.normalized * _spawnRingRadius;

                Instantiate(_enemyBlueprint, randomOnOrbit, Quaternion.identity);
            }

            _timeLastWave = Time.time;
        }
    }
}
