using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skilltree : MonoBehaviour
{
    [SerializeField] ResourceManager _resourceManager;

    int _upgradeRopeState = 0;
    List<int[]> _upgradeRopeCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}, new[]{4, 4}};

    int _upgradeShipGunState = 0;
    List<int[]> _upgradeShipGunCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    int _upgradeShipGunSpeedState = 0;
    List<int[]> _upgradeShipGunSpeedCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    int _upgradeShipHealthState = 0;
    List<int[]> _upgradeShipHealthCost = new(){new[]{1, 1}, new[]{2, 2}};

    int _upgradeShipSpeedState = 0;
    List<int[]> _upgradeShipSpeedCost = new(){new[]{1, 1}};

    int _upgradePistolState = 0;
    List<int[]> _upgradePistolCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    int _upgradeSpeedState = 0;
    List<int[]> _upgradeSpeedCost = new(){new[]{1, 1}, new[]{2, 2}, new[]{3, 3}};

    int _upgradeSlowState = 0;
    List<int[]> _upgradeSlowCost = new(){new[]{1, 1}, new[]{2, 2}};

    int _upgradeRangeState = 0;
    List<int[]> _upgradeRangeCost = new(){new[]{1, 1}};

    public void UpgradeRope(int level)
    {
        // Check if free to take
        if (level != _upgradeRopeState + 1)
        {
            return;
        }

        // Deduct Resources
        if (!_resourceManager.DeductResources(_upgradeRopeCost[level]))
        {
            return;
        }

        // Grey out the area that was just pressed

        // Add effect
        Debug.Log("EFFECT");
    }

    public void UpgradeShipGun(int level)
    {

    }

    public void UpgradeShipGunSpeed(int level)
    {
        
    }

    public void UpgradeShipHealth(int level)
    {

    }

    public void UpgradeShipSpeed(int level)
    {
        
    }

    public void UpgradePistol(int level)
    {

    }

    public void UpgradeSpeed(int level)
    {

    }

    public void UpgradeSlow(int level)
    {

    }

    public void UpgradeRange(int level)
    {

    }
}
