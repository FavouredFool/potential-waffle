using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skilltree;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    [SerializeField] SkillEffect _skillEffect;
    [SerializeField] int[] _unlockCost;
    [SerializeField] MenuOption[] _requiredMenuOptions;
    [SerializeField] Skilltree _skilltree;

    Image _image;

    public bool IsUnlocked = false;

    public void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Click()
    {
        if (!Unlock())
        {
            return;
        }
        
        _skilltree.DoEffect(_skillEffect);
    }

    public bool Unlock()
    {
        if (IsUnlocked)
        {
            return false;
        }

        if (IsUnlockable() && _skilltree.ResourceManager.DeductResources(_unlockCost))
        {
            IsUnlocked = true;
            return true;
        }

        return false;
    }

    public bool IsUnlockable()
    {
        bool unlockable = true;
        foreach (MenuOption menuOption in _requiredMenuOptions)
        {
            if (!menuOption.IsUnlocked)
            {
                unlockable = false;
                break;
            }
        }

        return unlockable;
    }

    public void SetImage()
    {
        Sprite sprite;

        if (IsUnlocked)
        {
            sprite = _skilltree.TextBGs[0];
        }
        else if (IsUnlockable())
        {
            sprite = _skilltree.TextBGs[1];
        }
        else
        {
            sprite = _skilltree.TextBGs[2];
        }

        _image.sprite = sprite;
    }
}
