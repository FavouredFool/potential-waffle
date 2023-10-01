using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceManager;

public class ResourceCollectable : MonoBehaviour
{
    [SerializeField] ResourceType _type;
    [SerializeField] float _magnetRadius;
    [SerializeField] float _magnetForce;

    PlayerMovement _player;
    Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (_player == null)
        {
            Debug.LogError("NO PLAYER FOUND!");
        }
    }

    void FixedUpdate()
    {
        Vector2 toPlayer = (Vector2)(_player.transform.position - transform.position);

        if (toPlayer.magnitude < _magnetRadius)
        {
            _rigidBody.AddForce(toPlayer.normalized * _magnetForce);
        }
    }

    public ResourceType GetResourceType()
    {
        return _type;
    }
}
