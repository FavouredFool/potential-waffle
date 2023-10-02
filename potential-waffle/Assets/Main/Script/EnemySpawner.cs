using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    [SerializeField] AnimationCurve _gradientScale;

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

        _enemiesPerSecond = _gradientScale.Evaluate(t) * _enemiesPerSecondIncreaseModifier + _enemiesPerSecondStart;

        if (Time.time - _timeLastWave > 1 / _enemiesPerSecond)
        {
            Vector2 randomOnOrbit = Random.insideUnitCircle.normalized * _spawnRingRadius;

            Enemy enemy = Instantiate(_enemyBlueprint, randomOnOrbit, Quaternion.identity);
            enemy.SetSlowMultiplicator(_slowMultiplicator);

            int averageHP = (int)(_gradientScale.Evaluate(t) * _healthIncreaseModifier + _healthStart);
            enemy.GetComponent<Hittable>().SetHP(averageHP + (int)Random.Range(-averageHP/6f, averageHP/6f));

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
