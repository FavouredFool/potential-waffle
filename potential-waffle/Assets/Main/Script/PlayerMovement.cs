using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosMouse.z = 0;
        Debug.Log(worldPosMouse);

        transform.rotation = Quaternion.LookRotation(Vector3.forward,  Quaternion.Euler(0, 0, 90) * (worldPosMouse - transform.position).normalized);
    }
}
