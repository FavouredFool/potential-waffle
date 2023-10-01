using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static ResourceManager;
using Shapes;

public class Hittable : MonoBehaviour
{
    [SerializeField] ResourceType _resourceType;
    [SerializeField] int _maxHealth;
    [SerializeField] int _minResource;
    [SerializeField] int _maxResource;
    [SerializeField] GameObject _remainingElementBlueprint;
    [SerializeField] GameObject _ResourceBlueprint;
    [SerializeField] Disc _healthbarDisc;
    [SerializeField] Gradient _healthbarGradient;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _damageColor;

    int _currentHealth;

    Tween _healthBarFadeOutTween;
    Tween _colorBlinkTween;
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
                Instantiate(_remainingElementBlueprint, transform.position, transform.rotation);
            }

            for (int i = 0; i < Random.Range(_minResource, _maxResource+1); i++) 
            {
                Instantiate(_ResourceBlueprint, transform.position, transform.rotation);
            }

            _healthBarFadeOutTween.Kill();
            _colorBlinkTween.Kill();
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
        // Flash white

        _currentHealth -= damageAmount;

        if (_healthBarFadeOutTween != null)
        {
            _healthBarFadeOutTween.Kill();
        }
        _healthBarFadeOutTween = DOTween.To(x => _discAlpha = x, 0.2f, 0, 3).SetEase(Ease.InCubic);

        if (_colorBlinkTween != null)
        {
            _colorBlinkTween.Kill();
        }
        _colorBlinkTween = DOTween.Sequence().Append(_spriteRenderer.DOColor(_damageColor, 0.05f)).Append(_spriteRenderer.DOColor(Color.white,0.05f));
    }

    public ResourceType GetResourceType()
    {
        return _resourceType;
    }
}
