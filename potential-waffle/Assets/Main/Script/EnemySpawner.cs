using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemyBlueprint;
    [SerializeField] float _spawnRingRadius = 8;
    [SerializeField] float _reachMaxTime;
    [SerializeField] float _healthStart;
    [SerializeField] float _healthIncreaseModifier;
    [SerializeField] float _enemiesPerSecondStart;
    [SerializeField] float _enemiesPerSecondIncreaseModifier;
    
    float _timeLastWave = float.NegativeInfinity;

    float _enemiesPerSecond;
    float _startTime = -1;
    int _slowMultiplicator = 1;


    void Start()
    {
        _enemiesPerSecond = _enemiesPerSecondStart;
    }

    void Update()
    {
        if (_startTime < 0)
        {
            return;
        }

        float t = (Time.time - _startTime) / _reachMaxTime;

        _enemiesPerSecond = t * _enemiesPerSecondIncreaseModifier + _enemiesPerSecondStart;

        if (Time.time - _timeLastWave > 1 / _enemiesPerSecond)
        {
            Vector2 randomOnOrbit = Random.insideUnitCircle.normalized * _spawnRingRadius;

            Enemy enemy = Instantiate(_enemyBlueprint, randomOnOrbit, Quaternion.identity);
            enemy.SetSlowMultiplicator(_slowMultiplicator);

            int averageHP = (int)(t * _healthIncreaseModifier + _healthStart);
            enemy.GetComponent<Hittable>().SetHP(averageHP + Random.Range(-averageHP/4, (averageHP+1)/4));

            _timeLastWave = Time.time;
        }
    }

    public void UpdateSlow()
    {
        _slowMultiplicator*=4;
    }

    public void SetStartTime()
    {
        _startTime = Time.time;
    }
}
