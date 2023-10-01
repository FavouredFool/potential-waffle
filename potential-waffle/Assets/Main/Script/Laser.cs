using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static ResourceManager;

public class Laser : MonoBehaviour
{
    public enum LaserType {SMALL, BIG}

    [SerializeField] LaserType _laserType;
    [SerializeField] float _shootingForce = 3f;
    [SerializeField] float _timeTillKill = 0.35f;

    Rigidbody2D _rigidBody;

    Tween _scaleTween;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        Vector2 direction = (Vector2) transform.up;
        _rigidBody.AddForce(direction * _shootingForce, ForceMode2D.Impulse);

        // Scale down and then destroy the laser
        _scaleTween = transform.DOScale(Vector3.zero, _timeTillKill).SetEase(Ease.InCubic).OnComplete(()=> Destroy(this.gameObject));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Hittable hittable = collision.gameObject.GetComponent<Hittable>();

        ResourceType resourceType = hittable.GetResourceType();

        if (hittable != null)
        {
            if (resourceType == ResourceType.METAL)
            {
                hittable.ReduceHP(1);
            }
            else
            {
                if (_laserType == LaserType.BIG)
                {
                    hittable.ReduceHP(1);
                }
                else
                {
                    hittable.ReduceHP(0);
                }
            }

            
        }

        if (_scaleTween != null)
        {
            _scaleTween.Kill();
        }
        
        Destroy(this.gameObject);
    }
}
