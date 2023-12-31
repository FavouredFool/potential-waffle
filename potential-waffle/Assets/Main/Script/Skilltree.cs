using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Skilltree : MonoBehaviour
{
    [SerializeField] CameraMovement _cameraMovement;
    [SerializeField] Gun _gun;
    [SerializeField] InvertedCircleCollider2D _circleCollider;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] ShipMovement _ship;
    [SerializeField] EnemySpawner _spawner;
    [SerializeField] ControlManager _controlManager;

    public enum SkillEffect {ROPE, SHIPGUN, SHIPGUNSPEED, SHIPHEALTH, SHIPSPEED, PISTOL, SPEED, SLOW, RANGE}
    public ResourceManager ResourceManager;
    public Sprite[] TextBGs;
    public MenuOption[] MenuOptions;

    bool _firstSkillStarted = false;

    AudioManager _audio;

    public void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
        SetImages();
    }

    public void DoEffect(SkillEffect skillEffect)
    {
        _audio.Play("UIButton");
        if (!_firstSkillStarted)
        {
            _spawner.SetStartTime();
            _firstSkillStarted = true;
        }
        
        SetImages();

        switch(skillEffect)
        {
            case SkillEffect.ROPE:
                RopeEffect();
                break;
            case SkillEffect.SHIPGUN:
                ShipGunEffect();
                break;
            case SkillEffect.SHIPGUNSPEED:
                ShipGunSpeedEffect();
                break;
            case SkillEffect.SHIPHEALTH:
                ShipHealthEffect();
                break;
            case SkillEffect.SHIPSPEED:
                ShipSpeedEffect();
                break;
            case SkillEffect.PISTOL:
                PistolEffect();
                break;
            case SkillEffect.SPEED:
                SpeedEffect();
                break;
            case SkillEffect.SLOW:
                SlowEffect();
                break;
            case SkillEffect.RANGE:
                RangeEffect();
                break;
        }
    }

    public void RopeEffect()
    {
        // Decrease Zoom, increase Gun Range + Rope Length
        _cameraMovement.DecreaseZoom();
        _gun.IncreaseSearchDistance();
        _circleCollider.IncreaseBorderRadius();
        _controlManager.MoveLineUp();
    }

    public void ShipGunEffect()
    {
        // should also increase thruster force
        _gun.UpgradeDamageMultiplier();
    }

    public void ShipGunSpeedEffect()
    {
        _gun.UpgradeFireRate();
    }

    public void ShipHealthEffect()
    {
        _ship.UpgradeHealth();
    }

    public void ShipSpeedEffect()
    {
        _ship.UpgradeShipSpeed();
    }

    public void PistolEffect()
    {
        // Multiply Attack speed by 2
        _playerMovement.UpgradeFireRate();
    }

    public void SpeedEffect()
    {
        _playerMovement.UpgradeSpeed();
    }

    public void SlowEffect()
    {
        _spawner.UpdateSlow();
    }

    public void RangeEffect()
    {
        _playerMovement.UpgradeTimeTillKillMultiplicator();
    }

    public void SetImages()
    {
        foreach (MenuOption menuOption in MenuOptions)
        {
            menuOption.SetImage();
        }
    }
}
