using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] GameObject _planetBitBlueprint;
    [SerializeField] GameObject _metalBlueprint;
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
            
            for (int i = 0; i < Random.Range(5, 9); i++) 
            {
                Instantiate(_planetBitBlueprint, transform.position, transform.rotation);
            }

            for (int i = 0; i < Random.Range(1, 2); i++) 
            {
                Instantiate(_metalBlueprint, transform.position, transform.rotation);
            }
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
