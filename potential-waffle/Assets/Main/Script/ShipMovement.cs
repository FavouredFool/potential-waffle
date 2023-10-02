using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Shapes;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _rotateSpeed = 10;
    [SerializeField] int _maxHealth;
    [SerializeField] GameObject _remainingElementBlueprint;
    [SerializeField] Disc _healthbarDisc;
    [SerializeField] Gradient _healthbarGradient;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _damageColor;

    Rigidbody2D _rigidbody;
    int _currentHealth;
    Tween _healthBarFadeOutTween;
    Tween _colorBlinkTween;
    float _discAlpha = 0.1f;
    AudioManager _audio;

    void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }

    void FixedUpdate()
    {
        Quaternion q = Quaternion.AngleAxis(_moveSpeed * Time.deltaTime, transform.forward);
        _rigidbody.MovePosition(q * _rigidbody.transform.position);

        _rigidbody.MoveRotation((_rotateSpeed * Time.time)%360);
    }

    void Update()
    {
        if (_currentHealth <= 0)
        {
            // Kill, spawn planetbits and resources
            
            // Game over!#
            // Does the station explode?

            _audio.Play("ShipCrash");
            
            for (int i = 0; i < Random.Range(60, 70); i++) 
            {
                Instantiate(_remainingElementBlueprint, transform.position, transform.rotation);
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

        _audio.Play("PlanetDestroyed");

        _currentHealth -= damageAmount;

        if (_healthBarFadeOutTween != null)
        {
            _healthBarFadeOutTween.Kill();
        }
        _healthBarFadeOutTween = DOTween.To(x => _discAlpha = x, 0.5f, 0.1f, 3).SetEase(Ease.InCubic);

        if (_colorBlinkTween != null)
        {
            _colorBlinkTween.Kill();
        }
        _colorBlinkTween = DOTween.Sequence().Append(_spriteRenderer.DOColor(_damageColor, 0.05f)).Append(_spriteRenderer.DOColor(Color.white,0.05f));
    }

    public void UpgradeHealth()
    {
        // 4 -> 8 -> 16
        _maxHealth *= 2;
        _currentHealth += _maxHealth / 2;
    }

    public void UpgradeShipSpeed()
    {
        _moveSpeed *= 2;
    }

    public void Heal()
    {
        _currentHealth = Mathf.Min(_currentHealth+1, _maxHealth);
    }

}
