using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] _planetBlueprints;
    [SerializeField] int[] _planetAmounts;
    [SerializeField] float[] _planetRetractStrength;
    [SerializeField] AnimationCurve _planet1Curve;
    [SerializeField] AnimationCurve _planet2Curve;
    [SerializeField] AnimationCurve _planet3Curve;
    [SerializeField] float _planetOffsets = 2;
    [SerializeField] float _timeImpulsesRespawn = 5;

    float _orbitRadius = 4.5f;
    float _allPlanets;

    float _timeLastRespawn = float.NegativeInfinity;
    
    AnimationCurve[] _planetCurves;
    ShipMovement _ship;


    void Update()
    {
        // Every x seconds, count all planets, respawn if its less than _allPlanets

        if (Time.time - _timeLastRespawn > _timeImpulsesRespawn)
        {
            int amount = GameObject.FindGameObjectsWithTag("Planet").Length;

            for (int i = 0; i < _allPlanets - amount; i++)
            {
                float percent = Random.Range(0f, 1f);
                int nr;

                if (percent < 15) nr = 2;
                else if (percent < 50) nr = 1;
                else nr = 0;

                SpawnPlanet(nr);
            }

            _timeLastRespawn = Time.time;
        }
    }

    public void SpawnPlanet(int type)
    {
        Vector2 planetPosition = Vector2.zero;

        int breakOutCounter = 0;
        while(breakOutCounter < 10000) 
        {
            breakOutCounter++;
        
            Vector2 randomOnOrbit = Random.insideUnitCircle.normalized * _orbitRadius;
            float weightedRandomNegativePositiveOne = _planetCurves[type].Evaluate(Random.Range(-1f, 1f));
            planetPosition = randomOnOrbit + randomOnOrbit.normalized * _planetOffsets * weightedRandomNegativePositiveOne;

            bool breakout = true;

            if ((planetPosition - (Vector2)_ship.transform.position).magnitude < 6f)
            {
                breakout = false;
            }

            if (breakout)
            {
                break;
            }
        }

        if (breakOutCounter >= 10000)
        {
            Debug.LogWarning("BROKEN OUT OF ENDLESS LOOP");
        }

        Instantiate(_planetBlueprints[type], planetPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    void Start()
    {
        _allPlanets = _planetAmounts[0] + _planetAmounts[1] + _planetAmounts[2];

        _ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipMovement>();
        _planetCurves = new[] {_planet1Curve, _planet2Curve, _planet3Curve};

        for (int j = 0; j < _planetAmounts.Length; j++)
        {
            List<Vector2> planetSpawns = new();

            for(int i = 0; i < _planetAmounts[j]; i++)
            {
                // 1. Have the planet be in the general area of the orbit + offset.
                // 2. Have the planet far away from the other planets

                Vector2 planetPosition = Vector2.zero;

                int breakOutCounter = 0;
                while(breakOutCounter < 10000) 
                {
                    breakOutCounter++;
                
                    Vector2 randomOnOrbit = Random.insideUnitCircle.normalized * _orbitRadius;
                    float weightedRandomNegativePositiveOne = _planetCurves[j].Evaluate(Random.Range(-1f, 1f));
                    planetPosition = randomOnOrbit + randomOnOrbit.normalized * _planetOffsets * weightedRandomNegativePositiveOne;

                    bool breakout = true;
                    for(int k = 0; k < planetSpawns.Count; k++)
                    {
                        if ((planetPosition - planetSpawns[k]).magnitude < _planetRetractStrength[j])
                        {
                            // try positions until you find a position where the planet is sufficiently far away from all other planets
                            breakout = false;
                            break;
                        }
                    }

                    if ((planetPosition - (Vector2)_ship.transform.position).magnitude < 0.8f)
                    {
                        breakout = false;
                    }

                    if (breakout)
                    {
                        break;
                    }
                }

                if (breakOutCounter >= 10000)
                {
                    Debug.LogWarning("BROKEN OUT OF ENDLESS LOOP");
                }

                Instantiate(_planetBlueprints[j], planetPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));

                planetSpawns.Add(planetPosition);
            }
        } 
    }
}
