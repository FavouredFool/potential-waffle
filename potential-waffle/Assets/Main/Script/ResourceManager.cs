using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] Text _metalCounter;
    [SerializeField] Text _gutsCounter;

    public enum ResourceType {METAL, GUTS}

    public int MetalAmount {get; set;} = 0;
    public int GutsAmount {get; set;} = 0;

    void Start()
    {
        UpdateHUD();
    }

    public void AddResource(ResourceType type)
    {
        if (type == ResourceType.METAL)
        {
            MetalAmount += 1;
        }
        else
        {
            GutsAmount += 1;
        }

        UpdateHUD();
    }

    public void UpdateHUD()
    {
        _metalCounter.text = MetalAmount.ToString();
        _gutsCounter.text = GutsAmount.ToString();
    }
}
