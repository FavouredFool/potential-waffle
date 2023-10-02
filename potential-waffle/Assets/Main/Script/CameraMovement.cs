using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Transform _ship;
    [SerializeField] Transform _planet;
    [SerializeField] float _initialZoom = 1.2f;
    [SerializeField] float _zoomDecrease = 0.4f;

    float _currentZoom;
    float _goalZoom;

    void Start()
    {
        _camera.orthographicSize = _initialZoom;
        _currentZoom = _initialZoom;
        _goalZoom = _initialZoom;
    }

    void Update()
    {
        if (_ship == null)
        {
            return;
        }

        // TODO this should be buffered for the feels
        transform.position = _ship.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (_ship.position - _planet.position).normalized);

        _camera.orthographicSize = _currentZoom;
    }

    public void DecreaseZoom() 
    {
        _goalZoom = _goalZoom + _zoomDecrease;
        DOTween.To(x => _currentZoom = x, _currentZoom, _goalZoom, 0.75f).SetEase(Ease.InOutQuad);
    }
}
