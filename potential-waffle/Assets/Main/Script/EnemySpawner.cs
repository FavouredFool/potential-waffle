using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemyBlueprint;
    [SerializeField] float _spawnRingRadius = 8;
    [SerializeField] float _timeBetweenWaves = 12;
    
    float _timeLastWave = float.NegativeInfinity;
    int _slowMultiplicator = 1;

    void Update()
    {
        NewWave();
    }

    void NewWave()
    {
        if (Time.time - _timeLastWave > _timeBetweenWaves)
        {
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                Vector2 randomOnOrbit = Random.insideUnitCircle.normalized * _spawnRingRadius;

                Enemy enemy = Instantiate(_enemyBlueprint, randomOnOrbit, Quaternion.identity);
                enemy.SetSlowMultiplicator(_slowMultiplicator);
            }

            _timeLastWave = Time.time;
        }
    }

    public void UpdateSlow()
    {
        _slowMultiplicator*=4;
    }
}
