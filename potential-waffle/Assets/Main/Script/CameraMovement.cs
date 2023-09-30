using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _ship;
    [SerializeField] Transform _planet;

    void Update()
    {
        // TODO this should be buffered for the feels
        transform.position = _ship.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (_ship.position - _planet.position).normalized);
    }

}
