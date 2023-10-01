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

    float _orbitRadius = 4.5f;

    
    AnimationCurve[] _planetCurves;


    void Start()
    {
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
