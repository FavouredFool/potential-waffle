using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Skilltree : MonoBehaviour
{
    [SerializeField] ResourceManager _resourceManager;

    [SerializeField] Sprite[] _textBGs;

    [SerializeField] UnityEngine.UI.Image [] _ropeImages;
    [SerializeField] UnityEngine.UI.Image [] _shipGunImages;
    [SerializeField] UnityEngine.UI.Image [] _shipGunSpeedImages;
    [SerializeField] UnityEngine.UI.Image [] _shipHealthImages;
    [SerializeField] UnityEngine.UI.Image [] _shipSpeedImages;
    [SerializeField] UnityEngine.UI.Image [] _pistolImages;
    [SerializeField] UnityEngine.UI.Image [] _speedImages;
    [SerializeField] UnityEngine.UI.Image [] _slowImages;
    [SerializeField] UnityEngine.UI.Image [] _rangeImages;


    float _currentMaxLevel = 0;

    const int _upgradeRopeStart = 0;
    int _upgradeRopeState = _upgradeRopeStart;
    List<int[]> _upgradeRopeCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}, new[]{4, 4}};

    const int _upgradeShipGunStart = 0;
    int _upgradeShipGunState = _upgradeShipGunStart;
    List<int[]> _upgradeShipGunCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    const int _upgradeShipGunSpeedStart = 0;
    int _upgradeShipGunSpeedState = _upgradeShipGunSpeedStart;
    List<int[]> _upgradeShipGunSpeedCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    const int _upgradeShipHealthStart = 1;
    int _upgradeShipHealthState = _upgradeShipHealthStart;
    List<int[]> _upgradeShipHealthCost = new(){new[]{1, 1}, new[]{2, 2}};

    const int _upgradeShipSpeedStart = 1;
    int _upgradeShipSpeedState = _upgradeShipSpeedStart;
    List<int[]> _upgradeShipSpeedCost = new(){new[]{1, 1}};

    const int _upgradePistolStart = 0;
    int _upgradePistolState = _upgradePistolStart;
    List<int[]> _upgradePistolCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    const int _upgradeSpeedStart = 0;
    int _upgradeSpeedState = _upgradeSpeedStart;
    List<int[]> _upgradeSpeedCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    const int _upgradeSlowStart = 1;
    int _upgradeSlowState = _upgradeSlowStart;
    List<int[]> _upgradeSlowCost = new(){new[]{1, 1}, new[]{2, 2}};

    const int _upgradeRangeStart = 1;
    int _upgradeRangeState = _upgradeRangeStart;
    List<int[]> _upgradeRangeCost = new(){new[]{1, 1}};

    public void Start()
    {
        ResetColorsForAllElements();
    }

    public void ResetColorsForAllElements() 
    {
        // To through all "blocks" of upgrades
        // compare it to the active Level
        // if its at or below the level -> blue
        // if its one above the level -> light grey
        // if its more above the level -> dark grey

        _currentMaxLevel = _upgradeRopeState;

        Image [][] images = new[]{_ropeImages, _shipGunImages, _shipGunSpeedImages, _shipHealthImages, _shipSpeedImages, _pistolImages, _speedImages, _slowImages, _rangeImages};
        int[] starts = new[]{_upgradeRopeStart, _upgradeShipGunStart, _upgradeShipGunSpeedStart, _upgradeShipHealthStart, _upgradeShipSpeedStart, _upgradePistolStart, _upgradeSpeedStart, _upgradeSlowStart, _upgradeRangeStart};
        int[] states = new[]{_upgradeRopeState, _upgradeShipGunState, _upgradeShipGunSpeedState, _upgradeShipHealthState, _upgradeShipSpeedState, _upgradePistolState, _upgradeSpeedState, _upgradeSlowState, _upgradeRangeState};

        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < images[j].Length; i++)
            {
                if (i < states[j] - starts[j])
                {
                    images[j][i].sprite = _textBGs[0];
                }
                else if (i < states[j]-starts[j]+1)
                {
                    if (i > _currentMaxLevel-starts[j])
                    {
                        images[j][i].sprite = _textBGs[2];
                    }
                    else
                    {
                        images[j][i].sprite = _textBGs[1];
                    }
                }
                else
                {
                    images[j][i].sprite = _textBGs[2];
                }
            }
        }
    }

    public void UpgradeRope(int level)
    {
        
        // Check if free to take
        if (level != _upgradeRopeState + 1)
        {
            return;
        }
        
        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeRopeCost[level-1]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeShipGun(int level)
    {
        // Check if free to take
        if (level != _upgradeShipGunState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeShipGunCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeShipGunSpeed(int level)
    {
                // Check if free to take
        if (level != _upgradeShipGunSpeedState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeShipGunSpeedCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeShipHealth(int level)
    {
        // Check if free to take
        if (level != _upgradeShipHealthState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeShipHealthCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeShipSpeed(int level)
    {
                // Check if free to take
        if (level != _upgradeShipSpeedState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeShipSpeedCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradePistol(int level)
    {
        // Check if free to take
        if (level != _upgradePistolState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradePistolCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeSpeed(int level)
    {
        // Check if free to take
        if (level != _upgradeSpeedState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeSpeedCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeSlow(int level)
    {
        // Check if free to take
        if (level != _upgradeSlowState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeSlowCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeRange(int level)
    {
        // Check if free to take
        if (level != _upgradeRangeState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeRangeCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed
        ResetColorsForAllElements();

        // Add effect
        Debug.Log("EFFECT");
    }
}
