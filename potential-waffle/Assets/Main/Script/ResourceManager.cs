using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType {METAL, GUTS}

    public int IronAmount {get; set;} = 0;
    public int GutsAmount {get; set;} = 0;

    public void AddResource(ResourceType type)
    {
        if (type == ResourceType.METAL)
        {
            IronAmount += 1;
        }
        else
        {
            GutsAmount += 1;
        }
    }
}
