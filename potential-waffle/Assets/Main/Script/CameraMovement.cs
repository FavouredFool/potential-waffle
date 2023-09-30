using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Transform _ship;
    [SerializeField] Transform _planet;
    [SerializeField] float _initialZoom = 1.2f;
    [SerializeField] float _zoomDecrease = 0.4f;

    void Start()
    {
        _camera.orthographicSize = _initialZoom;
    }

    void Update()
    {
        // TODO this should be buffered for the feels
        transform.position = _ship.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (_ship.position - _planet.position).normalized);

        if (Input.GetMouseButtonDown(2))
        {
            DecreaseZoom();
        }
    }

    public void DecreaseZoom() 
    {
        _camera.orthographicSize += _zoomDecrease;
    }

}
