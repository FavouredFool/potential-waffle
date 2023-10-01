using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;
using DG.Tweening;

public class Planet : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] int _minMetal;
    [SerializeField] int _maxMetal;
    [SerializeField] GameObject _planetBitBlueprint;
    [SerializeField] GameObject _metalBlueprint;
    [SerializeField] Disc _healthbarDisc;
    [SerializeField] Gradient _healthbarGradient;

    int _currentHealth;

    Tween _healthBarFadeOut;
    float _discAlpha = 0;

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

            for (int i = 0; i < Random.Range(_minMetal, _maxMetal+1); i++) 
            {
                Instantiate(_metalBlueprint, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);

            return;
        }
        
        float healthPercent = _currentHealth / (float)_maxHealth;
        _healthbarDisc.AngRadiansEnd = healthPercent * 2 * Mathf.PI + Mathf.PI/2;

        Color healthbarColor = _healthbarGradient.Evaluate(healthPercent);
        healthbarColor.a = _discAlpha;
        _healthbarDisc.Color = healthbarColor;
    }

    public void ReduceHP(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_healthBarFadeOut != null)
        {
            _healthBarFadeOut.Kill();
        }
        _healthBarFadeOut = DOTween.To(x => _discAlpha = x, 0.2f, 0, 3).SetEase(Ease.InCubic);
    }
}
