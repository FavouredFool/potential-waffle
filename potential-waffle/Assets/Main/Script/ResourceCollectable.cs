using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceManager;

public class ResourceCollectable : MonoBehaviour
{
    [SerializeField] ResourceType _type;

    public ResourceType GetResourceType()
    {
        return _type;
    }
}
