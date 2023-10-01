using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Skilltree : MonoBehaviour
{
    public enum SkillEffect {ROPE, SHIPGUN, SHIPGUNSPEED, SHIPHEALTH, SHIPSPEED, PISTOL, SPEED, SLOW, RANGE}
    public ResourceManager ResourceManager;
    public Sprite[] TextBGs;
    public MenuOption[] MenuOptions;

    public void Start()
    {
        SetImages();
    }

    public void DoEffect(SkillEffect skillEffect)
    {
        SetImages();

        Debug.Log("Effect: " + skillEffect);
    }

    public void SetImages()
    {
        foreach (MenuOption menuOption in MenuOptions)
        {
            menuOption.SetImage();
        }
    }
}
