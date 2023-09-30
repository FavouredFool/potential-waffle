using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] Disc _healthbarDisc;
    [SerializeField] Gradient _healthbarGradient;

    int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (_currentHealth <= 0)
        {
            // Kill, spawn planetbits and resources
            Destroy(this.gameObject);
            return;
        }
        
        float healthPercent = _currentHealth / (float)_maxHealth;
        _healthbarDisc.AngRadiansEnd = healthPercent * 2 * Mathf.PI + Mathf.PI/2;

        Color healthbarColor = _healthbarGradient.Evaluate(healthPercent);
        healthbarColor.a = 0.1f;
        _healthbarDisc.Color = healthbarColor;
    }

    public void ReduceHP(int damageAmount)
    {
        _currentHealth -= damageAmount;
    }
}
